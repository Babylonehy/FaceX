using UnityEngine;

namespace Assets.Scripts.NFAudio
{

    [ExecuteInEditMode]
    public class TexturePen
    {
        private Texture2D _texure2D;
        private Color[] _texPoints;

        public Color PenColor = Color.green;
        public int PenThinkness = 3;

        private struct IntVector
        {
            public int X;
            public int Y;
            public int Index;
        }

        public int NormalizedToPixels(float normalized, int lastIndex)
        {
            return Mathf.Clamp(Mathf.RoundToInt(normalized * lastIndex), 0, lastIndex);
        }


        public void Connect(Texture2D newTexture)
        {
            _texure2D = newTexture;
            _texPoints = _texure2D.GetPixels();
            _rightTopCornerPixel.X = newTexture.width - 1;
            _rightTopCornerPixel.Y = newTexture.height - 1;
            _rightTopCornerPixel.Index = newTexture.width * newTexture.height - 1;


            if (_texPoints.Length != _rightTopCornerPixel.Index + 1)
            {
                Debug.LogError("Something wrong with memory, size of array not correspond to texture size: texPoints.Length = " + _texPoints.Length +
                               "  RightDownPixel.index = " + _rightTopCornerPixel.Index);
            }
        }

        public void DrawRow(int rowIndex)
        {
            DrawRow(rowIndex, PenThinkness, PenColor);
        }

        public void DrawRow(int rowIndex, int lineThikness, Color penColor)
        {

            if (lineThikness == 1)
            {
                DrawSinglePixelRow(rowIndex, penColor);
                return;
            }
            int delta = lineThikness / 2;

            int startRow = rowIndex - delta + (lineThikness % 2 == 0 ? 1 : 0), endRow = rowIndex + delta;
            if (startRow < 0)
            {
                startRow = 0;
                endRow = startRow + lineThikness - 1;
            }

            if (endRow > _rightTopCornerPixel.Y)
            {
                endRow = _rightTopCornerPixel.Y;
                startRow = _rightTopCornerPixel.Y - lineThikness + 1;
            }

            for (int curRow = startRow; curRow <= endRow; curRow++)
                DrawSinglePixelRow(curRow, penColor);

        }

        public void DrawSinglePixelRow(int rowIndex, Color penColor)
        {
            if (rowIndex < 0 || rowIndex > _rightTopCornerPixel.Y)
                return;
            int rowWidth = _rightTopCornerPixel.X + 1;

            for (int x = 0; x < rowWidth; x++)
            {
                _texPoints[rowIndex * rowWidth + x] = penColor;
            }
        }

        public void DrawColumn(int columnIndex)
        {
            DrawColumn(columnIndex, PenThinkness, PenColor);
        }

        public void DrawColumn(int columnIndex, int lineThikness, Color penColor)
        {
            DrawColumn(columnIndex, lineThikness, penColor, 100f);
        }

        public void DrawColumn(int columnIndex, int lineThikness, Color penColor, float percentage)
        {

            if (lineThikness == 1)
            {
                DrawSinglePixelColumn(columnIndex, penColor, percentage);
                return;
            }
            int delta = lineThikness / 2;

            int startColumn = columnIndex - delta + (lineThikness % 2 == 0 ? 1 : 0);
            int endColumn = columnIndex + delta;
            if (startColumn < 0)
            {
                startColumn = 0;
                endColumn = startColumn + lineThikness - 1;
            }

            if (endColumn > _rightTopCornerPixel.X)
            {
                endColumn = _rightTopCornerPixel.X;
                startColumn = _rightTopCornerPixel.X - lineThikness + 1;
            }

            for (int curColumn = startColumn; curColumn <= endColumn; curColumn++)
                DrawSinglePixelColumn(curColumn, penColor, percentage);

        }

        public void DrawSinglePixelColumn(int columnIndex, Color penColor)
        {
            if (columnIndex < 0 || columnIndex > _rightTopCornerPixel.X)
                return;
            for (int y = 0; y <= _rightTopCornerPixel.Y; y++)
                DrawPixel(columnIndex, y, penColor);

        }
        public void DrawSinglePixelColumn(int columnIndex, Color penColor, float percentage)
        {
            if (columnIndex < 0 || columnIndex > _rightTopCornerPixel.X)
                return;
            float rate = 0.01f * Mathf.Clamp(percentage, 0f, 100f);
            if (rate > 0.99)
            {
                for (int y = 0; y <= _rightTopCornerPixel.Y; y++)
                    DrawPixel(columnIndex, y, penColor);

                return;
            }
            int columnCenter = Mathf.RoundToInt(0.5f * _rightTopCornerPixel.Y);
            int halfColumnHeight = Mathf.FloorToInt(0.5f * rate * _rightTopCornerPixel.Y);

            for (int y = columnCenter - halfColumnHeight; y <= columnCenter + halfColumnHeight; y++)
            {
                DrawPixel(columnIndex, y, penColor);
            }

        }

        public Color GetPixelColor(int columnIndex, int rowIndex)
        {
            if (IsOutOfBounds(columnIndex, rowIndex))
                return Color.clear;
            int rowWidth = _rightTopCornerPixel.X + 1;
            int pixelIndex = rowIndex * rowWidth + columnIndex;
            return _texPoints[pixelIndex];
        }

        public bool DrawOnBackgroundOnly = true;
        public Color BackgroundColor = Color.clear;


        public bool IsOutOfBounds(int columnIndex, int rowIndex)
        {
            return (
                rowIndex < 0
                || rowIndex > _rightTopCornerPixel.Y
                || columnIndex < 0
                || columnIndex > _rightTopCornerPixel.X
                );

        }

        public void DrawPixel(int columnIndex, int rowIndex, Color penColor)
        {

            if (IsOutOfBounds(columnIndex, rowIndex))
                return;
                
            int rowWidth = _rightTopCornerPixel.X + 1;
            int pixelIndex = rowIndex * rowWidth + columnIndex;

           /*
            if (DrawOnBackgroundOnly && _texPoints[pixelIndex] != BackgroundColor)
                return;
            */
            _texPoints[pixelIndex] = penColor;

        }

        public void DrawBounds(Color penColor)
        {

            DrawRow(0, PenThinkness, penColor);
            DrawRow(_rightTopCornerPixel.Y, PenThinkness, penColor);
            DrawColumn(0, PenThinkness, penColor, 100);
            DrawColumn(_rightTopCornerPixel.X, PenThinkness, penColor, 100);
        }



        public void Apply()
        {

            _texure2D.SetPixels(_texPoints);
            _texure2D.Apply();
        }


        private IntVector _rightTopCornerPixel;
    }

}