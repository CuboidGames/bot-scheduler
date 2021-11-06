using System;
using UnityEngine;

namespace BotScheduler.Systems.GridSystem
{
    public class Grid<T>
    {
        public event EventHandler<OnGridValueChangedEventArgs<T>> OnGridValueChanged;

        public readonly int width;

        public readonly int height;

        public readonly float cellSize;

        private readonly Vector3 position;

        private readonly Cell<T>[,] gridArray;

        public Grid(int width, int height, float cellSize, Vector3 originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.position = originPosition;

            gridArray = new Cell<T>[width, height];
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            var worldPosition = new Vector3(x, 0, y) * cellSize;
            var centerShift = new Vector3(((float)width - 1) / 2, 0, ((float)height - 1) / 2) * cellSize;

            return worldPosition + position - centerShift;
        }

        public Vector2Int GetGridPosition(Vector3 worldPosition)
        {
            var centerShift = new Vector3(((float)width) / 2, 0, ((float)height) / 2) * cellSize;
            var origin = worldPosition - position + centerShift;

            return new Vector2Int(
                Mathf.FloorToInt(origin.x / cellSize),
                Mathf.FloorToInt(origin.z / cellSize)
            );
        }

        public Cell<T> GetCell(int x, int y)
        {
            return gridArray[x, y];
        }

        public void SetCell(int x, int y, T value)
        {
            GetCell(x, y).SetValue(value);
        }

        public void SetCell(Vector2 worldPosition, T value)
        {
            var gridPosition = GetGridPosition(worldPosition);

            SetCell(gridPosition.x, gridPosition.y, value);
        }

        public void SetCell(Vector3 worldPosition, T value)
        {
            var gridPosition = GetGridPosition(worldPosition);

            SetCell(gridPosition.x, gridPosition.y, value);
        }

        public void GenerateCells()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    gridArray[i, j] = new Cell<T>(i, j, OnGridValueChanged);
                }
            }
        }
    }
}
