
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Systems.Commands;
using BotScheduler.Systems.Schedule;
using UnityEngine;

namespace BotScheduler.Systems.Commands
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
        static public BaseCommand GetCommand(CommandType commandType, GameObject target)
        {

            if (commandType == CommandType.MoveForward)
            {
                var command = new MoveCommand(Vector3.forward, 0.25f);
                command.SetTarget(target);

                return command;
            }

            if (commandType == CommandType.RotateCcw)
            {
                var command = new RotateCommand(Vector3.up, -Mathf.PI / 2);
                command.SetTarget(target);

                return command;
            }

            if (commandType == CommandType.RotateCw)
            {
                var command = new RotateCommand(Vector3.up, Mathf.PI / 2);
                command.SetTarget(target);

                return command;
            }

            return new NoopCommand();
        }
    }


}