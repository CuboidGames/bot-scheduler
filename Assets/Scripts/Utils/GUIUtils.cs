using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace BotScheduler.Utils
{
  public class GUIUtils
  {
    static public bool IsGUIObjectAtPosition<T>(Vector2 screenPosition) where T : MonoBehaviour
    {
      return GetGUIObjectAtPosition<T>(screenPosition) != null;
    }

    static public T GetGUIObjectAtPosition<T>(Vector2 screenPosition) where T : MonoBehaviour
    {
      var eventSystem = EventSystem.current;

      if (eventSystem == null)
      {
        Debug.LogWarning("Tried to raycast for GUI items but there is no active event system");
        return null;
      }

      var pointerEventData = new PointerEventData(eventSystem);
      List<RaycastResult> hits = new List<RaycastResult>();

      pointerEventData.position = screenPosition;
      eventSystem.RaycastAll(pointerEventData, hits);

      foreach (RaycastResult hit in hits)
      {
        if (hit.gameObject.TryGetComponent<T>(out var component))
        {
          return component;
        }
      }

      return null;
    }
  }
}