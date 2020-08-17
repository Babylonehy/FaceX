using System;
using System.Collections.Generic;
using System.Linq;

using MassAnimation.Resources;
using MassAnimation.Resources.Entities;
using MassAnimation.UnityPluginConnector;

using UnityEditor;
using UnityEngine;  

using Assets.Scripts.NFScript;
using Assets.Scripts.NFAudio;


namespace Assets.Scripts.NFEditor
{

    class AudioVisualizationWindow : EditorWindow
    {

        #region constants

        private const float TOP_POSITION = 5;
        private const float LEFT_POSITION = 5;
        private const float TEXT_BOX_WIDTH = 50;
        private const float TEXT_BOX_HEIGHT = 20;
        private const float BUTTON_WIDTH = 40;
        private const float BUTTON_HEIGHT = 20;
        private const float SLIDER_WIDTH = 900;
        private const int DEFAULT_NUMBER_OF_AUDIO_BOXES = 42;

        private const int AUDIO_BOX_HEIGHT = 50;

        private const int AUDIO_BOX_WIDTH = 30;


        private readonly Rect _playerRect = new Rect(LEFT_POSITION, TOP_POSITION, 1250, AUDIO_BOX_HEIGHT);

        #endregion


        #region public methods


        public void OnGUI()
        {
            try
            { 
                DrawPlayer();
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }

        }

        #endregion


        #region internal methods



        internal void OnLoadAudio(out string audioFilePath)
        {
            audioFilePath = null;

            string userSetPath = null;

            Controller.Instance.SoundManager.LoadFileFromFilesSystem(out userSetPath);
            RegenerateTextures();

            if (Controller.Instance.SoundManager.IsLoaded())
            {                
                audioFilePath = userSetPath;
            }

            Controller.Instance.SoundManager.SetPlayBackPosition(0);
        }


        internal void RegenerateTextures()
        {
            Controller.Instance.SoundManager.ReGenerateTextures(AUDIO_BOX_HEIGHT, AUDIO_BOX_WIDTH * DEFAULT_NUMBER_OF_AUDIO_BOXES);
        }


        internal void LoadAudio(string audioFilePath)
        {

            try
            { 
                Controller.Instance.SoundManager.LoadFile(audioFilePath); ;

                RegenerateTextures();

                Controller.Instance.SoundManager.SetPlayBackPosition(0);
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }
        }

        #endregion


        #region private methods

        private void DrawPlayer()
        {

            DrawAudioPanel(_playerRect);

            DrawAddKey(_playerRect);
            

            DrawPlayerButtons(LEFT_POSITION + 10, TOP_POSITION + AUDIO_BOX_HEIGHT + 30, BUTTON_WIDTH, BUTTON_HEIGHT);


        }

        private static void DrawAddKey(Rect playerRect)
        {
            var temp = GUI.backgroundColor;
            GUI.backgroundColor = Color.yellow;
            if (GUI.Button(new Rect(playerRect.xMax + 50, playerRect.yMin, AUDIO_BOX_WIDTH * 2, AUDIO_BOX_HEIGHT), "Add Key"))
            {
                Controller.Instance.SoundManager.OnSetKey();
            }
            GUI.backgroundColor = temp;
        }


        private static float DrawSliderArea(float currentLeftPosition, float topPosition, float textBoxWidth, float textBoxHeight, float sliderWidth)
        {
            GUI.enabled = false;
            GUI.TextField(new Rect(currentLeftPosition, topPosition, textBoxWidth, textBoxHeight), "0");
            GUI.TextField(new Rect(currentLeftPosition += textBoxWidth + 4, topPosition, textBoxWidth, textBoxHeight), "0");

            GUI.HorizontalSlider(new Rect(currentLeftPosition += textBoxWidth + 4, topPosition, sliderWidth, textBoxHeight), 0, 0, 100);

            GUI.TextField(new Rect(currentLeftPosition += sliderWidth + 4, topPosition, textBoxWidth, textBoxHeight), "0");
            GUI.TextField(new Rect(currentLeftPosition += (textBoxWidth + 4), topPosition, textBoxWidth, textBoxHeight), "0");

            GUI.enabled = true;
            return currentLeftPosition;
        }


        private static void DrawPlayerButtons(float currentLeftPosition, float topPosition, float buttonWidth, float buttonHeight)
        {
            var temp = GUI.backgroundColor;
            GUI.backgroundColor = Color.yellow;

            if (GUI.Button(new Rect(currentLeftPosition, topPosition, buttonWidth, buttonHeight), "|<<"))
            {
                Controller.Instance.SoundManager.SetPlayBackPosition(0);

            }
  

            if (Controller.Instance.SoundManager.IsPlayingState())
            {
                if (GUI.Button(new Rect(currentLeftPosition += buttonWidth + 4, topPosition, buttonWidth, buttonHeight), "||"))
                {
                    Controller.Instance.SoundManager.StopClip();
                }
            }
            else
            {
                if (GUI.Button(new Rect(currentLeftPosition += buttonWidth + 4, topPosition, buttonWidth, buttonHeight), ">"))
                {
                    Controller.Instance.SoundManager.Play(true);
                }
            }
 

            if (GUI.Button(new Rect(currentLeftPosition + (buttonWidth + 4), topPosition, buttonWidth, buttonHeight), ">||"))
            {
                Controller.Instance.SoundManager.SetPlayBackPosition(1);
            }

            GUI.backgroundColor = temp;
        }


        private void DrawAudioPanel(Rect panelRect)
        {
            Controller.Instance.SoundManager.SetComponentRect(panelRect);
            if (Controller.Instance.SoundManager.State == StateId.ClipIsPlaying)        
            {
                Repaint();
            }
            Controller.Instance.SoundManager.UpdateFromMainWindow();
        }

        #endregion

    }

}
