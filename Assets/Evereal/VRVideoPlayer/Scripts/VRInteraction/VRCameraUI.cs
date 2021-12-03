/* Copyright (c) 2017-present Evereal. All rights reserved. */

using System;
using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  // This class ensures that the UI (such as the reticle and selection bar)
  // are set up correctly.
  public class VRCameraUI : MonoBehaviour
  {
    // Reference to the canvas containing the UI.
    [SerializeField] private Canvas uiCanvas;

    private void Awake()
    {
      // Make sure the canvas is on.
      uiCanvas.enabled = true;

      // Set its sorting order to the front.
      uiCanvas.sortingOrder = Int16.MaxValue;

      // Force the canvas to redraw so that it is correct before the first render.
      Canvas.ForceUpdateCanvases();
    }
  }
}