using System.Collections;
using System.Collections.Generic;
using BotScheduler.Systems.Commands;
using UnityEngine;

namespace BotScheduler.UI
{
  public class CommandsBufferGUI : MonoBehaviour
  {
    [SerializeField]
    private CommandDraggable commandDropPrefab;

    [SerializeField]
    private float draggableWidth = 120;

    public void CreateCommandDraggables(List<CommandType> commands)
    {
      for (int i = 0; i < commands.Count; i++)
      {
        float offset = (i - (commands.Count / 2.0f) + 0.5f);
        float targetX = offset * draggableWidth;

        var newCommandDraggable = Instantiate<CommandDraggable>(
            commandDropPrefab,
            transform,
            false);

        newCommandDraggable.transform.localPosition = new Vector3(
            targetX,
            100,
            0
        );

        newCommandDraggable.SetCommandType(commands[i]);
      }
    }
  }
}