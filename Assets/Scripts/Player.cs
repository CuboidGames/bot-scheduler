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
      schedule1 = new Schedule();
      schedule2 = new Schedule();

      schedule1.Enqueue(new MoveCommand(gameObject, gameObject.transform.forward, 0.24f));
      schedule1.Enqueue(new RotateCommand(gameObject, gameObject.transform.up, Mathf.PI / 2));
      schedule1.Enqueue(new MoveCommand(gameObject, gameObject.transform.forward, 0.24f));

      schedule2.Enqueue(new MoveCommand(gameObject, gameObject.transform.forward, 0.24f));
      schedule2.Enqueue(new MoveCommand(gameObject, -gameObject.transform.forward, 0.24f));
      schedule2.Enqueue(new MoveCommand(gameObject, gameObject.transform.forward, 0.24f));
      schedule2.Enqueue(new MoveCommand(gameObject, -gameObject.transform.forward, 0.24f * 2));
      schedule2.Enqueue(new RotateCommand(gameObject, gameObject.transform.up, Mathf.PI));
      schedule2.Enqueue(new MoveCommand(gameObject, gameObject.transform.forward, 0.24f));
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