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

        public async Task RunFullSchedule(Schedule schedule)
        {
            currentSchedule = schedule;

            schedule.Restart();

            while (!schedule.IsLastSchedulable())
            {
                await RunNext();
            }
        }

        public async Task RunNext()
        {
            ISchedulable nextSchedulable = currentSchedule.GetNext();

            if (nextSchedulable == null)
            {
                await Task.Yield();
            }

            await nextSchedulable.Run();
        }
    }

}