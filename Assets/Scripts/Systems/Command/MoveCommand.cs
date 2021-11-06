using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotScheduler.Systems.Schedule;
using UnityEngine;

namespace BotScheduler.Systems.Commands
{
    public class MoveCommand : BaseGameObjectCommand, ISchedulable
    {

        protected Vector3 direction;
        protected float distance;

        public MoveCommand(Vector3 direction, float distance) : base()
        {
            this.direction = direction;
            this.distance = distance;
        }

        public override async Task Run()
        {
            var initialPosition = target.transform.position;
            var targetPosition = initialPosition + this.direction * distance;

            await RunInterpolated(1, (float delta) =>
            {
                rb.MovePosition(Vector3.Lerp(initialPosition, targetPosition, delta));
            });
        }
    }
}