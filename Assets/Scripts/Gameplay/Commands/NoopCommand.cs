using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.Gameplay.Commands
{
  public class NoopCommand : BaseCommand
  {

    public override IEnumerator Run(float duration)
    {
      float initialTime = Time.time;

      while (initialTime + duration > Time.time)
      {
        yield return null;
      }
    }
  }
}