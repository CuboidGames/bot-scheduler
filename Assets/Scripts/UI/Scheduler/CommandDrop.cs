
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Gameplay.Schedule;
using UnityEngine;

namespace BotScheduler.UI
{
  public class CommandDrop : LeanDrop
  {
    private BaseCommand _command;
    public BaseCommand command
    {
      get
      {
        return _command;
      }
    }

    private CommandType _commandType = CommandType.Noop;

    public CommandType commandType {
      get {
        return _commandType;
      }
      set {
        _commandType = value;
      }
    }

    public GameObject player;

    private CommandDraggableIcon icon;

    void Start()
    {
      Init();

      icon = GetComponentInChildren<CommandDraggableIcon>();

      icon.SetCommandType(commandType);
      _command = CommandFactory.GetCommand(commandType);
    }
  }


}