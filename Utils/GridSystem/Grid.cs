using System;
using UnityEngine;
using Utils.Misc;

namespace Utils.GridSystem
{
    /**
     * This class is based on the video of CodeMonkey on how to build a Grid System
     * Thanks for him !
     */
    public class Grid<TGridObject>
    {
        public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
        public class OnGridObjectChangedEventArgs
        {
            public int w;
            public int h;
        }
        private readonly int _width;
        private readonly int _height;
        private readonly float _cellSize;
        private readonly Vector3 _origin;

        private readonly TGridObject[,] gridArray;
        private readonly TextMesh[,] debugTextArray;
        
        public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject, bool showDebug = false)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _origin = originPosition;
            
            gridArray = new TGridObject[width, height];

            for (int w = 0; w < gridArray.GetLength(0); w++)
            {
                for (int h = 0; h < gridArray.GetLength(1); h++)
                {
                    gridArray[w, h] = createGridObject(this, w, h);
                }
            }

            if (showDebug)
            {
                debugTextArray = new TextMesh[width, height];
                for (int w = 0; w < gridArray.GetLength(0); w++)
                {
                    for (int h = 0; h < gridArray.GetLength(1); h++)
                    {
                        debugTextArray[w,h] = WorldText.CreateWorldText(gridArray[w, h]?.ToString(), null, GetWorldPosition(w, h) + new Vector3(cellSize, cellSize) * .5f, 20, Color.yellow, TextAnchor.MiddleCenter);
                        Debug.DrawLine(GetWorldPosition(w,h), GetWorldPosition(w, h +1), Color.yellow, 100f);
                        Debug.DrawLine(GetWorldPosition(w,h), GetWorldPosition(w + 1, h), Color.yellow, 100f);
                    }
                }
            
                Debug.DrawLine(GetWorldPosition(0,height), GetWorldPosition(width, height), Color.yellow, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.yellow, 100f);

                OnGridObjectChanged += (sender, eventArgs) => debugTextArray[eventArgs.w, eventArgs.h].text = gridArray[eventArgs.w, eventArgs.h]?.ToString();
            }
        }

        public int GetWidth() => _width;
        public int GetHeight() => _height;

        public float GetCellSize() => _cellSize;

        public Vector3 GetWorldPosition(int w, int h)
        {
            return new Vector3(w, h) * _cellSize + _origin;
        }

        public void GetXY(Vector3 worldPosition, out int w, out int h)
        {
            w = Mathf.FloorToInt((worldPosition - _origin).x / _cellSize);
            h = Mathf.FloorToInt((worldPosition - _origin).y / _cellSize);
        }

        public void SetGridObject(int w, int h, TGridObject value)
        {
            if (w >= 0 && h >= 0 && w < _width && h < _height)
            {
                gridArray[w, h] = value;
                if(OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs{ w = w, h = h});
            }
        }

        public void TriggerGridObjectChanged(int w, int h)
        {
            if(OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs{ w = w, h = h});
        }

        public void SetGridObject(Vector3 worldPosition, TGridObject value)
        {
            GetXY(worldPosition, out var w, out var h);
            SetGridObject(w, h, value);
        }

        public TGridObject GetGridObject(int w, int h)
        {
            if (w >= 0 && h >= 0 && w < _width && h < _height)
            {
                return gridArray[w, h];
            }

            return default;
        }
        
        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            GetXY(worldPosition, out var w, out var h);
            return GetGridObject(w,h);
        }
    }
}