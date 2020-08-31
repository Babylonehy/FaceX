using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFEditor
{

    public class AboutWindow : EditorWindow
    {


        #region constants

        private const string TMMessage = "NaturalFront™ 3D Face Animation Unity Plugin Pro - Windows";

        private const string Rights_Message = "U.S. Patents 8988419 and 8555164. \n\n© 2020 NaturalFront. \n\nAll rights reserved. ";

        private const string Version_Message = "Version 3.2 ";

        #endregion


        #region public methods


        public void OnGUI()
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


            GUIStyle messageStyle = new GUIStyle(GUI.skin.label);
            messageStyle.fontSize = 16;

            GUI.Label(new Rect(30, 30, 550, 70), TMMessage, messageStyle);

            messageStyle.fontSize = 11;
            GUI.Label(new Rect(30, 150, 400, 200), Rights_Message, messageStyle);

            messageStyle.fontSize = 9;
            GUI.Label(new Rect(30, 320, 200, 30), Version_Message, messageStyle);

        }

        #endregion
    }
}
