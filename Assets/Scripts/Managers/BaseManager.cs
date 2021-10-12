using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Schedule;
using BotScheduler.UI;
using UnityEditor;
using UnityEngine;

namespace BotScheduler.Managers
{
  public class BaseManager : MonoBehaviour
  {
    [HideInInspector]
    public LevelConfiguration levelConfiguration;

    protected List<GameObject> players;

    protected void Start()
    {
      players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }

    Transform GetGUIObjectAtPosition<Transform>(Vector2 position)
    {
      return this.GetComponent<Transform>();
    }
  }
}