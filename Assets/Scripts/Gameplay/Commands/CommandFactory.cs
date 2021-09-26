
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Gameplay.Schedule;
using UnityEngine;

namespace BotScheduler.Gameplay.Commands
{
  public enum CommandType
  {
    Noop,
    RotateCcw,
    RotateCw,
    MoveForward
  }
  public static class CommandFactory
  {
    static public BaseCommand GetCommand(CommandType commandType)
    {

      if (commandType == CommandType.MoveForward)
      {
        return new MoveCommand(Vector3.forward, 0.25f);
      }

      if (commandType == CommandType.RotateCcw)
      {
        return new RotateCommand(Vector3.up, -Mathf.PI / 2);
      }

      if (commandType == CommandType.RotateCw)
      {
        return new RotateCommand(Vector3.up, Mathf.PI / 2);
      }

      return new NoopCommand();
    }
  }


}