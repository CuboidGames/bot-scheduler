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

    private int size;

    public Schedule(int size)
    {
      this.size = size;

      for (int i = 0; i < size; i++)
      {
        this.commands.Add(new NoopCommand());
      }
    }

    public void Enqueue(int index, BaseCommand command)
    {
      if (index > size - 1)
      {
        throw new IndexOutOfRangeException();
      }

      commands.Insert(index, command);
    }

    public void Dequeue(int index, BaseCommand command)
    {
      if (index > size - 1)
      {
        throw new IndexOutOfRangeException();
      }

      commands.Remove(command);
      commands.Insert(index, new NoopCommand());
    }

    public void Restart()
    {
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
      return currentCommandIndex == size - 1;
    }
  }
}