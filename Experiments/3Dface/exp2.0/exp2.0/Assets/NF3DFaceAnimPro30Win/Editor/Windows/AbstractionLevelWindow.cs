
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFEditor
{

    public class AbstractionLevelWindow : EditorWindow
    {
        private readonly GUIContent[] _contents =
            {
                new GUIContent("  Abstract level"),
                new GUIContent("  Fine-control level")
            };

        private int _selected;

        public void OnGUI()
        {
            _selected = GUI.SelectionGrid(
                new Rect(20, 50, 150, 75),
                Controller.Instance.IsMostAbstractLevel ? 0 : 1,
                _contents,
                1,
                EditorStyles.radioButton) ;

            if (_selected == 0)
            {
                Controller.Instance.IsMostAbstractLevel = true;
            }
            else
            {
                Controller.Instance.IsMostAbstractLevel = false;
            }
        }
    }
}
