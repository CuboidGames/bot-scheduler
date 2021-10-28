using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.Systems.Schedule
{
  public class Schedule
  {
    public List<ISchedulable> schedulables { get; private set; } = new List<ISchedulable>();
    private int currentSchedulableIndex = -1;
    public int size { get; private set; }

    public Schedule(int size)
    {
      this.size = size;

      for (int i = 0; i < size; i++)
      {
        this.schedulables.Add(null);
      }
    }

    public void Enqueue(int index, ISchedulable schedulable)
    {
      if (index > size - 1)
      {
        throw new IndexOutOfRangeException();
      }

      schedulables.Insert(index, schedulable);
    }

    public void Dequeue(int index, ISchedulable schedulable)
    {
      if (index > size - 1)
      {
        throw new IndexOutOfRangeException();
      }

      schedulables.Remove(schedulable);
      schedulables.Insert(index, null);
    }

    public void Restart()
    {
      currentSchedulableIndex = -1;
    }

    public ISchedulable GetNext()
    {
      if (IsLastSchedulable())
      {
        throw new IndexOutOfRangeException();
      }

      currentSchedulableIndex++;

      return schedulables[currentSchedulableIndex];
    }

    public bool IsLastSchedulable()
    {
      return currentSchedulableIndex == size - 1;
    }
  }
}