using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotScheduler.Systems.Schedule;
using BotScheduler.UI;
using Level;
using UnityEditor;
using UnityEngine;

namespace BotScheduler.Managers
{
    public class ScheduleManager : BaseManager
    {

        List<PlayerScheduler> playerSchedulers = new List<PlayerScheduler>();

        protected new void Start()
        {
            base.Start();

            foreach (var player in players)
            {
                playerSchedulers.Add(player.GetComponent<PlayerScheduler>());
            }
        }

        public void PrepareSchedules()
        {
            foreach (var playerScheduler in playerSchedulers)
            {
                playerScheduler.PrepareSchedule();
            }
        }

        public async Task RunNext()
        {
            Task[] tasks = new Task[playerSchedulers.Count];

            for (var i = 0; i < playerSchedulers.Count; i++)
            {
                tasks[i] = playerSchedulers[i].RunNext();
            }

            await Task.WhenAll(tasks);
        }

        public bool HasNextSchedulable()
        {
            var result = playerSchedulers.Exists(playerScheduler => playerScheduler.HasNextSchedulable());

            return !result;
        }

        public async Task RunFullSchedules()
        {
            while (HasNextSchedulable())
            {
                await RunNext();
            }
        }
    }

    [CustomEditor(typeof(ScheduleManager))]
    public class ObjectBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var scheduleManager = (ScheduleManager)target;

            if (GUILayout.Button("Prepare schedules"))
            {
                scheduleManager.PrepareSchedules();
            }

            if (GUILayout.Button("Run next schedules"))
            {
                scheduleManager.RunNext();
            }

            if (GUILayout.Button("Run all schedules"))
            {
                scheduleManager.RunFullSchedules();
            }
        }
    }
}