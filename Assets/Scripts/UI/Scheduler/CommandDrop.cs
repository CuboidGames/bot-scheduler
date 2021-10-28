
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Systems.Commands;
using UnityEngine;

namespace BotScheduler.UI
{
    public class CommandDrop : LeanDrop
    {
        public BaseCommand command { get; private set; }

        public CommandType commandType { get; private set; }

        public GameObject player;

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

            command = CommandFactory.GetCommand(commandType, player);
        }

        public void SetCommandType(CommandType commandType)
        {
            this.commandType = commandType;
        }
    }
}