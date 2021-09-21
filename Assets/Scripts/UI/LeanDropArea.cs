using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using BotScheduler.UI;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LeanDropEvent : UnityEvent<LeanDrop>
{
}

namespace BotScheduler.UI
{

  public class LeanDropArea : MonoBehaviour
  {
    private UnityEvent<LeanDrop> OnDrop
    {
      get
      {
        if (onDrop == null)
        {
          onDrop = new LeanDropEvent();
        }

        return onDrop;
      }
    }

    [SerializeField]
    private LeanDropEvent onDrop;

    [SerializeField]
    private bool validateTags = false;

    [SerializeField]
    private List<string> validTags = new List<string>();

    public void OnLeanDrop(LeanDrop droppable)
    {
      OnDrop.Invoke(droppable);
    }

    public bool AcceptsDroppable(LeanDrop droppable) {
      if (!validateTags) {
        return true;
      }

      foreach (var tag in validTags) {
        if (droppable.CompareTag(tag)) {
          return true;
        }
      }

      return false;
    }
  }
}