/* Copyright (c) 2017-present Evereal. All rights reserved. */

using System;
using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  // In order to interact with objects in the scene
  // this class casts a ray into the scene and if it finds
  // a VRInteractiveItem it exposes it for other classes to use.
  // This script should be generally be placed on the camera.
  public class VREyeRaycaster : MonoBehaviour
  {
    // This event is called every frame that the user's gaze is over a collider.
    public event Action<RaycastHit> OnRaycasthit;

    private Transform cameraTransform;
    // Layers to exclude from the raycast.
    [SerializeField] private LayerMask exclusionLayers;
    // How far into the scene the ray is cast.
    [SerializeField] private float rayLength = 500f;
    // Optionally show the debug ray.
    [SerializeField] private bool showDebugRay;
    // Debug ray length.
    [SerializeField] private float debugRayLength = 5f;
    // How long the Debug ray will remain visible.
    [SerializeField] private float debugRayDuration = 1f;

    // The current interactive item
    private VRInteractiveItem currentInteractible;
    // The last interactive item
    private VRInteractiveItem lastInteractible;

    // Utility for other classes to get the current interactive item
    public VRInteractiveItem CurrentInteractible
    {
      get { return currentInteractible; }
    }

    private void Awake()
    {
      cameraTransform = GetComponent<Camera>().transform;
    }

    private void Update()
    {
      EyeRaycast();
    }

    private void EyeRaycast()
    {
      // Show the debug ray if required
      if (showDebugRay)
      {
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * debugRayLength, Color.blue, debugRayDuration);
      }

      // Create a ray that points forwards from the camera.
      Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
      RaycastHit hit;

      // Do the raycast forweards to see if we hit an interactive item
      if (Physics.Raycast(ray, out hit, rayLength, ~exclusionLayers))
      {
        // attempt to get the VRInteractiveItem on the hit object
        VRInteractiveItem interactible = hit.collider.GetComponent<VRInteractiveItem>();
        currentInteractible = interactible;

        // If we hit an interactive item and it's not the same as the last interactive item, then call Over
        if (interactible && interactible != lastInteractible)
          interactible.Over(hit.point);

        // Deactive the last interactive item
        if (interactible != lastInteractible)
          DeactiveLastInteractible();

        lastInteractible = interactible;

        if (OnRaycasthit != null)
          OnRaycasthit(hit);
      }
      else
      {
        // Nothing was hit, deactive the last interactive item.
        DeactiveLastInteractible();
        currentInteractible = null;
      }
    }

    private void DeactiveLastInteractible()
    {
      if (lastInteractible == null)
        return;

      lastInteractible.Out();
      lastInteractible = null;
    }

    private void HandleUp()
    {
      if (currentInteractible != null)
        currentInteractible.Up();
    }

    private void HandleDown()
    {
      if (currentInteractible != null)
        currentInteractible.Down();
    }

    private void HandleClick()
    {
      if (currentInteractible != null)
        currentInteractible.Click();
    }

    private void HandleDoubleClick()
    {
      if (currentInteractible != null)
        currentInteractible.DoubleClick();
    }
  }
}