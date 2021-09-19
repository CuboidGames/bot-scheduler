using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using UnityEngine;

namespace BotScheduler.Gameplay.Schedule
{
  public class Schedule
  {
    private List<BaseCommand> commands = new List<BaseCommand>();
    private int currentCommandIndex = -1;

    public void Enqueue(BaseCommand command)
    {
      commands.Add(command);
    }

    public void Dequeue(BaseCommand command)
    {
      var commandIndex = commands.IndexOf(command);

      if (currentCommandIndex >= commandIndex)
      {
        currentCommandIndex--;
      }

      commands.Remove(command);
    }

    public void Restart() {
      currentCommandIndex = -1;
    }

    public BaseCommand GetNext()
    {
      if (IsLastCommand())
      {
        throw new IndexOutOfRangeException();
      }

      currentCommandIndex++;

      return commands[currentCommandIndex];
    }

    public bool IsLastCommand()
    {
      return currentCommandIndex == commands.Count - 1;
    }
  }
}