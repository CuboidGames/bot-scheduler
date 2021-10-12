using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BotScheduler.CameraControl
{
  [RequireComponent(typeof(Camera))]
  public abstract class PanRotateCameraController : MonoBehaviour
  {
    public Transform transformationPivot;

    public Bounds cameraBounds;

    private Camera _camera;

    protected Vector3 initialPanPosition = Vector3.zero;
    private float initialRotation = 0;

    [SerializeField]
    private EventSystem eventSystem;


    protected bool isPanning = false;
    protected bool isRotating = false;

    private void Awake()
    {
      _camera = GetComponent<Camera>();
    }

    private void Start()
    {
      if (!eventSystem)
      {
        eventSystem = EventSystem.current;
      }
    }

    private void Update()
    {
      HandlePan();
      HandleRotation();
    }

    protected abstract bool IsPanningInput();
    protected abstract bool IsRotatingInput();
    protected abstract void SetPanning(bool value);
    protected abstract void SetRotating(bool value);

    protected abstract float GetScaledInputRotation();
    protected abstract Vector2 GetScaledInputPan();

    private void HandlePan()
    {
      if (!isPanning && IsPanningInput())
      {
        if (IsGUIInteraction())
        {
          return;
        }

        SetPanning(true);
        OnPanStart();
      }

      if (isPanning && IsPanningInput())
      {
        OnPan(GetScaledInputPan());
        return;
      }

      if (isPanning && !IsPanningInput())
      {
        SetPanning(false);
        OnPanEnd();
      }
    }

    private void HandleRotation()
    {
      if (!isRotating && IsRotatingInput())
      {
        if (IsGUIInteraction())
        {
          return;
        }

        SetRotating(true);
        OnRotateStart();
      }

      if (isRotating && IsRotatingInput())
      {
        OnRotate(GetScaledInputRotation());
        return;
      }

      if (isRotating && !IsRotatingInput())
      {
        SetRotating(false);
        OnRotateEnd();
      }
    }

    protected void OnPanStart()
    {
      initialPanPosition = transformationPivot.position;
    }
    protected void OnPan(Vector2 offset)
    {
      var xComponent = ProjectVector(_camera.transform.right, offset.x);
      var yComponent = ProjectVector(_camera.transform.forward, offset.y);

      var targetPosition = initialPanPosition + xComponent + yComponent;
      var boundedPosition = GetBoundedPosition(targetPosition);

      transformationPivot.position = boundedPosition;
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

    protected bool IsGUIInteraction()
    {
      return eventSystem.IsPointerOverGameObject();
    }

    protected Vector3 ProjectVector(Vector3 direction, float offset)
    {
      return Vector3.ProjectOnPlane(direction, Vector3.up).normalized * offset;
    }

    private Vector3 GetBoundedPosition(Vector3 position)
    {
      var boundedX = Mathf.Clamp(position.x, cameraBounds.center.x - cameraBounds.extents.x, cameraBounds.center.x + cameraBounds.extents.x);
      var boundedZ = Mathf.Clamp(position.z, cameraBounds.center.z - cameraBounds.extents.z, cameraBounds.center.z + cameraBounds.extents.z);

      return new Vector3(boundedX, 0, boundedZ);
    }

  }
}