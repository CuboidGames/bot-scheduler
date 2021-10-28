using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Systems.Commands;
using BotScheduler.Systems.Schedule;
using UnityEngine;

namespace BotScheduler.UI
{
  public class CommandDropArea : LeanDropArea
  {
    public Schedule schedule;

    public int index;

    public override void OnLeanDrop(LeanDrop droppable)
    {
      base.OnLeanDrop(droppable);

      // TODO: Find a way to resolve this through the method signature
      if (droppable is CommandDrop)
      {
        schedule.Enqueue(index, ((CommandDrop)droppable).command);
      }
    }
  }
}