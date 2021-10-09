using System;
using System.Collections;
using System.Collections.Generic;
using BotScheduler.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace BotScheduler.CameraControl
{
  public class MouseCameraController : PanRotateCameraController
  {
    private bool _isPanning = false;
    public bool isPanning
    {
      get
      {
        return _isPanning;
      }
      set
      {
        if (!_isPanning && value)
        {
          initialPanInputPosition = Input.mousePosition;
        }

        _isPanning = value;
      }
    }
    private Vector2 initialPanInputPosition = Vector2.zero;

    private bool _isRotating = false;
    public bool isRotating
    {
      get
      {
        return _isRotating;
      }
      set
      {
        if (!_isRotating && value)
        {
          initialRotateInputPosition = Input.mousePosition;
        }

        _isRotating = value;
      }
    }

    private Vector2 initialRotateInputPosition = Vector2.zero;


    private void Update()
    {
      HandleMouseInput();
    }

    private void HandleMouseInput()
    {
      HandleRotation();
      HandlePan();
    }
    private void HandlePan()
    {
      var isPanningInput = !Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0);

      if (isPanningInput)
      {
        if (!isPanning)
        {
          isPanning = true;
          base.OnPanStart();
        }

        base.OnPan(GetScaledInputPan());
        return;
      }

      if (isPanning)
      {
        isPanning = false;
        base.OnPanEnd();
      }
    }

    private void HandleRotation()
    {
      var isRotationInput = Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0);

      if (isRotationInput)
      {
        if (!isRotating)
        {
          if (GUIUtils.IsGUIObjectAtPosition<Image>(Input.mousePosition))
          {
            return;
          }

          isRotating = true;
          base.OnRotateStart();
        }

        base.OnRotate(GetScaledInputRotation());

        return;
      }

      if (isRotating)
      {
        isRotating = false;
        base.OnRotateEnd();
      }
    }

    private Vector2 GetScaledInputPan()
    {
      return new Vector2(
        initialPanInputPosition.x - Input.mousePosition.x,
        initialPanInputPosition.y - Input.mousePosition.y
      ) / 100f;
    }

    private float GetScaledInputRotation()
    {
      return (Input.mousePosition.x - initialRotateInputPosition.x) / 20f;
    }
  }
}