
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Gameplay.Schedule;
using UnityEngine;

namespace BotScheduler.UI
{
  enum CommandType {
    Noop,
    RotateCcw,
    RotateCw,
    MoveForward
  }
  public class CommandDrop : LeanDrop
  {
    private BaseCommand _command;
    public BaseCommand command {
      get {
        return _command;
      }
    }

    [SerializeField]
    private CommandType commandType = CommandType.Noop;

    [SerializeField]
    private GameObject player;

    void Start() {
      Init();

      if (commandType == CommandType.MoveForward) {
        _command = new MoveCommand(player, player.transform.forward, 0.25f);
      } else if (commandType == CommandType.RotateCcw) {
        _command = new RotateCommand(player, player.transform.up, -Mathf.PI / 2);
      } else if (commandType == CommandType.RotateCw) {
        _command = new RotateCommand(player, player.transform.up, Mathf.PI / 2);
      } else {
        _command = new NoopCommand();
      }
    }
  }


}