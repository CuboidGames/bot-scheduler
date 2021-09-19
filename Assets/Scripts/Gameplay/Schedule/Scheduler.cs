using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using UnityEngine;

namespace BotScheduler.Gameplay.Schedule
{
  public class Scheduler : MonoBehaviour
  {
    [SerializeField]
    private float stepDuration = 2f;
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

      currentRoutine = StartCoroutine(nextCommand.Run(stepDuration));

      yield return currentRoutine;
    }
  }

}