using System;
using System.Collections.Generic;
using System.Reflection;

using MassAnimation.Resources;

using UnityEngine;
using UnityEditor;


namespace Assets.Scripts.NFAudio
{

    [ExecuteInEditMode]
    public class SoundAnalyzerManager
    {

        #region constants

        private const double PlayLengthInSeconds = 0.5;      

        #endregion


        #region members

        internal AudioClip audioClip;
        internal AudioImporter audioImporter;

        private readonly SoundUI _soundUI;
        private SoundFileLoader _loader;

        private bool _toFixDuration;

        private Rect _waveFormRect; 
        private int _audioBitRate;
        private double _timeToStop = Double.MaxValue;

        private readonly FullWaveFormsGenerator _fullWaveFormGenerator;

        private readonly FullWaveFormData _fullWaveFormData = new FullWaveFormData();


        private Rect box;
        private Rect ConsoleAreaRect;

        private string message;

        private Assembly _unityEditorAssembly;
        private Type _audioUtilClass;


        private MethodInfo _playClipMethod;
        private MethodInfo _pauseMethod;

        private MethodInfo _stopClipMethod;
        private MethodInfo _stopMethod;

        private MethodInfo _resumeClip;
        private MethodInfo _isClipPlaying;
        private MethodInfo _setSamplePosition;
        private MethodInfo _getClipSamplePosition;


        #endregion


        #region properties

        public StateId State { get; private set; }

        public float CurrentPlayBackPosition { get; private set; }

        private bool ToFixDuration
        {
            set
            {
                _toFixDuration = value;
            }
        }

        #endregion



        #region constructors

        public SoundAnalyzerManager()
        {
            try
            {

                _fullWaveFormGenerator = new FullWaveFormsGenerator(_fullWaveFormData);

                _soundUI = new SoundUI(_fullWaveFormData, SetPlayBackPosition);    

                _soundUI.FrameSelected += FrameSelected;
                _soundUI.KeyFrameSet += KeyFrameSet;
                _soundUI.KeyFrameDeleted += KeyFrameDeleted;

                BindExternalMethods();

                _soundUI.OnRefresh += () => { StopIfNeeded(); };

                _toFixDuration = true;

                CurrentPlayBackPosition = 0;
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
            catch (System.Exception exp)
            {
                Debug.Log(exp.Message);
            }

        }

        #endregion



        #region events & handlers

        public event EventHandler<IntEventArgs> AudioFrameSelected;
        public event EventHandler<IntEventArgs> AudioKeyFrameSet;
        public event EventHandler<IntEventArgs> AudioKeyFrameDeleted;

        #endregion



        #region public methods

    

        public void LoadFileFromFilesSystem(out string userSetPath)
        {
            userSetPath = null;

            try
            {

                string[] filters = { "Audio files", "mp3,wav,wma,ogg", "All Files", "*" };
                string path = EditorUtility.OpenFilePanelWithFilters("Audio to load", "", filters);

                if (string.IsNullOrEmpty(path))
                {
                    Debug.Log("Nothing to load: a file is not chosen. ");
                    userSetPath = null;
                    return;
                }
                
                LoadFile(path);
                userSetPath = path.Clone() as string;
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }
        }


        public bool IsLoaded()
        {
            return State == StateId.Ready || IsPlayingState();
        }

        public bool IsPlayingState()
        {
            return State == StateId.ClipIsPlaying;
        }


        public void Play(bool fixDuration)
        {
            try
            {
                if (audioClip != null)      
                {

                    int startSample = Mathf.RoundToInt(CurrentPlayBackPosition * (audioClip.samples - 1));

                    Play(startSample, fixDuration);
                }
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }
        }

        public void Play(int startPosition, bool fixDuration)
        {
            try
            { 

                if (audioClip == null)
                {
                    
                    return;
                }

                PauseClip();
                SetSamplePosition(startPosition);
                ResumeClip();


                if (IsClipPlaying())
                {
                    
                }
                else
                {
                    PlayClip(audioClip.samples - 1);
                    PauseClip();
                    SetSamplePosition(startPosition);
                    ResumeClip();
                }

                ToFixDuration = fixDuration;
                if (fixDuration)
                {
                    _timeToStop = GetCurrentPlayingTime() + PlayLengthInSeconds;
                }


                SetState(StateId.ClipIsPlaying, " clip is playing starting " + startPosition + " -- ");

            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }

        }

        public void SetPlayBackPosition(float audiofileFraction)
        {
            try
            {

                CurrentPlayBackPosition = Mathf.Clamp01(audiofileFraction);
                
                _soundUI.ShowPlayBackPosition(CurrentPlayBackPosition);
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
        }


        public void SetComponentRect(Rect componentRect)
        {
            _waveFormRect = componentRect;
        }

        public void SetComponentRect(float left, float top, float width, float height)
        {
            _waveFormRect.Set(left, top, width, height);
        }

        public void UpdateFromMainWindow()
        {
            try
            {
                _soundUI.RefreshWithOnGUI(_waveFormRect);

                if (State == StateId.ClipIsPlaying)
                {
                    if (!IsClipPlaying())
                    {
                        SetState(StateId.Ready, "-- May be clip is finished? ---");
                    }
                    float curpos = GetClipSamplePosition();

                    CurrentPlayBackPosition = Mathf.Clamp01((curpos) / ((float)audioClip.samples));
                    _soundUI.ShowPlayBackPosition(CurrentPlayBackPosition);
                }
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
        }


        public void OnWindowDestroy()
        {
            if (_loader != null)
            {
                _loader.Abort();
            }
            FinalStopClip();
        }


        public void LoadFile(string fsFilePath)
        {
            try
            {
                if (_loader == null)
                {
                    _loader = new SoundFileLoader(this);
                }
                audioClip = null;
                _loader.SyncLoadFile(fsFilePath);
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }
        }


     

        public void ReGenerateTextures(float newTotalWaveFormHeight, float newTotalWaveFormWidth)
        {
            bool rc = false;

            try
            {

                _soundUI.ClearSelectedFrames();

                rc = (audioClip != null && audioImporter != null);
                if (!rc)
                {
                    Debug.Log("No audio file is loaded. ");
                    _fullWaveFormGenerator.GenerateFakeWaveForm(newTotalWaveFormWidth, newTotalWaveFormHeight);
                }
                else
                {
                    _fullWaveFormGenerator.GenerateFullWaveForm(audioClip, audioImporter, newTotalWaveFormWidth, newTotalWaveFormHeight);
                    PlayClip(audioClip.samples - 1);
                    PauseClip();
                }

                SetSamplePosition(0);
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
            
        }

        public void OnSetKey()
        {
            _soundUI.OnSetKey(_fullWaveFormGenerator.OneFullWaveForm.MarkersData);
        }


        public void StopClip()
        {
            if (audioClip == null)
                return;
            
            _stopClipMethod.Invoke(null, new object[] { audioClip });
            SetState(StateId.Ready, "Clip is stopped");
            SetSamplePosition(1);

        }


        #endregion


        #region internal methods

        internal void SetState(StateId newState, string message)
        {
            if (State == StateId.CopyToTempFolderInProgress && newState != State)
            {
                _soundUI.FixedTimeProgressBar.Clear();
            }

            if (State == StateId.LoadingAudioData && newState != State)
            {
                _soundUI.FixedTimeProgressBar.Clear();
            }

            if (State == StateId.ClipIsPlaying && newState != State)
            {
                FinalStopClip();
            }


            switch (newState)
            {
                case StateId.CopyToTempFolderInProgress:
                    _soundUI.FixedTimeProgressBar.Activate(3f, "Copy file to temp folder", message);
                    
                    State = newState;

                    break;

                case StateId.LoadingAudioData:
                    _soundUI.FixedTimeProgressBar.Activate(3f, "Loading audio data", message);
                    State = newState;
                    
                    break;

                case StateId.Abort:
                    State = StateId.NotReady;
                    
                    break;

                case StateId.DataLoaded:
                    State = StateId.Ready;
                    
                    break;

                default:
                    State = newState;
                    
                    break;
            }

            UpdateFromMainWindow();

        }

        #endregion


        #region private methods

        
        private void  FrameSelected(int frameNumber)
        {


            IntEventArgs e = new IntEventArgs(frameNumber);

            EventHandler<IntEventArgs> temp = AudioFrameSelected;

            if (temp != null)
            {
                temp(this, e);
            }
        }

        
        private void KeyFrameSet(int frameNumber)
        {

            Debug.Log("A keyframe is added at:  " + frameNumber);

            IntEventArgs e = new IntEventArgs(frameNumber);

            EventHandler<IntEventArgs> temp = AudioKeyFrameSet;

            if (temp != null)
            {
                temp(this, e);
            }
        }

 
        private void KeyFrameDeleted(int frameNumber)
        {

            Debug.Log("A keyframe at " + frameNumber + "  is deleted. ");

            IntEventArgs e = new IntEventArgs(frameNumber);

            EventHandler<IntEventArgs> temp = AudioKeyFrameDeleted;

            if (temp != null)
            {
                temp(this, e);
            }
        }

        private double GetCurrentPlayingTime()
        {
            if (audioClip == null)
            {
                return 0.0;
            }

            var simplePosition = GetClipSamplePosition();
            var currentPlayBackPosition = Mathf.Clamp01((simplePosition) / ((float)audioClip.samples));
            return currentPlayBackPosition * _fullWaveFormData.MarkersData.TotalLenghtInSeconds;
        }

        private void StopIfNeeded()
        {
            if (_toFixDuration)
            {
                var currentPosition = GetCurrentPlayingTime();

                if (currentPosition <= _timeToStop || !IsPlayingState())
                {
                    return;
                }

                StopClip();
            }
        }

        private void BindExternalMethods()
        {
            try
            { 
                _unityEditorAssembly = typeof(AudioImporter).Assembly;
                _audioUtilClass = _unityEditorAssembly.GetType("UnityEditor.AudioUtil");

                //_playClipMethod = _audioUtilClass.GetMethod("PlayClip", BindingFlags.Static | BindingFlags.Public, null,
                //    new[] { typeof(AudioClip), typeof(Int32) }, null);

                _playClipMethod = _audioUtilClass.GetMethod(
                                            "PlayClip",
                                            BindingFlags.Static | BindingFlags.Public,
                                            null,
                                            new System.Type[] {
                                                            typeof(AudioClip),
                                                            typeof(Int32),
                                                            typeof(Boolean)
                                                              },
                                            null );

                _pauseMethod = _audioUtilClass.GetMethod("PauseClip", BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(AudioClip) }, null);

                _resumeClip = _audioUtilClass.GetMethod("ResumeClip", BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(AudioClip) }, null);

                _isClipPlaying = _audioUtilClass.GetMethod("IsClipPlaying", BindingFlags.Static | BindingFlags.Public);
                _setSamplePosition = _audioUtilClass.GetMethod("SetClipSamplePosition", BindingFlags.Static | BindingFlags.Public);

                _stopClipMethod = _audioUtilClass.GetMethod("StopClip", BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(AudioClip) }, null);
                _stopMethod = _audioUtilClass.GetMethod("StopAllClips", BindingFlags.Static | BindingFlags.Public);
                _getClipSamplePosition = _audioUtilClass.GetMethod("GetClipSamplePosition", BindingFlags.Static | BindingFlags.Public);
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
        }

        private bool IsClipPlaying()
        {
            bool playing = (bool) _isClipPlaying.Invoke(null, new object[] {audioClip});

            return playing;
        }

        private void PauseClip()
        {
            _pauseMethod.Invoke(null, new object[] {audioClip});
        }


        private void ResumeClip()
        {
            _resumeClip.Invoke(null, new object[] {audioClip});
        }

        private void SetSamplePosition(int samplePosition)
        {
            if(audioClip == null) return;
            _setSamplePosition.Invoke(null, new object[] {audioClip, samplePosition});
        }

        private void FinalStopClip()
        {
            _stopMethod.Invoke(null, null);
        }

        private int GetClipSamplePosition()
        {
            int position = (int) _getClipSamplePosition.Invoke(null, new object[] {audioClip});
            return position;
        }

        private void PlayClip(int startSample)
        {
            //_playClipMethod.Invoke(null, new object[] {audioClip, startSample});

            _playClipMethod.Invoke(null,
                                    new object[] {
                                            audioClip,
                                            startSample,
                                            false }
                                   );
        }

        #endregion

    }

    public enum StateId
    {
        NotReady,
        LoadingStarted,
        CopyToTempFolderInProgress,
        LoadingAudioData,
        DataLoaded,
        ClipIsPlaying,
        Abort,
        Ready,
    }


}
