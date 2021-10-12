using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.CameraControl
{
  public class AbstractCameraController : MonoBehaviour
  {
    [SerializeField]
    private Transform transformationPivot;

    [SerializeField]
    private Bounds cameraBounds;

    private void Awake()
    {
      PanRotateCameraController cameraController;

#if UNITY_IOS || UNITY_ANDROID
      cameraController = gameObject.AddComponent<TouchCameraController>();
#else
      cameraController = gameObject.AddComponent<MouseCameraController>();
#endif

      cameraController.transformationPivot = transformationPivot;
      cameraController.cameraBounds = cameraBounds;
    }

    public void OnDrawGizmos()
    {
      Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
      Gizmos.DrawWireCube(cameraBounds.center, cameraBounds.size);
    }
  }
}