using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.Gameplay.Commands
{
  public abstract class BaseCommand
  {

    protected GameObject target;

    public BaseCommand(GameObject target)
    {
      this.target = target;
    }

    public abstract IEnumerator Run();
  }
}