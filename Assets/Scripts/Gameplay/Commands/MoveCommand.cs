using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.Gameplay.Commands
{
  public class MoveCommand : BaseCommand
  {

    protected Vector3 direction;
    protected float distance;

    public MoveCommand(GameObject target, Vector3 direction, float distance) : base(target)
    {
      this.direction = direction;
      this.distance = distance;
    }

    public override IEnumerator Run()
    {
      var targetPosition = target.transform.position + this.direction * distance;

      while (Vector3.Distance(targetPosition, target.transform.position) > 0.01)
      {
        target.transform.position = Vector3.Lerp(target.transform.position, targetPosition, Time.deltaTime * 10);
        yield return null;
      }

      target.transform.position = targetPosition;
    }
  }
}