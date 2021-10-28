using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Systems.Commands;
using UnityEngine;

namespace BotScheduler.UI
{
  public class CommandsBuffer : MonoBehaviour
  {
    [SerializeField]
    private CommandDrop commandDropPrefab;

    [SerializeField]
    private float draggableWidth = 120;

    public void CreateCommandDraggables(List<CommandType> commands)
    {
      for (int i = 0; i < commands.Count; i++)
      {
        float offset = (i - (commands.Count / 2.0f) + 0.5f);
        float targetX = offset * draggableWidth;

        var newCommandDraggable = Instantiate<CommandDrop>(
            commandDropPrefab,
            transform,
            false);

        newCommandDraggable.transform.localPosition = new Vector3(
            targetX,
            0,
            0
        );

        newCommandDraggable.SetCommandType(commands[i]);
      }
    }
  }
}