using BotScheduler.Utils;
using UnityEngine;

namespace BotScheduler.CameraControl
{
  public class MouseCameraController : PanRotateCameraController
  {
    private Vector2 initialPanInputPosition = Vector2.zero;
    private Vector2 initialRotateInputPosition = Vector2.zero;

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
      return !Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0);
    }

    protected override bool IsRotatingInput()
    {
      return Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0);
    }

    protected override void SetRotating(bool value)
    {
        if (!isRotating && value)
        {
          initialRotateInputPosition = Input.mousePosition;
        }

        isRotating = value;
    }

    protected override Vector2 GetScaledInputPan()
    {
      return new Vector2(
        initialPanInputPosition.x - Input.mousePosition.x,
        initialPanInputPosition.y - Input.mousePosition.y
      ) / 100f;
    }

    protected override float GetScaledInputRotation()
    {
      return (Input.mousePosition.x - initialRotateInputPosition.x) / 20f;
    }

  }
}