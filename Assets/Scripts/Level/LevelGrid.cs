using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using UnityEditor;
using System.Threading.Tasks;

namespace Level
{
    [AddComponentMenu("LevelGrid/Menu")]
    public class LevelGrid : MonoBehaviour
    {
        [SerializeField] private int width = 0;
        [SerializeField] private int height = 0;

        [SerializeField] private float cellSize = 1;

        [SerializeField] private GameObject tilePrefab;

        private GridSystem.Grid<GameObject> grid;

        void Start()
        {
            GenerateGrid();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var newTile = Instantiate(tilePrefab, transform);
                grid.SetCell(
                    Random.Range(0, width),
                    Random.Range(0, height),
                    newTile
                );
            }
        }

        [ContextMenu("Generate Grid")]
        void GenerateGrid()
        {
            grid = new GridSystem.Grid<GameObject>(
                width,
                height,
                cellSize,
                Vector3.zero
            );

            grid.OnGridValueChanged += ValueChangeHandler;

            grid.GenerateCells();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var newTile = Instantiate(tilePrefab, transform);
                    grid.SetCell(i, j, newTile);
                }
            }
        }

        [ContextMenu("Destroy Grid")]
        void DestroyGrid()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            grid.OnGridValueChanged -= ValueChangeHandler;

            grid = null;
        }

        void ValueChangeHandler(object sender, OnGridValueChangedEventArgs<GameObject> update)
        {
            ReplaceTile(update.oldValue, update.newValue, update.x, update.y);
        }

        async void ReplaceTile(GameObject oldTile, GameObject newTile, int x, int y)
        {
            if (oldTile != null) {
                oldTile.transform.localPosition = grid.GetWorldPosition(x, y);
                Destroy(oldTile);

                await Task.Delay(100);
            }

            newTile.transform.localPosition = grid.GetWorldPosition(x, y);

        }

        void Destroy()
        {
            DestroyGrid();
        }
    }
}
