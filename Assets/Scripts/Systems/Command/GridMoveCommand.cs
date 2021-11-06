using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotScheduler.Systems.Schedule;
using UnityEngine;

namespace BotScheduler.Systems.Commands
{
    public class GridMoveCommand : BaseGameObjectCommand, ISchedulable
    {
        protected GridSystem.Grid<GameObject> grid;

        int x = 0;
        int y = 0;

        public GridMoveCommand(int x, int y) : base()
        {
            this.x = x;
            this.y = y;
        }

        public void SetGrid(GridSystem.Grid<GameObject> grid)
        {
            this.grid = grid;
        }

        public override async Task Run()
        {
            var initialPosition = target.transform.position;
            var currentCoordinates = grid.GetGridPosition(initialPosition);
            var targetCoordinates = grid.GetCell(currentCoordinates.x, currentCoordinates.y);
            var targetPosition = grid.GetWorldPosition(targetCoordinates.x + x, targetCoordinates.y + y);

            await RunInterpolated(1, (float delta) =>
            {
                rb.MovePosition(Vector3.Lerp(initialPosition, targetPosition, delta));
            });
        }
    }
}