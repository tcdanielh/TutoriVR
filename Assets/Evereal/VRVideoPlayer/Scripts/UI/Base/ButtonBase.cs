/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  [RequireComponent(typeof(VRInteractiveItem))]
  [RequireComponent(typeof(BoxCollider))]
  public class ButtonBase : MonoBehaviour
  {
    // Reference to video player ctrl.
    public VideoPlayerCtrl videoPlayerCtrl;
    // The interactable object for where the user should look to cause on "onClick" event.
    private VRInteractiveItem interactiveItem;
    // Used to start and stop the filling coroutine based on input.
    private Coroutine selectionFillRoutine;

    protected const string LOG_FORMAT = "[ButtonBase] {0}";

    protected virtual void OnClick()
    {
    }

    private void HandleOver(Vector3 point)
    {
      if (ReticleHighlighted.singleton)
      {
        ReticleHighlighted.singleton.ClearOnClickEvents();
        ReticleHighlighted.singleton.OnClick += OnClick;

        selectionFillRoutine = StartCoroutine(ReticleHighlighted.singleton.FillSelectionRadial());
      }
    }

    private void HandleOut()
    {
      // If the radial is active stop filling it and reset it's amount.
      if (ReticleHighlighted.singleton)
      {
        ReticleHighlighted.singleton.OnClick -= OnClick;

        if (ReticleHighlighted.singleton.IsSelectionRadialActive)
        {
          if (selectionFillRoutine != null)
            StopCoroutine(selectionFillRoutine);

          ReticleHighlighted.singleton.Hide();
        }
      }
    }

    private void HandleClick()
    {
      OnClick();
    }

    private void Awake()
    {
      // Reference to VRInteractiveItem Component
      interactiveItem = GetComponent<VRInteractiveItem>();
      if (videoPlayerCtrl == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "VideoPlayerCtrl not attached!");
      }
    }

    private void OnEnable()
    {
      interactiveItem.OnOver += HandleOver;
      interactiveItem.OnOut += HandleOut;
      interactiveItem.OnClick += HandleClick;
    }

    private void OnDisable()
    {
      interactiveItem.OnOver -= HandleOver;
      interactiveItem.OnOut -= HandleOut;
      interactiveItem.OnClick -= HandleClick;
    }
  }
}