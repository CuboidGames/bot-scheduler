using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Gameplay.Schedule;
using UnityEditor;
using UnityEngine;


namespace BotScheduler.UI
{
  public class PlayerScheduler : MonoBehaviour
  {

    [SerializeField]
    public GameObject player;


    // command creation
    private int commandsCount = 4;

    private Schedule schedule;
    private Scheduler scheduler;

    private CommandsContainer commandsContainer;
    private ScheduleCreator scheduleCreator;

    void Start()
    {
      // TODO: move schedule creation to level manager
      schedule = new Schedule(commandsCount);

      var c1 = ScriptableObject.CreateInstance<AvailableLevelCommand>();
      c1.commandType = CommandType.MoveForward;
      c1.count = 1;

      var c2 = ScriptableObject.CreateInstance<AvailableLevelCommand>();
      c2.commandType = CommandType.RotateCcw;
      c2.count = 1;

      var c3 = ScriptableObject.CreateInstance<AvailableLevelCommand>();
      c3.commandType = CommandType.RotateCw;
      c3.count = 1;

      var c4 = ScriptableObject.CreateInstance<AvailableLevelCommand>();
      c4.commandType = CommandType.Noop;
      c4.count = 1;

      var availableLevelCommands = ScriptableObject.CreateInstance<AvailableLevelCommands>();
      availableLevelCommands.levelCommands.Add(c1);
      availableLevelCommands.levelCommands.Add(c2);
      availableLevelCommands.levelCommands.Add(c3);
      availableLevelCommands.levelCommands.Add(c4);

      scheduler = GetComponentInChildren<Scheduler>();
      commandsContainer = GetComponentInChildren<CommandsContainer>();
      scheduleCreator = GetComponentInChildren<ScheduleCreator>();

      scheduleCreator.player = player;

      commandsContainer.CreateCommandDraggables(availableLevelCommands.levelCommands);
      scheduleCreator.CreateScheduleSlots(schedule);
    }

    public void RunSchedule()
    {
      foreach (BaseCommand command in schedule.commands) {
          if (command is BaseGameObjectCommand) {
              ((BaseGameObjectCommand) command).target = player;
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