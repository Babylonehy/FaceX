using System;

using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFAudio
{
    [ExecuteInEditMode]
    public class FixedTimeProgressBar
    {
        private float _startVal;
        private float _seconds;
        private float _progress;

        private bool _isActive;
        private string _title;
        private string _info;
        private Action _onCancel;

        public void Activate(float seconds, string title, string info)
        {
            Activate(seconds, title, info, null);
        }

        public void Activate(float seconds, string title, string info, Action onCancel)
        {
            _seconds = seconds;
            _startVal = Convert.ToSingle(EditorApplication.timeSinceStartup);
            _isActive = true;
            _progress = 0f;
            _title = title;
            _info = info;
            _onCancel = onCancel;
        }


        public void RefreshInOnGUI()
        {
            if (!_isActive)
                return;
            if (EditorUtility.DisplayCancelableProgressBar(
                _title, _info,
                _seconds > 1f && _progress < _seconds ? (_progress / _seconds) : _progress))
            {
                Debug.Log("Progress bar canceled by the user");

                if (_onCancel != null)
                    _onCancel();
                Clear();
                _progress = Convert.ToSingle(EditorApplication.timeSinceStartup) - _startVal;
            }
        }


        public void Clear()
        {
            _isActive = false;
            _startVal = 0;
            EditorUtility.ClearProgressBar();
        }

    }
}