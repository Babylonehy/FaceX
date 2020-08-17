
using System;
using System.Collections.Generic;
using System.Linq;

using MassAnimation.Resources;

using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFAudio
{
    [ExecuteInEditMode]
    internal class SoundUI
    {

        #region members

        private readonly FullWaveFormData _waveFormData;

        private readonly Action<float> _onPositionSelected;
        

        private Rect _waveFormRect = new Rect(0, 0, 1, 1);
        private Rect _markerRect = new Rect(0, 0, 1, 1);

        private float _waveFormSelectionFrac;
        private Texture2D _waveFormSelectionTexture;
        private Rect _waveFormSelectionRect;

        private GUIStyle _bigMarkerStyle;
        private GUIStyle _smallMarkerStyle;

        internal readonly FixedTimeProgressBar FixedTimeProgressBar = new FixedTimeProgressBar();


        #endregion


        #region properties

        private GUIStyle BigMarkerStyle
        {
            get
            {
                return _bigMarkerStyle ?? (_bigMarkerStyle = new GUIStyle
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 10,
                    richText = true
                });
            }
        }

        private GUIStyle SmallMarkerStyle
        {
            get
            {
                if (_smallMarkerStyle == null)
                {
                    _smallMarkerStyle = new GUIStyle(BigMarkerStyle) { fontSize = 5 };
                }
                return _bigMarkerStyle;
            }
        }        

        internal int AudioFramesRate { get; set; }
 

        #endregion



        #region constructors

        internal SoundUI(FullWaveFormData newWaveformData, Action<float> onPositionSelected)
        {
            _waveFormData = newWaveformData;
            _onPositionSelected = onPositionSelected;
 

            AudioFramesRate = ControlConstants.FramesPerSecond;    
        }

        #endregion



        #region events & handlers & Action

        internal event Action<int> FrameSelected = i => { };
        internal event Action<int> KeyFrameSet = i => { };
        internal event Action<int> KeyFrameDeleted = i => { };

        public Action OnRefresh;

        #endregion


        #region internal methods

        internal void RefreshProgressbar()
        {
            FixedTimeProgressBar.RefreshInOnGUI();
        }


        internal void RefreshWithOnGUI(Rect componentRect)
        {
            _waveFormRect = componentRect;


            RefreshProgressbar();
            if (_waveFormData.WaveImage != null)
            {
                RedrawWaveFormImage();

                _markerRect.Set(_waveFormRect.xMin, _waveFormRect.yMin - _waveFormData.MarkersImage.height, _waveFormRect.width,
                    _waveFormData.MarkersImage.height);
                RedrawMarkersFormImage();
            }

            CheckMouseClickEvent();
            RedrawWaveformSelection();
            

            RedrawSelectedFrames();

            OnRefresh();
        }


        internal void ShowPlayBackPosition(float audofileFraction)
        {
            _waveFormSelectionFrac = audofileFraction;
            RedrawWaveformSelection();
        }


        internal void OnSetKey(MarkerCalculator markersData)
        {
            var selectionCenter = _waveFormRect.width * _waveFormSelectionFrac;

            var frame = new AudioFrame(); 
            frame.FrameNumber = (int)(SelectedTimePosition() * AudioFramesRate);
            frame.LeftBoundInPixels = selectionCenter - 5;
            frame.RightBoundInPixels = selectionCenter + 5;


            KeyFrameSet(frame.FrameNumber);

            SaveFrame(frame);
        }

        internal void ClearSelectedFrames()
        {
            _framesList.Clear();
        }


        #endregion


        #region private methods

        private void RedrawSelectedFrames()
        {

            try
            {            

                Texture2D solidColorTex = new Texture2D(1, 1);
                solidColorTex.SetPixel(0, 0, new Color(1f, 0, 0, 0.5f));
                solidColorTex.Apply();

                GUIStyle frameStyle = new GUIStyle(GUI.skin.box);
                frameStyle.normal.background = solidColorTex;

                foreach (var frame in _framesList)
                {
                    int shiftCoef = 0;
                    GUI.Box(
                        new Rect(frame.LeftBoundInPixels + 5 - shiftCoef, _waveFormRect.yMin + 2, (frame.RightBoundInPixels - frame.LeftBoundInPixels),
                            _waveFormRect.yMax - _waveFormRect.yMin - 5), "", frameStyle);
                }
            }
            catch (Exception exp)
            {
                Debug.LogError(exp.Message);
            }

        }

        private void GenerateWaveFormSelectionTexture()
        {
            try
            { 
                int textureWidth = 10;
                _waveFormSelectionTexture = new Texture2D(textureWidth, 1, TextureFormat.RGBA32, false);
                Color[] pixels = _waveFormSelectionTexture.GetPixels();
                Color color = Color.green;
                int xMax = textureWidth - 1;
                for(int x = 0; x <= xMax; x++)
                {
                    pixels[x] = color;
                    pixels[x].a = Mathf.Clamp(1f - 0.5f*(x - 0.5f*xMax)*(x - 0.5f*xMax), 0.25f, 1f);
                }
                _waveFormSelectionTexture.SetPixels(pixels);
                _waveFormSelectionTexture.Apply();
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
        }

        private void RedrawWaveformSelection()
        {
            try
            { 
                int imageHalfWidh = 2;
                if(_waveFormSelectionTexture == null)
                {
                    GenerateWaveFormSelectionTexture();
                }

                float xWaveFormSelectionCenter = _waveFormRect.xMin + _waveFormRect.width*_waveFormSelectionFrac;
                _waveFormSelectionRect = _waveFormRect;
                _waveFormSelectionRect.xMin = xWaveFormSelectionCenter - imageHalfWidh;
                _waveFormSelectionRect.xMax = xWaveFormSelectionCenter + imageHalfWidh;

                if (_waveFormSelectionTexture != null)
                {
                    GUI.DrawTexture(_waveFormSelectionRect, _waveFormSelectionTexture, ScaleMode.StretchToFill);

                    Rect labeRect = new Rect(_waveFormSelectionRect);
                    float labeWidth = _waveFormSelectionRect.height;
                    labeRect.xMin = xWaveFormSelectionCenter; 
                    labeRect.xMax = xWaveFormSelectionCenter + labeWidth;
                    labeRect.yMin = _waveFormSelectionRect.yMax + _waveFormSelectionRect.width;
                    labeRect.yMax = _waveFormSelectionRect.yMax + 5f * _waveFormSelectionRect.width;

                    float timePos = SelectedTimePosition();
                    GUI.Label(labeRect, string.Format("<size=10><color=white>{0:####.###}  sec</color> </size>", timePos), BigMarkerStyle);

                }
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }

        }

        private float SelectedTimePosition()
        {
            
            var selectedTimePosition = _waveFormSelectionFrac * _waveFormData.MarkersData.TotalLenghtInSeconds;
           
            return selectedTimePosition;
        }
        
        private bool _isAudioFileScrollingByMouse;

        private void _onFinishAudioFileScrolling()
        {
            if(!_isAudioFileScrollingByMouse)
            {
                return;
            }

            _isAudioFileScrollingByMouse = false;


            if(_onPositionSelected != null)
            {
                _onPositionSelected(_waveFormSelectionFrac);
            }
        }


        private void CheckMouseClickEvent()
        {
            var curEvent = Event.current;

            if(!curEvent.isMouse)
            {
                return;
            }
            if(curEvent.type == EventType.MouseUp && _isAudioFileScrollingByMouse)
            {
                _onFinishAudioFileScrolling();
                return;
            }

            var mousePosition = curEvent.mousePosition;

            switch (curEvent.button)
            {
                case 0:
                    LeftMouseClicked(mousePosition);
                    FrameSelected((int)(SelectedTimePosition() * AudioFramesRate));
                    break;
                case 1:
                    RightMouseClicked(mousePosition);
                    break;
            }
        }

        private void RightMouseClicked(Vector2 mousePosition)
        {
            if(!IsMouseEventOfComponentArea(mousePosition))
            {
                return;
            }

            var clickedFrame = GetSelectedClickedFrame(mousePosition);

            if(clickedFrame == null)
            {
                return;
            }

            var contextMenu = new GenericMenu();
            contextMenu.AddItem(new GUIContent("Delete selected key"), false, () =>
            {
                _framesList.Remove(clickedFrame);
                KeyFrameDeleted(clickedFrame.FrameNumber);
            });
            contextMenu.ShowAsContext();
        }

        private AudioFrame GetSelectedClickedFrame(Vector2 mousePosition)
        {
            var selectionCenter = mousePosition.x - _waveFormRect.xMin;       

            return _framesList.FirstOrDefault(f => selectionCenter > f.LeftBoundInPixels && selectionCenter < f.RightBoundInPixels);

        }

        private void LeftMouseClicked(Vector2 mousePosition)
        {
            bool mouseClickedOnWaveForm = IsMouseEventOfComponentArea(mousePosition);

            if (mouseClickedOnWaveForm)
            {
                _isAudioFileScrollingByMouse = true;
                Event.current.Use();
                float fraction =
                    Mathf.Clamp01((mousePosition.x - _waveFormRect.xMin)/(_waveFormRect.xMax - _waveFormRect.xMin));

                ShowPlayBackPosition(fraction);
            }

            if(_isAudioFileScrollingByMouse)
            {
                
                _onFinishAudioFileScrolling();
            }

            
        }

        private bool IsMouseEventOfComponentArea(Vector2 mousePosition)
        {
            float componentLenght = _waveFormRect.xMax - _waveFormRect.xMin;
            float indent = componentLenght*0.005f;

            return _waveFormRect.xMin - indent <= mousePosition.x && mousePosition.x <= _waveFormRect.xMax + indent &&
                   _waveFormRect.yMin <= mousePosition.y && mousePosition.y <= _waveFormRect.yMax;
        }

        
        private void RedrawMarkersFormImage()
        {
            GUI.DrawTexture(_markerRect, (_waveFormData.MarkersImage), ScaleMode.StretchToFill);
            Rect labelRect = _markerRect;

            labelRect.yMin = _markerRect.yMin - 2f*_markerRect.height;
            labelRect.height = 2f*_markerRect.height;

            var markerCalc = _waveFormData.MarkersData;
            float labelWidth = 5f*_markerRect.width/markerCalc.Count;
            labelRect.width = labelWidth;

            MarkerCalculator.MarkerTypeId curMarkerType;
            float placeInPixels;

            for(int i = 1; i < markerCalc.Count; i++)
            {
                curMarkerType = markerCalc.GetMarkerType(i);
                if(
                    curMarkerType != MarkerCalculator.MarkerTypeId.Small
                    && curMarkerType != MarkerCalculator.MarkerTypeId.NotFilled
                    )
                {
                    placeInPixels = _markerRect.xMin + i*_markerRect.width/markerCalc.Count;
                    labelRect.xMin = placeInPixels - 0.45f*labelWidth;
                    labelRect.xMax = placeInPixels + 0.45f*labelWidth;
                    if(curMarkerType == MarkerCalculator.MarkerTypeId.Big)
                    {
                        labelRect.yMin = _markerRect.yMin - 3f*_markerRect.height;

                        GUI.Label
                            (
                                labelRect,
                                string.Format(@"<size=10><color=white>{0:####.###}</color></size>",
                                    markerCalc.GetPlaceInSeconds(i)),
                                BigMarkerStyle
                            );
                    }
                    else
                    {
                        labelRect.yMin = _markerRect.yMin - 1.7f*_markerRect.height;
                        GUI.Label
                            (
                                labelRect,
                                string.Format(@"<size=8><color=white>{0:####.##}</color></size>",
                                    markerCalc.GetPlaceInSeconds(i)),
                                SmallMarkerStyle
                            );
                    }
                }
            }
        }

        private void RedrawWaveFormImage()
        {
            
            var temp = GUI.backgroundColor;

            GUI.backgroundColor = Color.white;
            
            GUI.DrawTexture(_waveFormRect, (_waveFormData.WaveImage), ScaleMode.StretchToFill);


            GUI.backgroundColor = temp;
        }

        private readonly List<AudioFrame> _framesList = new List<AudioFrame>();

        private void SaveFrame(AudioFrame frame)
        {
            if (_framesList.All(f => f.FrameNumber != frame.FrameNumber))
            {
                _framesList.Add(frame);
            }
        }

        #endregion


    }
}
