/* Copyright (c) 2017-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class AttachMouseLook : MonoBehaviour
  {
    public GameObject controlObject;

    public SmoothMouseLook.RotationAxes axes = SmoothMouseLook.RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    private const string LOG_FORMAT = "[AttachMouseLook] {0}";

    void Start()
    {
      // add mouse movement control if in editor mode
      if (Application.isEditor)
      {
        Debug.LogFormat(LOG_FORMAT, "Editor mode detected, attach SmoothMouseLook script for control.");
        SmoothMouseLook sml = controlObject.AddComponent<SmoothMouseLook>();

        sml.sensitivityX = sensitivityX;
        sml.sensitivityY = sensitivityY;
        sml.minimumX = minimumX;
        sml.maximumX = maximumX;
        sml.minimumY = minimumY;
        sml.maximumY = maximumY;
      }
    }
  }
}