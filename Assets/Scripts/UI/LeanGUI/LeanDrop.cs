using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using UnityEngine;
using UnityEngine.EventSystems;
using BotScheduler.Utils;

namespace BotScheduler.UI
{
  public class LeanDrop : MonoBehaviour
  {
    private LeanDrag drag;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private LeanDropArea currentDropArea;

    [SerializeField]
    private float dropAreaSnapDuration = 0.3f;

    [SerializeField]
    private bool changeParent = false;

    [SerializeField]
    private LeanDropArea dropOutsideFallback;

    private Coroutine currentSnapRoutine;

    protected void Awake()
    {
      mainCamera = mainCamera ?? Camera.main;
      drag = GetComponent<LeanDrag>();
    }

    protected void Start()
    {
      drag.OnEnd.AddListener(OnDragEnd);
    }

    private void SnapInDropArea()
    {
      if (changeParent) {
        transform.SetParent(currentDropArea.transform);
      }

      if (currentSnapRoutine != null) {
        StopCoroutine(currentSnapRoutine);
      }

      currentSnapRoutine = StartCoroutine(SnapInDropAreaCoroutine());
    }

    private IEnumerator SnapInDropAreaCoroutine()
    {
      float initialTime = Time.time;

      Vector3 initialPosition = transform.position;

      while (initialTime + dropAreaSnapDuration > Time.time)
      {
        float interpolateStep = (Time.time - initialTime) / dropAreaSnapDuration;

        transform.position = Vector3.Lerp(initialPosition, currentDropArea.transform.position, Timing.EaseInOut(interpolateStep));
        yield return null;
      }

      transform.position = currentDropArea.transform.position;
    }

    private void OnDragEnd()
    {

      var dropArea = GUIUtils.GetGUIObjectAtPosition<LeanDropArea>(Input.mousePosition);

      if (dropArea) {
        currentDropArea = dropArea;
        dropArea.OnLeanDrop(this);
      }

      // no suitable drop area was found in raycast
      // check if there is a fallback drop area
      if (!dropArea && dropOutsideFallback) {
        currentDropArea = dropOutsideFallback;
      }

      if (currentDropArea) {
        SnapInDropArea();
      }
    }
  }
}