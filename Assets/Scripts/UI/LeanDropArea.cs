using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using UnityEngine;

namespace BotScheduler.UI
{
  public class LeanDropArea : MonoBehaviour
  {
    void Start()
    {
    }

    public void OnDrop(LeanDrop droppable)
    {
      Debug.Log(droppable.transform.name);
    }
  }
}