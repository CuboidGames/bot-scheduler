using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Systems.Schedule;
using BotScheduler.UI;
using UnityEditor;
using UnityEngine;

namespace BotScheduler.Managers
{
  public class BaseManager : MonoBehaviour
  {
    [HideInInspector]
    public LevelConfiguration levelConfiguration;

    public List<GameObject> players { get; private set; }

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