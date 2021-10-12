using UnityEngine;
using BotScheduler.Utils;

namespace BotScheduler.CameraControl
{
  public class TouchCameraController : PanRotateCameraController
  {
    private Vector2 initialPanInputPosition = Vector2.zero;
    private Vector2 initialRotateTouch1Position = Vector2.zero;
    private Vector2 initialRotateTouch2Position = Vector2.zero;

    protected override void SetPanning(bool value)
    {
      if (!isPanning && value)
      {
        initialPanInputPosition = Input.mousePosition;
      }

      isPanning = value;
    }

    protected override bool IsPanningInput()
    {
      return Input.touches.Length == 1;
    }

    protected override bool IsRotatingInput()
    {
      return Input.touches.Length >= 2;
    }

    protected override void SetRotating(bool value)
    {
      if (!isRotating && value)
      {
        initialRotateTouch1Position = Input.touches[0].position;
        initialRotateTouch2Position = Input.touches[1].position;

      }

      isRotating = value;
    }

    protected override Vector2 GetScaledInputPan()
    {
      return new Vector2(
        initialPanInputPosition.x - Input.touches[0].position.x,
        initialPanInputPosition.y - Input.touches[0].position.y
      ) / 100f;
    }

    protected override float GetScaledInputRotation()
    {
      var initialVector = initialRotateTouch1Position - initialRotateTouch2Position;
      var currentVector = Input.touches[0].position - Input.touches[1].position;

      return Vector2.SignedAngle(initialVector, currentVector);
    }


  }
}