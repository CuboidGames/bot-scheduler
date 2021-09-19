using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.Gameplay.Commands
{
  public class MoveCommand : BaseGameObjectCommand
  {

    protected Vector3 direction;
    protected float distance;

    public MoveCommand(GameObject target, Vector3 direction, float distance) : base(target)
    {
      this.direction = direction;
      this.distance = distance;
    }

    public override IEnumerator Run(float duration)
    {
      var initialPosition = target.transform.position;
      var targetPosition = initialPosition + this.direction * distance;

      yield return RunInterpolated(duration, (float delta) =>
      {
        target.transform.position = Vector3.Lerp(initialPosition, targetPosition, delta);
      });
    }
  }
}