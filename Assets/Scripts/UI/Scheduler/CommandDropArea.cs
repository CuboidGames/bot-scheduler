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

    public GameObject player;

    public override void OnLeanDrop(LeanDrop droppable)
    {
      base.OnLeanDrop(droppable);

      // TODO: Find a way to resolve this through the method signature
      if (droppable is CommandDrop)
      {
        var command = ((CommandDrop) droppable).command;

        if (command is BaseGameObjectCommand) {
          var gameObjectCommand = (BaseGameObjectCommand) command;

          gameObjectCommand.SetTarget(player);
        }

        schedule.Enqueue(index, command);
      }
    }
  }
}