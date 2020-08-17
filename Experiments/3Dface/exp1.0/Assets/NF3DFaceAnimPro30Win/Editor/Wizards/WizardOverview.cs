
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFEditor
{

    public class WizardOverview : EditorWindow, IWizardPage
    {

        #region constants

        private const string Message =
            "   Overview:\n\n\n   A front-face photo similar to the example is required.\n\n\n     ";

        private const string FollowGuides = "Please strictly follow guidelines";
        private const string AvoidMistakes = "Also avoid mistakes";

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

            DrawInstructions();

            if (GUI.Button(new Rect(20, 520, 75, 20), "Next"))
            {
                OnNext();
            }

        }

        public void OnNext()
        {
            WizardController.GoToPage<WizardLoadPhotos>(this);
        }

        public void OnBack()
        {
        }

        public void OnReset()
        {
            
        }

        #endregion


        #region private methods

        private void DrawInstructions()
        {

            GUIStyle messageStyle = new GUIStyle(GUI.skin.label);
            messageStyle.fontSize = 11;

            GUIStyle linkStyle = new GUIStyle(GUI.skin.label);
            Color linkColor = EditorGUIUtility.isProSkin ? new Color(0.5f, 0.5f, 1) : Color.blue;
            linkStyle.normal.textColor = linkColor;
            linkStyle.hover.textColor = linkColor;

            GUI.Label(new Rect(10, 10, 400, 200), Message, messageStyle);

            if (GUI.Button(new Rect(160, 375, 200, 20), FollowGuides, linkStyle))
            {
                Application.OpenURL("https://www.youtube.com/watch?v=DS7IkiH5thw");
            }

            if (GUI.Button(new Rect(160, 400, 200, 20), AvoidMistakes, linkStyle))
            {
                Application.OpenURL("https://www.youtube.com/watch?v=nN2TkKnrzTg");
            }

            var frontImage = (Texture)Resources.Load("FrontImage");

            GUI.DrawTexture(new Rect(160, 170, 120, 155), frontImage, ScaleMode.StretchToFill);
            GUI.Label(new Rect(295, 250, 100, 20), "(example)");

        }

        #endregion

    }

}
