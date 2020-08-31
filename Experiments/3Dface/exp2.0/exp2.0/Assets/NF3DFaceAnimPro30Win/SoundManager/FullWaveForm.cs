
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFAudio
{
    [ExecuteInEditMode]
    public class FullWaveFormData
    {
        public Texture2D WaveImage;
        public Texture2D MarkersImage;

        public readonly MarkerCalculator MarkersData = new MarkerCalculator();
    }


    [ExecuteInEditMode]
    public class FullWaveFormsGenerator
    {
        #region used for imitation that clip is loaded
        const float FakeClipDuration = 10f; 
        const float FakeClipSamplesRate = 10000; 
        #endregion 


        public readonly FullWaveFormData OneFullWaveForm;

        public float TotalWidth { get; private set; }

        public float TotalHeight { get; private set; }

        private int MarkersImageTextureHeight
        {
            get { return Mathf.RoundToInt(0.25f*TotalHeight); }
        }

        private int MarkersImageTextureWidth
        {
            get { return Mathf.RoundToInt(TotalWidth); }
        }

        private int OneElementWidth;
        private int OneElementHeight;
        private int ElementsNumber;


        public FullWaveFormsGenerator(FullWaveFormData waveFormData)
        {
            if (waveFormData == null)
            {
                Debug.LogError("Got null parameter");
            }
            OneFullWaveForm = waveFormData;
          
        }

        public void GenerateFakeWaveForm(float totalWidth, float totalHeight)
        {
            TotalHeight = totalHeight;
            TotalWidth = totalWidth;
            OneFullWaveForm.WaveImage = new Texture2D(Mathf.RoundToInt(totalWidth), Mathf.RoundToInt(totalHeight), TextureFormat.RGBA32, false);


            OneFullWaveForm.MarkersImage = new Texture2D(MarkersImageTextureWidth, MarkersImageTextureHeight, TextureFormat.RGBA32, false);
            OneFullWaveForm.MarkersData.ResetWithImageWidth(MarkersImageTextureWidth, FakeClipDuration, Mathf.RoundToInt(FakeClipDuration*FakeClipSamplesRate));
            DrawSilence();
            DrawSoundMarkersAtWaveFormImage();
            GenerateSoundMarkers();
           
        }

        public bool GenerateFullWaveForm(AudioClip clip, AudioImporter importer, float totalWidth, float totalHeight)
        {
            TotalHeight = totalHeight;
            TotalWidth = totalWidth;

            OneFullWaveForm.WaveImage = new Texture2D(Mathf.RoundToInt(totalWidth), Mathf.RoundToInt(totalHeight), TextureFormat.RGBA32, false);
            if (OneFullWaveForm.WaveImage == null)
            {
                Debug.Log("Can't generate waveform image");
                return false;
            }

            OneFullWaveForm.MarkersImage = new Texture2D(MarkersImageTextureWidth, MarkersImageTextureHeight, TextureFormat.RGBA32, false);
            
                OneFullWaveForm.MarkersData.ResetWithImageWidth(MarkersImageTextureWidth, clip);

            DrawSoundMarkersAtWaveFormImage();
            GenerateSoundMarkers();

            float[] samples = new float[clip.samples * clip.channels];



            clip.GetData(samples, 0);
            
            float maxAbs = 0;
            for (int i = 0; i < samples.Length; i++)
                maxAbs = maxAbs * maxAbs > samples[i] * samples[i] ? maxAbs : Mathf.Abs(samples[i]);
            
            if (maxAbs > 100 * float.Epsilon)
            {
                float nRate = 1f / maxAbs;
                for (int i = 0; i < samples.Length; i++)
                    samples[i] =  samples[i] * nRate;
            }
            DrawWave(samples, clip.channels);


            return OneFullWaveForm.WaveImage != null;
        }

        private void DrawWave(float[] samples, int channels )
        {


            TexturePen pen = new TexturePen();
            pen.Connect(OneFullWaveForm.WaveImage);
            pen.BackgroundColor = pen.GetPixelColor(0, 0);
            pen.PenColor = Color.green;
            pen.PenThinkness = 1;
            
            int width = OneFullWaveForm.WaveImage.width;
            int blocksCount = samples.Length / channels;
           
            for (int column = 0; column < width; column++)
            {
                float curPlace = (column + float.Epsilon) / width;
                int blockBegin = Mathf.RoundToInt(blocksCount * curPlace * channels);
                for (int j = 0; j < channels && blockBegin + j < samples.Length; j++)
                {
                    int row = (int)( 0.5f*(1+samples[blockBegin+j]) * OneFullWaveForm.WaveImage.height);
                    row = Mathf.Clamp(row, 0, OneFullWaveForm.WaveImage.height-1);
                    pen.DrawPixel(column, row    , Color.green);
                    pen.DrawPixel(column, row + 1, Color.green);
                    pen.DrawPixel(column, row - 1, Color.green);
                }

            }
            pen.Apply(); 
        }
        private void GenerateSoundMarkers()
        {
            TexturePen pen = new TexturePen();
            pen.Connect(OneFullWaveForm.MarkersImage);
            pen.BackgroundColor = pen.GetPixelColor(0, 0);
            pen.PenColor = Color.grey;

            pen.PenThinkness = 5;
            pen.PenColor = Color.gray;
            MarkerCalculator markersData = OneFullWaveForm.MarkersData;

            for(int i = 0; i < markersData.Count; i++)
            {
                markersData.SetCurrentMarker(i);

                MarkerCalculator.MarkerTypeId markerType = markersData.MarkerType;
                switch(markerType)
                {
                    case MarkerCalculator.MarkerTypeId.Start:
                    case MarkerCalculator.MarkerTypeId.End:
                        pen.DrawColumn(markersData.PlaceInPixels, 5, Color.white, 100);
                        break;

                    case MarkerCalculator.MarkerTypeId.Big:
                        pen.DrawColumn(markersData.PlaceInPixels, 3, Color.gray, 100);
                        break;
                    case MarkerCalculator.MarkerTypeId.Middle:
                        pen.DrawColumn(markersData.PlaceInPixels, 1, Color.white, 50);
                        break;

                    case MarkerCalculator.MarkerTypeId.Small:
                        pen.DrawColumn(markersData.PlaceInPixels, 1, Color.white, 25);
                        break;

                    default:
                        break;
                }
            }
           

            pen.Apply();
        }


        private void DrawSilence()
        {
            TexturePen pen = new TexturePen();
            pen.Connect(OneFullWaveForm.WaveImage);
            pen.BackgroundColor = pen.GetPixelColor(0, 0);
            pen.PenColor = Color.green;

            pen.PenThinkness = 1;
            pen.DrawRow( Mathf.RoundToInt(0.5f*OneFullWaveForm.WaveImage.height) );
            pen.Apply();
        }

        private void DrawSoundMarkersAtWaveFormImage()
        {
            TexturePen pen = new TexturePen();
            pen.Connect(OneFullWaveForm.WaveImage);
            pen.BackgroundColor = pen.GetPixelColor(0, 0);
            pen.PenColor = Color.grey;

            pen.PenThinkness = 5;


            MarkerCalculator markersData = OneFullWaveForm.MarkersData;

            for(int i = 0; i < markersData.Count; i++)
            {
                markersData.SetCurrentMarker(i);

                MarkerCalculator.MarkerTypeId markerType = markersData.MarkerType;
                switch(markerType)
                {
 
                    case MarkerCalculator.MarkerTypeId.Big:
                        pen.DrawColumn(markersData.PlaceInPixels, 3, Color.gray, 100);
                        break;
                    case MarkerCalculator.MarkerTypeId.Middle:
                        pen.DrawColumn(markersData.PlaceInPixels, 1, Color.white, 100);
                        break;

                    case MarkerCalculator.MarkerTypeId.Small:
                        pen.DrawColumn(markersData.PlaceInPixels, 1, Color.white, 100);
                        break;
                }
            }
            pen.DrawBounds(Color.gray);


            pen.Apply();
        }
    }
}
