using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotScheduler.Systems.Schedule;
using UnityEngine;

namespace BotScheduler.Systems.Commands
{
    public class NoopCommand : BaseCommand, ISchedulable
    {
        public override async Task Run()
        {
            await Task.Yield();
        }
    }
}