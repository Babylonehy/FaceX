using UnityEngine;

namespace Assets.Scripts.NFAudio
{
    public class MarkerCalculator
    {
        public enum MarkerTypeId
        {
            NotFilled,
            Start,
            End,
            Big,
            Middle,
            Small
        }


        public int CurrentMarkerId { get; private set; }

        public float PlaceInSeconds
        {
            get { return GetPlaceInSeconds(CurrentMarkerId); }

        }

        public float GetPlaceInSeconds(int markerId)
        {
            return Mathf.Clamp((markerId * TotalLenghtInSeconds) / (Count + 0f), 0f, TotalLenghtInSeconds); 

        }
        public float PlaceInSamples
        {
            get { return Mathf.RoundToInt((CurrentMarkerId*TotalLenghtInSamples)/(Count + 0f)); }

        }

        public int PlaceInPixels
        {
            get { return GetPlaceInPixels(CurrentMarkerId); }

        }

        public int GetPlaceInPixels(int markerId)
        {
            return Mathf.RoundToInt((markerId * TotalLenghtInPixels) / (float)Count); 

        }


        public MarkerTypeId MarkerType
        {
            get { return GetMarkerType(CurrentMarkerId); }

        }

        public MarkerTypeId GetMarkerType(int markerId)
        {
            MarkerTypeId ret;
            if (markerId == 0)
                ret = MarkerTypeId.Start;
            else if (markerId >= Count - 1)
                ret = MarkerTypeId.End;
            else if (markerId % 10 == 0)
                ret = MarkerTypeId.Big;
            else if (markerId % 5 == 0)
                ret = MarkerTypeId.Middle;
            else
                ret = MarkerTypeId.Small;
            return ret;
        }

        
        private int _count = 3;

        public int Count
        {
            get { return _count; }
            private set { _count = value > 0 ? value : 3; }
        }

        public const int MINIMAL_DISTANCE_IN_PIXELS = 5;
        public float TotalLenghtInSeconds { get; private set; }
        public int TotalLenghtInSamples { get; private set; }
        public int TotalLenghtInPixels { get; private set; }
        private readonly float[] _timeScaleUnits = { 0.001f, 0.005f, 0.01f, 0.05f, 0.1f, 0.25f, 0.5f, 1f, 5f, 10f, 25f, 60f, 100f, 600f, 3600f, 10000f };
        private int _selectedTimeScaleUnitId = -1;

        public float TimeUnitLenghtInSecond {
            get
            {
                if (_selectedTimeScaleUnitId < 0)
                    return 0;
                return _timeScaleUnits[_selectedTimeScaleUnitId];
            }
        }

        public int ResetWithImageWidth(int markerImageWidth, float lenghtInSeconds, int lenghtInSamples)
        {
            TotalLenghtInPixels = markerImageWidth;
            TotalLenghtInSeconds = lenghtInSeconds;
            TotalLenghtInSamples = lenghtInSamples;

            int maximalPossibleCount = TotalLenghtInPixels / MINIMAL_DISTANCE_IN_PIXELS + 1 + (TotalLenghtInPixels % MINIMAL_DISTANCE_IN_PIXELS > 0 ? 1 : 0);

            float minimalPossibleUnitInSeconds = TotalLenghtInSeconds / (maximalPossibleCount + 0f);
            _selectedTimeScaleUnitId = -1;
            float selectedUnitInSeconds = -1f;
            for (int i = 0; i < _timeScaleUnits.Length; i++)
            {
                if (minimalPossibleUnitInSeconds < _timeScaleUnits[i] && selectedUnitInSeconds < 0f)
                {
                    _selectedTimeScaleUnitId = i;
                    selectedUnitInSeconds = _timeScaleUnits[_selectedTimeScaleUnitId];
                    break;
                }
            }
            if (_selectedTimeScaleUnitId < 0)
            {
                _selectedTimeScaleUnitId = _timeScaleUnits.Length - 1;
                selectedUnitInSeconds = _timeScaleUnits[_selectedTimeScaleUnitId];
                Debug.LogError("Actual sound file is definitelly too long to work with it...");
            }

            Count = Mathf.RoundToInt(TotalLenghtInSeconds / selectedUnitInSeconds);

            return Count;

        }


        public int ResetWithImageWidth(int markerImageWidth, AudioClip clip)
        {
            return ResetWithImageWidth(markerImageWidth, clip.length, clip.samples);
        }

        public void SetCurrentMarker(int markerId)
        {
            CurrentMarkerId = Mathf.Clamp(markerId, 0, Count);
        }


        public float GetDistanceBetweenMarkers()
        {
            return TotalLenghtInPixels/(float)Count;
        }

        public AudioFrame GetFrameByCoordinates(float point)
        {
            var frame = new AudioFrame();

            const int SMALL_MARKERS_IN_BIG_ONE = 10;
            var distanceBetweenBigsMarkers = GetDistanceBetweenMarkers()*SMALL_MARKERS_IN_BIG_ONE;
            var frameNumber = (int)(point/ distanceBetweenBigsMarkers);
            int left = frameNumber * SMALL_MARKERS_IN_BIG_ONE;
            int right = left + SMALL_MARKERS_IN_BIG_ONE;

            frame.LeftBoundInPixels= GetPlaceInPixels(left);
            frame.RightBoundInPixels = GetPlaceInPixels(right);
            frame.FrameNumber = frameNumber + 1;

            return frame;

        }
    }
}