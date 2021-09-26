using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Gameplay.Schedule;
using UnityEditor;
using UnityEngine;

namespace BotScheduler.UI
{
  public class ScheduleCreator : MonoBehaviour
  {
    private Scheduler scheduler;
    private Schedule schedule;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private CommandDropArea dropAreaPrefab;

    [SerializeField]
    private float dropAreaWidth = 120;

    [SerializeField]
    private int commandsCount = 4;

    private void Start()
    {
      scheduler = GetComponent<Scheduler>();
      schedule = new Schedule(commandsCount);

      for (int i = 0; i < commandsCount; i++)
      {
        float offset = (i - (commandsCount / 2.0f) + 0.5f);
        float targetX = offset * dropAreaWidth;

        var newDropArea = Instantiate<CommandDropArea>(
            dropAreaPrefab,
            transform,
            false);

        newDropArea.index = i;
        newDropArea.command = new MoveCommand(player, player.transform.forward, 0.25f);
        newDropArea.schedule = schedule;

        newDropArea.transform.localPosition = new Vector3(
            targetX,
            0,
            0
        );
      }
    }

    public void RunSchedule()
    {
      StartCoroutine(scheduler.RunSchedule(schedule));
    }
  }

  [CustomEditor(typeof(ScheduleCreator))]
  public class ObjectBuilderEditor : Editor
  {
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      var myScript = (ScheduleCreator)target;

      if (GUILayout.Button("Run schedule"))
      {
        myScript.RunSchedule();
      }
    }
  }
}