using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotScheduler.CameraControl
{
  public class CameraController : MonoBehaviour
  {
    [SerializeField]
    private Transform transformationPivot;

    private void Awake()
    {
      PanRotateCameraController cameraController;

#if UNITY_IOS || UNITY_ANDROID
      cameraController = gameObject.AddComponent<TouchCameraController>();
#else
      cameraController = gameObject.AddComponent<MouseCameraController>();
#endif

      cameraController.transformationPivot = transformationPivot;
    }
  }
}