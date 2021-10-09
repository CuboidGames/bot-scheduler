using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BotScheduler.Utils;
using UnityEngine.UI;

namespace BotScheduler.CameraControl
{
  public class TouchCameraController : PanRotateCameraController
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
          initialRotateTouch1Position = Input.touches[0].position;
          initialRotateTouch2Position = Input.touches[1].position;

        }

        _isRotating = value;
      }
    }

    private Vector2 initialRotateTouch1Position = Vector2.zero;
    private Vector2 initialRotateTouch2Position = Vector2.zero;


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
      var isPanningInput = Input.touches.Length == 1;

      if (isPanningInput)
      {
        if (!isPanning)
        {
          if (GUIUtils.IsGUIObjectAtPosition<Image>(Input.mousePosition))
          {
            return;
          }

          isPanning = true;
          base.OnPanStart();
        }

        var scaledPan = GetScaledInputPan();
        base.OnPan(scaledPan);

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
      var isRotationInput = Input.touches.Length >= 2;

      if (isRotationInput)
      {
        if (!isRotating)
        {
          isRotating = true;
          base.OnRotateStart();
        }

        var scaledRotation = GetScaledInputRotation();

        base.OnRotate(scaledRotation);

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
        initialPanInputPosition.x - Input.touches[0].position.x,
        initialPanInputPosition.y - Input.touches[0].position.y
      ) / 100f;
    }

    private float GetScaledInputRotation()
    {
      var initialVector = initialRotateTouch1Position - initialRotateTouch2Position;
      var currentVector = Input.touches[0].position - Input.touches[1].position;

      return Vector2.SignedAngle(initialVector, currentVector);
    }
  }
}