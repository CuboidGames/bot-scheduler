using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotScheduler.Systems.Schedule;
using UnityEngine;

namespace BotScheduler.Systems.Commands
{
    public abstract class BaseCommand : ISchedulable
    {

        protected delegate void RunInterpolatedCallback(float delta);

        abstract public Task Run();


        protected async Task RunInterpolated(float duration, RunInterpolatedCallback func)
        {
            float initialTime = Time.time;

            func(0);

            while (initialTime + duration > Time.time)
            {
                func((Time.time - initialTime) / duration);

                await Task.Yield();
            }

            func(1);
        }

    }
}