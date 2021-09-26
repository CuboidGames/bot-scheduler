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
    private Camera mainCamera;

    [SerializeField]
    private LeanDropArea currentDropArea;

    [SerializeField]
    private float dropAreaSnapDuration = 0.3f;

    [SerializeField]
    private LeanDropArea dropOutsideFallback;

    private Coroutine currentSnapRoutine;

    void Start()
    {
      Init();
    }

    protected void Init() {
      mainCamera = Camera.main;
      drag = GetComponent<LeanDrag>();

      drag.OnEnd.AddListener(OnDragEnd);
    }

    private void SnapInDropArea() {
      if (currentSnapRoutine != null) {
        StopCoroutine(currentSnapRoutine);
      }

      currentSnapRoutine = StartCoroutine(StartSnapRoutine());
    }

    private IEnumerator StartSnapRoutine() {
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
      var eventSystem = EventSystem.current;

      if (eventSystem == null)
      {
        return;
      }

      var pointerEventData = new PointerEventData(eventSystem);
      List<RaycastResult> hits = new List<RaycastResult>();

      pointerEventData.position = Input.mousePosition;

      eventSystem.RaycastAll(pointerEventData, hits);

      bool dropAreaFound = false;

      foreach (RaycastResult hit in hits) {
        if (!hit.gameObject.TryGetComponent<LeanDropArea>(out var dropArea)) {
          continue;
        }

        if (!dropArea.AcceptsDroppable(this)) {
          continue;
        }

        dropAreaFound = true;
        currentDropArea = dropArea;

        dropArea.OnLeanDrop(this);
        break;
      }

      // loop did not break, so no suitable drop area was found in raycast
      // check if there is a fallback drop area
      if (!dropAreaFound && dropOutsideFallback) {
        currentDropArea = dropOutsideFallback;
      }

      if (currentDropArea) {
        SnapInDropArea();
      }
    }
  }
}