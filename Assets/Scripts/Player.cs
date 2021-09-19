using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Gameplay.Schedule;
using UnityEngine;

namespace BotScheduler
{
  public class Player : MonoBehaviour
  {
    private Coroutine currentAction;

    [SerializeField]
    private Scheduler scheduler;

    private Schedule schedule1;
    private Schedule schedule2;

    void Start()
    {
      schedule1 = new Schedule(4);
      schedule2 = new Schedule(6);

      schedule1.Enqueue(0, new MoveCommand(gameObject, gameObject.transform.forward, 0.24f));
      schedule1.Enqueue(1, new RotateCommand(gameObject, gameObject.transform.up, Mathf.PI / 2));
      schedule1.Enqueue(3, new MoveCommand(gameObject, gameObject.transform.forward, 0.24f));

      schedule2.Enqueue(0, new MoveCommand(gameObject, gameObject.transform.forward, 0.24f));
      schedule2.Enqueue(1, new MoveCommand(gameObject, -gameObject.transform.forward, 0.24f));
      schedule2.Enqueue(2, new MoveCommand(gameObject, gameObject.transform.forward, 0.24f));
      schedule2.Enqueue(3, new MoveCommand(gameObject, -gameObject.transform.forward, 0.24f * 2));
      schedule2.Enqueue(4, new RotateCommand(gameObject, gameObject.transform.up, Mathf.PI));
      schedule2.Enqueue(5, new MoveCommand(gameObject, gameObject.transform.forward, 0.24f));
    }

    void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        RunSchedule(schedule1);
      }

      if (Input.GetMouseButtonDown(1))
      {
        RunSchedule(schedule2);
      }
    }

    private void RunSchedule(Schedule schedule)
    {
      if (currentAction != null)
      {
        StopCoroutine(currentAction);
      }

      currentAction = StartCoroutine(scheduler.RunSchedule(schedule));
    }
  }

}