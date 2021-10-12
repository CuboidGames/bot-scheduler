using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using Lean.Gui;

namespace BotScheduler.Utils
{
  public class GUIUtils
  {
    static private List<RaycastResult> RaycastGUIObjects(Vector2 screenPosition)
    {
      return LeanGui.RaycastGui(Input.mousePosition, ~LayerMask.NameToLayer("UI"));
    }

    static private bool RaycastGUIObject(Vector2 screenPosition, out RaycastResult hitResult)
    {
      var hitResults = GUIUtils.RaycastGUIObjects(screenPosition);
      var hasHit = hitResults.Count > 0;

      if (hasHit)
      {
        hitResult = hitResults[0];
      }
      else
      {
        hitResult = new RaycastResult();
      }

      return hasHit;
    }


    static public GameObject GetGUIObjectAtPosition(Vector2 screenPosition)
    {
      if (GUIUtils.RaycastGUIObject(screenPosition, out var hit))
      {
        return hit.gameObject;
      }

      return null;
    }

    static public T GetGUIObjectAtPosition<T>(Vector2 screenPosition) where T : MonoBehaviour
    {
      var hits = GUIUtils.RaycastGUIObjects(screenPosition);

      foreach (RaycastResult hitResult in hits)
      {
        if (hitResult.gameObject.TryGetComponent<T>(out var component))
        {
          return component;
        }
      }

      return null;
    }

    static public bool IsGUIObjectAtPosition(Vector2 screenPosition)
    {
      return GetGUIObjectAtPosition(screenPosition) != null;
    }

    static public bool IsGUIObjectAtPosition<T>(Vector2 screenPosition) where T : MonoBehaviour
    {
      return GetGUIObjectAtPosition<T>(screenPosition) != null;
    }

    static public GameObject[] GetGUIObjectsAtPosition(Vector2 screenPosition)
    {
      var hits = GUIUtils.RaycastGUIObjects(screenPosition);
      var gameObjects = new List<GameObject>();

      foreach (var hitResult in hits)
      {
        gameObjects.Add(hitResult.gameObject);
      }

      return gameObjects.ToArray();
    }
  }
}