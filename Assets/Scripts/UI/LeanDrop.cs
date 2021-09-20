using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BotScheduler.UI
{
  public class LeanDrop : MonoBehaviour
  {
    private LeanDrag drag;
    private Camera mainCamera;

    void Start()
    {
      mainCamera = Camera.main;
      drag = GetComponent<LeanDrag>();

      drag.OnEnd.AddListener(OnDragEnd);
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

      foreach (RaycastResult hit in hits) {
        if (!hit.gameObject.TryGetComponent<LeanDropArea>(out var dropArea)) {
          continue;
        }

        dropArea.OnDrop(this);
        break;
      }
    }
  }
}