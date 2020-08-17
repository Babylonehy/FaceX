
using System;
using System.Collections.Generic;
using System.Threading;

using MassAnimation.Adapters.PhotoAdapter;
using MassAnimation.AnimationElements;
using MassAnimation.Avatar.Entities;
using MassAnimation.Resources;
using MassAnimation.Resources.Entities;
using MassAnimation.UnityPluginConnector;

using UnityEditor;
using UnityEngine;

using Assets.Scripts.NFScript;
using Assets.Scripts.NFAudio;


namespace Assets.Scripts.NFEditor
{

    internal class Controller       
    {

		#region private members

        private readonly SoundAnalyzerManager _soundManager = new SoundAnalyzerManager();               

        private static Controller _instance;

		private static List<PickedPoint> _frontImagePickedPts;		
		private static string _frontImagePath;		
		private static EyeColor _eyeColor = EyeColor.Brown;
        private static int _speedUp = 2;
        private static bool _exportModelOnly = false;


        private bool _isMostAbstractLevel;
        private PoseConfigViewModel _currentPoseViewModel;

        [SerializeField]
        private ControlLever _currentCtrlLever;

        private ulong _currentFrame;


        private int _currentKeyFrame;
        private List<int> _audioKeyFrames;
        private bool _keyFrameSelected;
			
		#endregion


		#region internal members	

		internal static readonly List<EditorWindow> ApplicationWindows = new List<EditorWindow>();

        internal static bool ReadyToProceed = false;
		
		#endregion


		#region properties      

        internal static Controller Instance
        {
            get
            {
                return _instance ?? (_instance = new Controller());
            }
        }
			
		internal static string FrontImagePath 
		{
			get
			{
				return _frontImagePath;
			}
			set
			{
				if (value != null)
				{
					if (_frontImagePath == null) 
					{
						_frontImagePath = value;
					}
					else  
					{
						if (_frontImagePath != value)
						{
							_frontImagePath = value;
						}
					}
				}
				
			}
		}
		
		internal static EyeColor ColorOfEyes
		{
			get
			{
				return _eyeColor;
			}
			set
			{
				_eyeColor = value;
			}
		}


        internal static int SpeedUp
        {
            get
            {
                return _speedUp;
            }
            set
            {
                _speedUp = value;
            }
        }


        internal bool ExportModelOnly
        {
            get
            {
                return _exportModelOnly;
            }
            set
            {
                _exportModelOnly = value;

            }
        }

        internal bool IsMostAbstractLevel 
		{
			get
			{
				return _isMostAbstractLevel;
			}
			set
			{
				if (_isMostAbstractLevel == value)
				{
					return;
				}
				_isMostAbstractLevel = value;

				try
				{
					if (_isMostAbstractLevel)
					{
						_currentCtrlLever = ControlLever.Abstract;
					}
					else
					{
						_currentCtrlLever = ControlLever.Finest;
					}

					OnAbstractionLevelChanged();
				}
				catch(UnityException exp)
				{
					Debug.LogError(exp.Message);
				}
			}
		}

        internal int SelectedBrowIndex { get; set; }
        internal int SelectedExpressionIndex { get; set; }
        internal int SelectedMouthMuscleIndex { get; set; }

        internal float BrowMovement { get; set; }
        internal float ExpressionMovement { get; set; }
        internal float MouthMuscleMovement { get; set; }

        internal float LipNarrowWide { get; set; }
        internal float JawOpenClose { get; set; }

        internal float HorizontalEyeMovement { get; set; }
        internal float VerticalEyeMovement { get; set; }

        internal ulong CurrentFrame
        {
            get
            {
                return _currentFrame;
            }

            private set
            {
                try
                {
                    _currentFrame = value;
                }
                catch (UnityException exp)
                {
                    Debug.LogError(exp.Message);
                }
            }
        }

        internal bool KeyFrameSelected
        {
            get
            {
                return _keyFrameSelected;
            }
        }

        public SoundAnalyzerManager SoundManager 
        { 
            get 
            { 
                return _soundManager; 
            } 
        } 
		
		#endregion


		#region constructors
		
		private Controller()
		{

            ResetAllUIValues();

            ResetAllAudioValues();

            RegisterEvents();

            IsMostAbstractLevel = true;
		}
		
		#endregion

	  
		#region events & handlers

        internal event EventHandler<IntEventArgs> BrowChanged;
		internal event EventHandler<IntEventArgs> PrimaryExpressionChanged;		
		internal event EventHandler<IntEventArgs> MouthMuscleChanged;
		
		internal event EventHandler<IntensityEventArgs> BrowValueChanged;
        internal event EventHandler<IntensityEventArgs> ExpressionValueChanged;
		internal event EventHandler<IntensityEventArgs> MouthMuscleValueChanged;

        internal event EventHandler<IntensityEventArgs> LipNarrowWideChanged;
        internal event EventHandler<IntensityEventArgs> JawOpenCloseChanged;
        internal event EventHandler<IntensityEventArgs> EyeHorizontalMoved;
        internal event EventHandler<IntensityEventArgs> EyeVerticalMoved;

        internal event EventHandler<StrEventArgs> AnimatableCreated;

        internal event Action AbstractionLevelChanged;
		
		internal void OnBrowChanged(IntEventArgs e)
		{
			SelectedBrowIndex = e.IntVal;

			EventHandler<IntEventArgs> temp = BrowChanged;
			
			if (temp != null)
			{
				temp(this, e);
			}
		}

		internal void OnExpressionChanged(IntEventArgs e)
		{
			SelectedExpressionIndex = e.IntVal;
			
			EventHandler<IntEventArgs> temp = PrimaryExpressionChanged;
			
			if (temp != null)
			{
				temp(this, e);
			}
		}


		internal void OnMouthMuscleChanged(IntEventArgs e)
		{
			SelectedMouthMuscleIndex = e.IntVal;
			
			EventHandler<IntEventArgs> temp = MouthMuscleChanged;
			
			if (temp != null)
			{
				temp(this, e);
			}
		}

		internal void OnBrowValueChanged(IntensityEventArgs e)
		{
			BrowMovement = (float)e.Intensity;
			
			EventHandler<IntensityEventArgs> temp = BrowValueChanged;
			
			if (temp != null)
			{
				temp(this, e);
			}
		}

		internal void OnExpressionValueChanged(IntensityEventArgs e)
		{
			ExpressionMovement = (float)e.Intensity;
			
			EventHandler<IntensityEventArgs> temp = ExpressionValueChanged;
			
			if (temp != null)
			{
				temp(this, e);
			}
		}


		internal void OnMouthMuscleValueChanged(IntensityEventArgs e)
		{
			MouthMuscleMovement = (float)e.Intensity;
			
			EventHandler<IntensityEventArgs> temp = MouthMuscleValueChanged;
			
			if (temp != null)
			{
				temp(this, e);
			}
		}

        internal void OnLipNarrowWideChanged(IntensityEventArgs e)
        {
            LipNarrowWide = (float)e.Intensity;

            EventHandler<IntensityEventArgs> temp = LipNarrowWideChanged;

            if (temp != null)
            {
                temp(this, e);
            }
        }

        internal void OnJawOpenCloseChanged(IntensityEventArgs e)
        {
            JawOpenClose = (float)e.Intensity;

            EventHandler<IntensityEventArgs> temp = JawOpenCloseChanged;

            if (temp != null)
            {
                temp(this, e);
            }
        }

        internal void OnHorizontalEyeMoved(IntensityEventArgs e)
        {
            HorizontalEyeMovement = (float)e.Intensity;

            EventHandler<IntensityEventArgs> temp = EyeHorizontalMoved;

            if (temp != null)
            {
                temp(this, e);
            }
        }

        internal void OnVerticalEyeMoved(IntensityEventArgs e)
        {
            VerticalEyeMovement = (float)e.Intensity;

            EventHandler<IntensityEventArgs> temp = EyeVerticalMoved;

            if (temp != null)
            {
                temp(this, e);
            }
        }


        internal void SelectedExpressionChanged(object sender, IntEventArgs e)
        {
            try
            {
                UIController.OnControlReset(null, null);

                ResetExpUIValues();

            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }

        }

        private void SelectedBrowChanged(object sender, IntEventArgs e)
        {
            try
            {                
                UIController.RaiseAreaResetEvent(AnimatableArea.EYEBROW);

                ResetBrowUIValues();
            }
            catch (Exception exp)
            {
                Debug.LogError(exp.Message);
            }
        }

        private void SelectedMouthMuscleChanged(object sender, IntEventArgs e)
        {
            try
            {
                UIController.RaiseAreaResetEvent(AnimatableArea.MOUTH);

                ResetMouthUIValues();
            }
            catch (Exception exp)
            {
                Debug.LogError(exp.Message);
            }
        }

        void AudioFrameSelected(object sender, IntEventArgs e)
        {
            try
            {
                if (_audioKeyFrames != null)
                {
                    if (_audioKeyFrames.Contains(e.IntVal))
                    {
                        _keyFrameSelected = true;
                    }
                    else
                    {
                        _keyFrameSelected = false;
                    }
                }

                SetFrame((ulong)e.IntVal);
            }
            catch (Exception exp)
            {
                Debug.LogError(exp.Message);
            }
        }

        void AudioKeyFrameSet(object sender, IntEventArgs e)
        {
            try
            {
                _currentKeyFrame = e.IntVal;
                _audioKeyFrames.Add(_currentKeyFrame);
            }
            catch (Exception exp)
            {
                Debug.LogError(exp.Message);
            }
        }

        void AudioKeyFrameDeleted(object sender, IntEventArgs e)
        {
            try
            {
                _audioKeyFrames.Remove(e.IntVal);
                _currentKeyFrame = -1;

                RemoveKeyFrame(e.IntVal);
            }
            catch (Exception exp)
            {
                Debug.LogError(exp.Message);
            }
        }


		#endregion


		#region internal methods		

	    internal void CreateWindow<T>(Rect rect, string abstractionLevel) where T : EditorWindow
	    {
				try
				{
		            var wind = EditorWindow.GetWindowWithRect<T>(rect, true, abstractionLevel);
		            ApplicationWindows.Add(wind);
		            Debug.Log("Window is created");
				}
				catch(UnityException exp)
				{
					Debug.LogError(exp.Message);
				}
	    }

	    internal void Close()
	    {
	            foreach (var window in ApplicationWindows)
	            {
	                if (window != null)
	                {
                        //Debug.Log("Window is closing");
	                    window.Close();
	                }
	            }

	            _instance = null;
	    }


		#region build avatar

		internal static void AddFrontImagePoints(List<PickedPoint> pickedPts)
		{
			try
			{
				_frontImagePickedPts = pickedPts;
			}
			catch(UnityException exp)
			{
				Debug.LogError(exp.Message);
			}
		}


        internal static bool BuildAvatarFromPhotos()
        {
            
            bool success = false;

            try
            {
                Animatable animatable = BuildAvatarFromFrontImage();

                if (animatable != null)
                {

                    try
                    {                        
                        string texPath = ResourceDirectories.TempModelDirectory;
                        StrEventArgs strArgs = new StrEventArgs(texPath);
                        Controller.Instance.OnAnimatableCreated(strArgs);
                    }
                    catch (Exception oacExp)
                    {                        
                        Debug.LogError(oacExp.Message);
                    }

                    success = GenerateUnityModel(animatable);

                }

            }
            catch
            {                
                throw;
            }

            
            return success;
        }

        private static Animatable BuildAvatarFromFrontImage()
        {
            Animatable animatable = null;

            var projFolder = System.IO.Directory.GetCurrentDirectory();

            try
            {
                if (FrontImagePath == null)
                {

                    return animatable;
                }

                List<Point> ptList = new List<Point>();

                foreach (PickedPoint pickedFrontPt in _frontImagePickedPts)
                {
                    int xCord = (int)pickedFrontPt.Location.x;
                    int yCord = (int)pickedFrontPt.Location.y;
                    Point pt = new Point(xCord, yCord);

                    ptList.Add(pt);
                }

                Point[] pointLocations = ptList.ToArray();

                if ((pointLocations != null) && (pointLocations.Length == 11))
                {                    

                    ModelConnector mc = new ModelConnector();
                    animatable = mc.BuildAvatarFromFrontImage(FrontImagePath, pointLocations, ColorOfEyes, SpeedUp);

                }

            }
            catch
            {
 
                throw;
            }
            finally
            {
                System.IO.Directory.SetCurrentDirectory(projFolder);
            }

            return animatable;
        }


        internal static bool GenerateUnityModel(Animatable animatable)
        {
            bool success = false;

            AnimatableUnity animatableUnity = null;

            try
            {
                AnimationElementManager.RemoveExtraFeatures(animatable);
                animatableUnity = EntityHelper.CreateUnityAnimatable(animatable);

                if (animatableUnity != null)
                {
                    PluginManager.Instance.Model = animatableUnity;
                    PluginManager.Instance.GenerateModel();

                    success = true;
                }

            }
            catch (UnityException exp)
            {                
                Debug.LogError(exp.Message);
                throw;
            }

            return success;
        }

		#endregion

		#region animation

        internal void RememberPose(PoseConfigViewModel poseViewModel)
        {
            try
            {

                _currentPoseViewModel = poseViewModel;

                if (0 < _currentKeyFrame)
                {
                    _currentPoseViewModel.FrameNo = (ulong)_currentKeyFrame;
                }

                MassAnimation.Resources.Entities.Pose pose = Convertor.ConvertPoseConfigVMdlToPose(_currentPoseViewModel);

                UIController.AddPose(_currentCtrlLever, pose);

            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
        }

        internal void RemoveKeyFrame(int frameNo)
        {
            try
            {

                UIController.RemovePoseAt(frameNo);

            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
        }


        internal void AnimateRememberedPoses()
        {
            try
            {

                    PluginManager.Instance.AnimationRunning = true;
                    UIController.Animate3DAvatars();
                    PluginManager.Instance.AnimationRunning = false;

            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }
        }


        internal void OnAnimate()
        {
            try
            {
                Controller.Instance.ResetAllUIValues();
                
                Thread _animationThread = new Thread(Controller.Instance.AnimateRememberedPoses);
                _animationThread.Start();

                
                if (UIController.AudioPathExist())
                {
                    Controller.Instance.SoundManager.SetPlayBackPosition(0);
                    Controller.Instance.SoundManager.Play(false);
                }
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }
        }


		internal void UpdateAnimation(PoseConfigViewModel poseViewModel)
		{
			try
			{
                _currentPoseViewModel = poseViewModel;

                PluginManager.Instance.ChangePose(_currentPoseViewModel, _currentCtrlLever);
								
			}
			catch(UnityException exp)
			{
				Debug.LogError(exp.Message);
			}

		}

        internal void UpdateEyes(float eyeHorizontalMovement, float eyeVerticalMovement)
        {
            try
            {

                PluginManager.Instance.ChangeEyes(eyeHorizontalMovement, eyeVerticalMovement);
 
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }

        }


        internal void ResetAnimation(object sender, IntEventArgs e)
        {
            try
            {
                UIController.OnControlReset(null, null);

                ResetAllUIValues();

            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }

        }

		#endregion

		
		internal void SetFrame(ulong frameNo)
		{
			CurrentFrame = frameNo;
		}
		

		#endregion


		#region private methods

        private void ResetAllUIValues()
        {
            SelectedBrowIndex = 0;
            SelectedExpressionIndex = 0;
            SelectedMouthMuscleIndex = 0;

            BrowMovement = 0;
            ExpressionMovement = 0;
            MouthMuscleMovement = 0;
        
            JawOpenClose = 0;

            HorizontalEyeMovement = 0;
            VerticalEyeMovement = 0;

            LipNarrowWide = 50;
        }

        private void ResetAllAudioValues()
        {
            _currentKeyFrame = -1;
            _audioKeyFrames = new List<int>();

            _keyFrameSelected = false;

            _currentPoseViewModel = null;            

        }

        private void RegisterEvents()
        {
            BrowChanged += SelectedBrowChanged;

            MouthMuscleChanged += SelectedMouthMuscleChanged;

            PrimaryExpressionChanged += SelectedExpressionChanged;

            AnimatableCreated += ProcessTextureFiles;

            if (_soundManager != null)
            {
                _soundManager.AudioFrameSelected += AudioFrameSelected;
                _soundManager.AudioKeyFrameSet += AudioKeyFrameSet;
                _soundManager.AudioKeyFrameDeleted += AudioKeyFrameDeleted;
            }

        }
    


        private void ResetExpUIValues()
        {

            ExpressionMovement = 0;

            JawOpenClose = 0;

            HorizontalEyeMovement = 0;
            VerticalEyeMovement = 0;

            LipNarrowWide = 50;
        }


        private void ResetBrowUIValues()
        {

            BrowMovement = 0;

        }


        private void ResetMouthUIValues()
        {

            MouthMuscleMovement = 0;

            JawOpenClose = 0;

            LipNarrowWide = 50;
        }


		private void OnAbstractionLevelChanged()
		{
            try
            { 

                ResetAnimation(null, null);

			    if (AbstractionLevelChanged != null)
			    {
				    AbstractionLevelChanged();
			    }
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
		}

        internal void OnAnimatableCreated(StrEventArgs e)
        {
            EventHandler<StrEventArgs> temp = AnimatableCreated;

            if (temp != null)
            {
                temp(this, e);
            }
        }


        private void ProcessTextureFiles(object sender, StrEventArgs e)
        {
            try
            {
                PluginManager.Instance.ProcessTexture(e);
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
        }


        #endregion

    }

	internal enum DropDownEnum
	{
		brow,
		expression,
		mouthMuscles
	}

}
