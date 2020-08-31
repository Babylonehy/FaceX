
using System;

using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFEditor
{

    public class GetReadyWindow : EditorWindow
    {
        private const string Message =
            "\n To get the software work properly, please: \n\n   -- make sure your computer meeting the minimum requirements; \n\n   -- while the software is running, close all debuggers/decompilers/reflectors. \n\n\n\n By clicking \"Done\", you acknowdge the above conditions are satisfied.";

        public void OnGUI()
        {
            try
            {

                Color defaultTextColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;

                GUI.skin.button.normal.textColor = defaultTextColor;
                GUI.skin.button.onHover.textColor = defaultTextColor;
                GUI.skin.label.normal.textColor = defaultTextColor;
                GUI.skin.label.onNormal.textColor = defaultTextColor;
                GUI.skin.label.onHover.textColor = defaultTextColor;
                EditorStyles.radioButton.onFocused.textColor = defaultTextColor;
                EditorStyles.radioButton.onHover.textColor = defaultTextColor;
                EditorStyles.radioButton.onActive.textColor = defaultTextColor;
                EditorStyles.radioButton.onNormal.textColor = defaultTextColor;
                EditorStyles.radioButton.normal.textColor = defaultTextColor;

                GUI.TextArea(new Rect(25, 30, 580, 200), Message);

                var buttonRect = new Rect(25 + 580 - 105, 30 + 200 + 50, 105, 20);

                if (GUI.Button(buttonRect, "Done"))
                {

                    Controller.ReadyToProceed = true;
                    WizardController.ActiveWindow = this;
                    Close();

                }

            }
            catch (Exception exp)
            {

                EditorUtility.DisplayDialog("Error", exp.Message, "Ok");
            }

        }

    }

}
