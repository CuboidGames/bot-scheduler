using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Gameplay.Schedule;
using UnityEngine;


namespace BotScheduler.UI
{
  public class PlayerSchedulerGUI : MonoBehaviour
  {
    public GameObject player;
    private CommandsContainer _commandsContainer;
    private CommandsContainer commandsContainer {
      get {
        if (_commandsContainer == null) {
          _commandsContainer = GetComponentInChildren<CommandsContainer>();
        }

        return _commandsContainer;
      }
    }

    private ScheduleCreator _scheduleCreator;
    private ScheduleCreator scheduleCreator {
      get {
        if (_scheduleCreator == null) {
          _scheduleCreator = GetComponentInChildren<ScheduleCreator>();
        }

        return _scheduleCreator;
      }
    }

    public void CreateScheduleGUI(Schedule schedule, List<AvailableLevelCommand> levelCommands) {
      commandsContainer.CreateCommandDraggables(levelCommands);
      scheduleCreator.CreateScheduleSlots(schedule);
    }
  }
}