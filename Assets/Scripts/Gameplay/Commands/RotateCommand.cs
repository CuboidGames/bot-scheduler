using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Commands
{
  public class RotateCommand : BaseCommand
  {

    protected Vector3 axis;
    protected float distance;

    public RotateCommand(GameObject target, Vector3 axis, float distance) : base(target)
    {
      this.axis = axis / Mathf.PI * 180;
      this.distance = distance;
    }

    public override IEnumerator Run()
    {
      var targetRotation = target.transform.rotation * Quaternion.Euler(axis.x * this.distance, axis.y * this.distance, axis.z * this.distance);

      while (Quaternion.Angle(target.transform.rotation, targetRotation) > 0.01)
      {
        target.transform.rotation = Quaternion.Lerp(target.transform.rotation, targetRotation, Time.deltaTime * 10);
        yield return null;
      }

      target.transform.rotation = targetRotation;
    }
  }
}