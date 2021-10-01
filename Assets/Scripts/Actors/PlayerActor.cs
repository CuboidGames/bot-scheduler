using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Level;
using BotScheduler.Gameplay.Schedule;
using UnityEngine;

namespace BotScheduler.Actors {
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(PlayerScheduler))]
    [RequireComponent(typeof(Scheduler))]
    public class PlayerActor : MonoBehaviour {}

}