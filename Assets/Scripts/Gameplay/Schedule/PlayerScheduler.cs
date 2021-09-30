using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Gameplay.Schedule;
using UnityEditor;
using UnityEngine;


namespace BotScheduler.Gameplay.Schedule
{
  public class PlayerScheduler : MonoBehaviour
  {
    // command creation
    private int commandsCount = 4;

    private Schedule schedule;
    private Scheduler scheduler;

    void Start()
    {
    }

    public void RunSchedule()
    {
      foreach (BaseCommand command in schedule.commands) {
          if (command is BaseGameObjectCommand) {
              ((BaseGameObjectCommand) command).target = this.gameObject;
          }
      }

      StartCoroutine(scheduler.RunSchedule(schedule));
    }
  }

  [CustomEditor(typeof(PlayerScheduler))]
  public class ObjectBuilderEditor : Editor
  {
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      var scheduler = (PlayerScheduler)target;

      if (GUILayout.Button("Run schedule"))
      {
        scheduler.RunSchedule();
      }
    }
  }
}