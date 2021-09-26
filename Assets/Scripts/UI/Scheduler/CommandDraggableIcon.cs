
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using BotScheduler.Gameplay.Schedule;
using UnityEngine;

namespace BotScheduler.UI
{
  [System.Serializable]
  public class CommandSpriteTuple
  {
    public CommandType commandType;
    public Sprite sprite;

    public float rotationCorrection = 0;
  }

  public class CommandDraggableIcon : MonoBehaviour
  {
    private UnityEngine.UI.Image image;

    [SerializeField]
    private CommandSpriteTuple[] spriteTuples = { };

    public void SetCommandType(CommandType commandType)
    {
      image = GetComponent<UnityEngine.UI.Image>();

      foreach (CommandSpriteTuple spriteTuple in spriteTuples)
      {
        if (spriteTuple.commandType == commandType)
        {
          image.sprite = spriteTuple.sprite;
          image.transform.rotation = Quaternion.Euler(0, 0, spriteTuple.rotationCorrection);
          return;
        }
      }

      throw new KeyNotFoundException($"Sprite for command type {commandType} was not found");
    }
  }
}