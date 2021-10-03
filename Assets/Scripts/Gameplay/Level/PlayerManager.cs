using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Schedule;
using BotScheduler.UI;
using UnityEditor;
using UnityEngine;

namespace BotScheduler.Gameplay.Level
{


  public class PlayerManager : MonoBehaviour
  {
    [SerializeField]
    private LevelManager levelManager;


    [SerializeField]
    private List<GameObject> players;

    private List<Schedule.Schedule> schedules = new List<Schedule.Schedule>();

    [SerializeField]
    private GameObject scheduleGUIPrefab;

    [SerializeField]
    private GameObject commandsGUIPrefab;

    [SerializeField]
    private Canvas canvasGui;

    private ScheduleCreatorGUI activeScheduler;
    private CommandsContainerGUI commandsGUI;
    private List<ScheduleCreatorGUI> schedulerGUIs = new List<ScheduleCreatorGUI>();

    void Start()
    {
      if (players.Count == 0)
      {
        Debug.LogWarning("No players injected in LevelManager!");
        return;
      }

      InitPlayerSchedulers();
      InitSchedulersGUI();
      InitCommandsGUI();
    }

    void Update()
    {

      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // TODO: Use a HashSet to improve lookup times down to O(1)
        if (Physics.Raycast(ray, out var hit) && players.Contains(hit.transform.gameObject))
        {
          ActivatePlayerScheduler(hit.transform.gameObject);
        }
      }
    }

    void InitPlayerSchedulers()
    {
      foreach (var player in players)
      {
        // Init scheduler objects
        var schedule = new Schedule.Schedule(levelManager.levelConfiguration.queueSize);
        schedules.Add(schedule);

        var playerScheduler = player.GetComponent<PlayerScheduler>();
        playerScheduler.SetSchedule(schedule);
      }
    }

    public void InitCommandsGUI() {
        // Init scheduler GUI
        var commands = Instantiate(commandsGUIPrefab, canvasGui.transform);
        var commandsGUI = commands.GetComponent<CommandsContainerGUI>();

        commandsGUI.CreateCommandDraggables(levelManager.levelConfiguration.commands);
    }

    public void InitSchedulersGUI() {
      for (int i = 0; i < players.Count; i++)
      {
        var player = players[i];
        var schedule = schedules[i];

        // Init scheduler GUI
        var scheduler = Instantiate(scheduleGUIPrefab, canvasGui.transform);
        var playerScheduleGUI = scheduler.GetComponent<ScheduleCreatorGUI>();

        playerScheduleGUI.player = player;
        playerScheduleGUI.CreateScheduleSlots(schedule);
        playerScheduleGUI.gameObject.SetActive(false);

        schedulerGUIs.Add(playerScheduleGUI);
      }
    }

    public void ActivatePlayerScheduler(GameObject player)
    {
      if (activeScheduler)
      {
        activeScheduler.gameObject.SetActive(false);
      }

      foreach (var scheduler in schedulerGUIs)
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