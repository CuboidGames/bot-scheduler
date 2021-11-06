using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;


namespace BotScheduler.Systems.Schedule
{
    public class PlayerScheduler : MonoBehaviour
    {
        private Schedule schedule;
        private Scheduler scheduler;

        private void Awake()
        {
            scheduler = GetComponent<Scheduler>();
        }

        public void SetSchedule(Schedule newSchedule)
        {
            schedule = newSchedule;
        }

        public void PrepareSchedule()
        {
            scheduler.PrepareSchedule(schedule);
        }

        public bool HasNextSchedulable() {
            return scheduler.HasNextSchedulable();
        }

        public async Task RunNext()
        {
            await scheduler.RunNext();
        }

        public async void RunSchedule()
        {
            await scheduler.RunFullSchedule(schedule);
        }
    }

    [CustomEditor(typeof(PlayerScheduler))]
    public class ObjectBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var scheduler = (PlayerScheduler)target;

            if (GUILayout.Button("Run schedule"))
            {
                scheduler.RunSchedule();
            }
        }
    }
}