using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Commands;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Coroutine currentAction;

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      RunCommand((target) => new MoveCommand(target, target.transform.forward, 0.23f));
    }

    if (Input.GetMouseButtonDown(1))
    {
      RunCommand((target) => new RotateCommand(target, target.transform.up, Mathf.PI / 2));
    }
  }

  private void RunCommand(Func<GameObject, BaseCommand> constructor)
  {
    if (currentAction != null)
    {
      StopCoroutine(currentAction);
    }

    var command = constructor(gameObject);

    currentAction = StartCoroutine(command.Run());

  }
}
