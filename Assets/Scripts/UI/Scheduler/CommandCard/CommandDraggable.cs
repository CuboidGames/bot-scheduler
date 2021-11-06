
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Systems.GridSystem;
using BotScheduler.Systems.Commands;
using UnityEngine;

namespace BotScheduler.UI
{
    public class CommandDraggable : LeanDrop
    {
        public BaseCommand command { get; private set; }

        public CommandType commandType { get; private set; }

        public GameObject player;

        public BotScheduler.Systems.GridSystem.Grid<GameObject> grid;

        private CommandIcon icon;

        private new void Awake()
        {
            base.Awake();

            icon = GetComponentInChildren<CommandIcon>();
        }

        private new void Start()
        {
            base.Start();

            icon.SetCommandType(commandType);
        }

        public void SetCommandType(CommandType commandType)
        {
            this.commandType = commandType;

            if (commandType == CommandType.MoveForward)
            {
                var gameObjectCommand = new GridMoveCommand(0, 1);
                gameObjectCommand.SetTarget(player);
                gameObjectCommand.SetGrid(grid);

                command = gameObjectCommand;
            }
            else if (commandType == CommandType.RotateCcw)
            {
                var gameObjectCommand = new RotateCommand(Vector3.up, -Mathf.PI / 2);
                gameObjectCommand.SetTarget(player);

                command = gameObjectCommand;
            }
            else if (commandType == CommandType.RotateCw)
            {
                var gameObjectCommand = new RotateCommand(Vector3.up, Mathf.PI / 2);
                gameObjectCommand.SetTarget(player);

                command = gameObjectCommand;
            }
            else if (commandType == CommandType.GridMove)
            {
                var gameObjectCommand = new GridMoveCommand(1, 1);
                gameObjectCommand.SetTarget(player);
                gameObjectCommand.SetGrid(grid);

                command = gameObjectCommand;
            }
            else
            {
                command = new NoopCommand();
            }
        }
    }
}
