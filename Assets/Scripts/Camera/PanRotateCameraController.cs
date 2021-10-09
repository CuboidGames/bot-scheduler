using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.CameraControl
{
  [RequireComponent(typeof(Camera))]
  public class PanRotateCameraController : MonoBehaviour
  {
    public Transform transformationPivot;

    protected Camera _camera { get; private set; }

    private Vector3 initialPanPosition = Vector3.zero;
    private float initialRotation = 0;

    private void Awake()
    {
      _camera = GetComponent<Camera>();
    }

    protected void OnPanStart()
    {
      initialPanPosition = transformationPivot.position;
    }
    protected void OnPan(Vector2 offset)
    {
      var xComponent = ProjectVector(_camera.transform.right, offset.x);
      var yComponent = ProjectVector(_camera.transform.forward, offset.y);

      transformationPivot.position = initialPanPosition + xComponent + yComponent;
    }
    protected void OnPanEnd()
    {
      initialPanPosition = Vector2.zero;
    }

    protected void OnRotateStart()
    {
      initialRotation = transformationPivot.rotation.eulerAngles.y;
    }
    protected void OnRotate(float angle)
    {
      transformationPivot.rotation = Quaternion.Euler(0, initialRotation + angle, 0);
    }
    protected void OnRotateEnd()
    {
      initialRotation = 0;
    }

    protected Vector3 ProjectVector(Vector3 direction, float offset)
    {
      return Vector3.ProjectOnPlane(direction, Vector3.up).normalized * offset;
    }
  }
}