using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.Gameplay.Commands
{
  public abstract class BaseGameObjectCommand : BaseCommand
  {

    protected GameObject target;

    public BaseGameObjectCommand(GameObject target)
    {
      this.target = target;
    }
  }
}