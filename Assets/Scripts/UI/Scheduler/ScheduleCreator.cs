using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.UI
{
  public class ScheduleCreator : MonoBehaviour
  {
    [SerializeField]
    private LeanDropArea dropAreaPrefab;

    [SerializeField]
    private float dropAreaWidth = 120;

    [SerializeField]
    private int commandsCount = 4;

    private void Start()
    {
      for (int i = 0; i < commandsCount; i++)
      {
        float offset = (i - (commandsCount / 2.0f) + 0.5f);
        float targetX = offset * dropAreaWidth;

        var newDropArea = Instantiate<LeanDropArea>(
            dropAreaPrefab,
            transform,
            false);

        newDropArea.transform.localPosition = new Vector3(
            targetX,
            0,
            0
        );
      }
    }
  }

}