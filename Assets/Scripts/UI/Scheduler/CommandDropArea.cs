using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Gameplay.Schedule;
using UnityEngine;

namespace BotScheduler.UI
{
  public class CommandDropArea : LeanDropArea
  {
    public BaseCommand command;
    public Schedule schedule;

    public int index;

    public override void OnLeanDrop(LeanDrop drop)
    {
      base.OnLeanDrop(drop);

      schedule.Enqueue(index, command);
    }
  }
}