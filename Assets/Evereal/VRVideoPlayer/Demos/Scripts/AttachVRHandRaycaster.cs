/* Copyright (c) 2017-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class AttachVRHandRaycaster : MonoBehaviour
  {
    public GameObject controlObject;
    public GameObject vrHandRaycasterPrefab;

    private const string LOG_FORMAT = "[AttachVRHandRaycaster] {0}";

    void Start()
    {
      // add mouse movement control if in editor mode
      if (Application.isEditor)
      {
        Debug.LogFormat(LOG_FORMAT, "Editor mode detected, attach VRHandRaycaster script for interaction.");
        GameObject raycaster = Instantiate(
          vrHandRaycasterPrefab,
          new Vector3(
            controlObject.transform.position.x,
            controlObject.transform.position.y - 3.0f,
            controlObject.transform.position.z + 1.0f
          ),
          Quaternion.identity
        );

        SmoothMouseLook sml = raycaster.AddComponent<SmoothMouseLook>();

        sml.sensitivityX = 15F;
        sml.sensitivityY = 15F;
        sml.minimumX = -120F;
        sml.maximumX = 120F;
        sml.minimumY = -60F;
        sml.maximumY = 60F;
      }
    }
  }
}