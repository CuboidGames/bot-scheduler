using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.Gameplay.Commands
{
  public abstract class BaseGameObjectCommand : BaseCommand
  {

    public GameObject target;

    public void RunFor(float duration, GameObject target)
    {
      this.target = target;

      Run(duration);
    }
  }
}