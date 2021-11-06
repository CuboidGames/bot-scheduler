using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BotScheduler.Systems.Schedule
{
    public class Scheduler : MonoBehaviour
    {
        private Schedule currentSchedule;

        public void PrepareSchedule(Schedule schedule) {
            currentSchedule = schedule;
            currentSchedule.Restart();
        }

        public bool HasNextSchedulable() {
            return currentSchedule.HasNextSchedulable();
        }

        public async Task RunFullSchedule(Schedule schedule)
        {
            PrepareSchedule(schedule);

            while (!HasNextSchedulable())
            {
                await RunNext();
            }
        }

        public async Task RunNext()
        {
            ISchedulable nextSchedulable = currentSchedule.GetNext();

            if (nextSchedulable != null)
            {
                await nextSchedulable.Run();
            }

            await Task.Yield();
        }
    }

}