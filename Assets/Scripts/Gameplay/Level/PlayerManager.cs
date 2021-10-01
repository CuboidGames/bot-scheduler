using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Schedule;
using BotScheduler.UI;
using UnityEditor;
using UnityEngine;

namespace BotScheduler.Gameplay.Level
{

  [Serializable]
  public class PlayerLevelConfigurator {
    public GameObject player;
    public AvailableLevelCommands commands;
    public Schedule.Schedule schedule;
  }

  public class PlayerManager : MonoBehaviour
  {
    [SerializeField]
    private List<PlayerLevelConfigurator> playerConfigurations = new List<PlayerLevelConfigurator>();

    [SerializeField]
    private GameObject schedulerGUIPrefab;

    [SerializeField]
    private Canvas canvasGui;

    private PlayerSchedulerGUI activeScheduler;
    private List<PlayerSchedulerGUI> schedulers = new List<PlayerSchedulerGUI>();

    void Start()
    {
      InitPlayerSchedulers();
    }

    void Update()
    {

      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // TODO: Use a HashSet to improve lookup times down to O(1)
        if (Physics.Raycast(ray, out var hit))
        {
          foreach (var playerConfiguration in playerConfigurations) {
            if (playerConfiguration.player == hit.transform.gameObject) {
              ActivatePlayerScheduler(playerConfiguration.player);
              break;
            }
          }
        }
      }
    }

    void InitPlayerSchedulers()
    {
      if (playerConfigurations.Count == 0)
      {
        Debug.LogWarning("No player configurations injected in LevelManager!");
        return;
      }

      foreach (var playerConfigurator in playerConfigurations)
      {
        // Init scheduler objects
        var playerScheduler = playerConfigurator.player.GetComponent<PlayerScheduler>();

        playerConfigurator.schedule = new Schedule.Schedule(5);
        playerScheduler.SetSchedule(playerConfigurator.schedule);


        // Init scheduler GUI
        var scheduler = Instantiate(schedulerGUIPrefab, canvasGui.transform);
        var playerSchedulerGUI = scheduler.GetComponent<PlayerSchedulerGUI>();

        playerSchedulerGUI.player = playerConfigurator.player;
        playerSchedulerGUI.CreateScheduleGUI(
          playerConfigurator.schedule,
          playerConfigurator.commands.levelCommands);
        playerSchedulerGUI.gameObject.SetActive(false);

        schedulers.Add(playerSchedulerGUI);
      }
    }

    public void ActivatePlayerScheduler(GameObject player)
    {
      if (activeScheduler)
      {
        activeScheduler.gameObject.SetActive(false);
      }

      foreach (var scheduler in schedulers)
      {
        if (scheduler.player == player)
        {
          scheduler.gameObject.SetActive(true);
          activeScheduler = scheduler;

#if UNITY_EDITOR
          Selection.activeGameObject = player;
#endif
          break;
        }
      }
    }
  }
}