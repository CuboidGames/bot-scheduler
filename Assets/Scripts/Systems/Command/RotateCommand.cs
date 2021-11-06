using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotScheduler.Systems.Schedule;
using UnityEngine;

namespace BotScheduler.Systems.Commands
{
    public class RotateCommand : BaseGameObjectCommand, ISchedulable
    {

        protected Vector3 axis;
        protected float distance;

        public RotateCommand(Vector3 axis, float distance) : base()
        {
            this.axis = axis / Mathf.PI * 180;
            this.distance = distance;
        }

        public override async Task Run()
        {
            var initialRotation = target.transform.rotation;
            var targetRotation = initialRotation * Quaternion.Euler(axis.x * this.distance, axis.y * this.distance, axis.z * this.distance);

            await RunInterpolated(1, (float delta) =>
            {
                rb.MoveRotation(Quaternion.Lerp(initialRotation, targetRotation, delta));
            });
        }
    }
}