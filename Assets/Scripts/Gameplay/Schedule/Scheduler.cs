using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using UnityEngine;

namespace BotScheduler.Gameplay.Schedule
{
  public class Scheduler : MonoBehaviour
  {
    private Schedule currentSchedule;
    private Coroutine currentRoutine;

    public IEnumerator RunSchedule(Schedule schedule)
    {
      currentSchedule = schedule;

      schedule.Restart();

      while (!schedule.IsLastCommand())
      {
        yield return RunNext();
      }
    }

    private IEnumerator RunNext()
    {
      BaseCommand nextCommand = currentSchedule.GetNext();

      if (currentRoutine != null)
      {
        StopCoroutine(currentRoutine);
      }

      IEnumerator commandEnumerator = nextCommand.Run();

      currentRoutine = StartCoroutine(commandEnumerator);

      return commandEnumerator;
    }
  }

}