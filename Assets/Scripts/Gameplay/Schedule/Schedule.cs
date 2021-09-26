using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using UnityEngine;

namespace BotScheduler.Gameplay.Schedule
{
  public class Schedule
  {
    private List<BaseCommand> _commands = new List<BaseCommand>();
    public List<BaseCommand> commands { get { return _commands; } }
    private int currentCommandIndex = -1;

    private int _size;
    public int size { get { return _size; } }

    public Schedule(int size)
    {
      this._size = size;

      for (int i = 0; i < size; i++)
      {
        this._commands.Add(new NoopCommand());
      }
    }

    public void Enqueue(int index, BaseCommand command)
    {
      if (index > _size - 1)
      {
        throw new IndexOutOfRangeException();
      }

      _commands.Insert(index, command);
    }

    public void Dequeue(int index, BaseCommand command)
    {
      if (index > _size - 1)
      {
        throw new IndexOutOfRangeException();
      }

      _commands.Remove(command);
      _commands.Insert(index, new NoopCommand());
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

      return _commands[currentCommandIndex];
    }

    public bool IsLastCommand()
    {
      return currentCommandIndex == _size - 1;
    }
  }
}