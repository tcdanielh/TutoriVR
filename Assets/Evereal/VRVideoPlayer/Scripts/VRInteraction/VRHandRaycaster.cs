/* Copyright (c) 2017-present Evereal. All rights reserved. */

using System;
using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  // In order to interact with objects in the scene
  // this class casts a ray into the scene and if it finds
  // a VRInteractiveItem it exposes it for other classes to use.
  // This script should be generally be placed on the controller.
  public class VRHandRaycaster : MonoBehaviour
  {
    public GameObject pointer;

    // This event is called every frame that the user's gaze is over a collider.
    public event Action<RaycastHit> OnRaycasthit;

    // Layers to exclude from the raycast.
    [SerializeField] private LayerMask exclusionLayers;
    // How far into the scene the ray is cast.
    [SerializeField] public float rayLength = 500.0f;
    [SerializeField] public float displayRayLength = 50.0f;
    // Ray line renderer.
    private LineRenderer lineRenderer;

    // Reference to the VRInput so when the fire button is pressed it can be handled.
    private VRInput vrInput = null;

    // The current interactive item
    private VRInteractiveItem currentInteractible;
    // The last interactive item
    private VRInteractiveItem lastInteractible;

    // Utility for other classes to get the current interactive item
    public VRInteractiveItem CurrentInteractible
    {
      get { return currentInteractible; }
    }

    private void Start()
    {
      //lineRenderer = GetComponent<LineRenderer>();
      //      Debug.Log("lr");
    }

    private void OnEnable()
    {
      vrInput = VRInput.singleton;
      if (vrInput == null)
      {
        vrInput = gameObject.AddComponent<VRInput>();
      }
      vrInput.OnClick += HandleClick;
    }

    private void OnDisable()
    {
      vrInput.OnClick -= HandleClick;
    }
    private void Update()
    {
      HandRaycast();
    }

    private void HandRaycast()
    {
      // Create a ray that points forwards from the camera.
      Ray ray = new Ray(transform.position, transform.forward);
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

      // Default end position
      Vector3 endPosition = transform.position + (transform.forward * displayRayLength);
      if (currentInteractible != null)
      {
        // Based on hit
        endPosition = hit.point;
      }
      Vector3 halfPosition = transform.position + (transform.forward * displayRayLength / 10);

      // Set position of pointer
      if (pointer != null)
        pointer.transform.position = endPosition;

      // Set line renderer
      //lineRenderer.SetPosition(0, transform.position);
      //lineRenderer.SetPosition(1, halfPosition);
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