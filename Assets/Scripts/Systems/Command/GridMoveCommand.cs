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
        protected Vector3 direction;
        protected float distance;

        public GridMoveCommand(Vector3 direction, float distance) : base()
        {
            this.direction = direction;
            this.distance = distance;
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
            var targetPosition = grid.GetWorldPosition(targetCoordinates.x, targetCoordinates.y + 1);

            await RunInterpolated(1, (float delta) =>
            {
                target.transform.position = Vector3.Lerp(initialPosition, targetPosition, delta);
            });
        }
    }
}