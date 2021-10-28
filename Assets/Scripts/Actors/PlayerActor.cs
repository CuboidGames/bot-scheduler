using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Level;
using BotScheduler.Systems.Schedule;
using UnityEngine;

namespace BotScheduler.Actors {
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(PlayerScheduler))]
    [RequireComponent(typeof(Scheduler))]
    public class PlayerActor : MonoBehaviour {}

}