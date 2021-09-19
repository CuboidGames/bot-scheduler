using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.Gameplay.Commands
{
  public class RotateCommand : BaseGameObjectCommand
  {

    protected Vector3 axis;
    protected float distance;

    public RotateCommand(GameObject target, Vector3 axis, float distance) : base(target)
    {
      this.axis = axis / Mathf.PI * 180;
      this.distance = distance;
    }

    public override IEnumerator Run(float duration)
    {
      var initialRotation = target.transform.rotation;
      var targetRotation = initialRotation * Quaternion.Euler(axis.x * this.distance, axis.y * this.distance, axis.z * this.distance);

      yield return RunInterpolated(duration, (float delta) => {
        target.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, delta);
      });
    }
  }
}