using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Systems.Schedule;
using BotScheduler.UI;
using UnityEditor;
using UnityEngine;

namespace BotScheduler.Managers
{
  public class PlayerManager : BaseManager
  {

    private List<Schedule> schedules = new List<Schedule>();

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject scheduleGUIPrefab;

    [SerializeField]
    private GameObject commandsGUIPrefab;

    [SerializeField]
    private Canvas canvasGUI;

    private ScheduleCreatorGUI activeScheduler;
    private CommandsBuffer commandsGUI;
    private List<ScheduleCreatorGUI> schedulerGUIs = new List<ScheduleCreatorGUI>();

    private new void Start()
    {
      base.Start();

      if (!_camera) {
        _camera = Camera.main;
      }

      InitPlayerSchedulers();
      InitSchedulersGUI();
      InitCommandsGUI();
    }

    private void Update()
    {

      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

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
        var schedule = new Schedule(levelConfiguration.queueSize);
        schedules.Add(schedule);

        var playerScheduler = player.GetComponent<PlayerScheduler>();
        playerScheduler.SetSchedule(schedule);
      }
    }

    void InitCommandsGUI()
    {
      if (!commandsGUIPrefab || !canvasGUI)
      {
        Debug.LogWarning("Couldn't initialize commands GUI due to unset commandsGUIPrefab or canvasGUI");
        return;
      }

      // Init scheduler GUI
      var commands = Instantiate(commandsGUIPrefab, canvasGUI.transform);
      var commandsGUI = commands.GetComponent<CommandsBuffer>();

      commandsGUI.CreateCommandDraggables(levelConfiguration.commands);
    }

    void InitSchedulersGUI()
    {
      if (!scheduleGUIPrefab || !canvasGUI)
      {
        Debug.LogWarning("Couldn't initialize schedulers GUI due to unset schedulerGUIPrefab or canvasGUI");
        return;
      }

      for (int i = 0; i < players.Count; i++)
      {
        var player = players[i];
        var schedule = schedules[i];

        // Init scheduler GUI
        var scheduler = Instantiate(scheduleGUIPrefab, canvasGUI.transform);
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