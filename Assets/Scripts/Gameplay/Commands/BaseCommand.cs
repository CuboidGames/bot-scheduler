using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.Gameplay.Commands
{
  public abstract class BaseCommand
  {

    protected delegate void RunInterpolatedCallback(float delta);

    public abstract IEnumerator Run(float duration);

    protected IEnumerator RunInterpolated(float duration, RunInterpolatedCallback func)
    {
      float initialTime = Time.time;

      func(0);

      while (initialTime + duration > Time.time)
      {
        func((Time.time - initialTime) / duration);
        yield return null;
      }

      func(1);
    }
  }
}