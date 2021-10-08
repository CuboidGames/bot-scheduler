using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Gameplay.Schedule;
using UnityEditor;
using UnityEngine;

namespace BotScheduler.UI
{
  public class ScheduleCreatorGUI : MonoBehaviour
  {
    [SerializeField]
    private CommandDropArea dropAreaPrefab;

    [SerializeField]
    private float dropAreaWidth = 120;

    public GameObject player;

    public void CreateScheduleSlots(Schedule schedule)
    {
      for (int i = 0; i < schedule.size; i++)
      {
        float offset = (i - (schedule.size / 2.0f) + 0.5f);
        float targetX = offset * dropAreaWidth;

        var newDropArea = Instantiate<CommandDropArea>(
            dropAreaPrefab,
            transform,
            false);

        newDropArea.index = i;
        newDropArea.schedule = schedule;

        newDropArea.transform.localPosition = new Vector3(
            targetX,
            0,
            0
        );
      }
    }
  }
}