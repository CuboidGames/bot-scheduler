using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotScheduler.Systems.Schedule;
using UnityEngine;

namespace BotScheduler.Systems.Commands
{
    public abstract class BaseGameObjectCommand : BaseCommand, ISchedulable
    {
        protected GameObject target;

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }
    }
}