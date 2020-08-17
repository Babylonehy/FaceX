
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFEditor
{

    public class ExportChoiceWindow : EditorWindow
    {
        private readonly GUIContent[] _contents =
            {                
                new GUIContent("  Export the model with animation"),
                new GUIContent("  Export the current pose only")
            };

        private int _selected;

        public void OnGUI()
        {
            _selected = GUI.SelectionGrid(
                new Rect(20, 50, 280, 75),
                Controller.Instance.ExportModelOnly ? 0 : 1,
                _contents,
                1,
                EditorStyles.radioButton) ;

            if (_selected == 0)
            {
                Controller.Instance.ExportModelOnly = false;
            }
            else
            {
                Controller.Instance.ExportModelOnly = true;
            }

            var buttonRect = new Rect(20 + 280 - 120, 50 + 75 + 30, 85, 20);

            if (GUI.Button(buttonRect, "OK"))
            {

                try
                {
                    AppUIWindow.OnExport();
                }
                catch (UnityException exp)
                {
                    Debug.LogError(exp.Message);
                }

                Close();

            }

        }
    }
}
