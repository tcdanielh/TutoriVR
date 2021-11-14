/* Copyright (c) 2020-present Evereal. All rights reserved. */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Evereal.VRVideoPlayer
{
  // This class will invoke an OnClick event for the hotspot button as soon
	// after the users gazes over it for a defined period of time
  [RequireComponent(typeof(Image))]
  public class ReticleHighlighted : MonoBehaviour
  {
    public static ReticleHighlighted singleton;
    // Reference to the image who's fill amount is adjusted to display the bar.
		// This will likely be attached to our Camera UI
    private Image selectionImage;
    // The time we are waiting to complete the gaze interaction
    public float waitTime = 2f;
    // Used to start and stop the filling coroutine based on input.
    private Coroutine selectionFillRoutine;
    // Used to allow the coroutine to wait for the bar to fill.
    // private bool radialFilled;
    // Whether or not the bar is currently useable.
    private bool isSelectionRadialActive;

    public bool IsSelectionRadialActive
    {
      get
      {
        return isSelectionRadialActive;
      }
    }

    public event Action OnClick;

    public void ClearOnClickEvents()
    {
      OnClick = null;
    }

    public void Hide()
    {
      selectionImage.gameObject.SetActive(false);
      isSelectionRadialActive = false;

      // This effectively resets the radial for when it's shown again.
      selectionImage.fillAmount = 0f;
    }

    public void Show()
    {
      selectionImage.gameObject.SetActive(true);
      isSelectionRadialActive = true;
    }

    public IEnumerator FillSelectionRadial()
    {
      // At the start of the coroutine, the bar is not filled.
      // radialFilled = false;

      // Make sure the radial is visible and usable.
      Show();

      // Create a timer and reset the fill amount.
      float timer = 0f;
      selectionImage.fillAmount = 0f;

      // This loop is executed once per frame until the timer exceeds the duration.
      while (timer < waitTime)
      {
        // The image's fill amount requires a value from 0 to 1 so we normalise the time.
        selectionImage.fillAmount = timer / waitTime;

        // Increase the timer by the time between frames and wait for the next frame.
        timer += Time.deltaTime;
        yield return null;
      }

      // When the loop is finished set the fill amount to be full.
      selectionImage.fillAmount = 1f;

      // Turn off the radial so it can only be used once.
      isSelectionRadialActive = false;

      // The radial is now filled so the coroutine waiting for it can continue.
      // radialFilled = true;

      // call OnClick now that the selection is complete
      if (OnClick != null)
        OnClick();

      // Once it's been used make the radial invisible.
      Hide();
    }

    private void Awake()
    {
      selectionImage = GetComponent<Image>();
      if (singleton != null)
        return;
      singleton = this;
    }

    private void Start()
    {
      // Setup the radial to have no fill at the start and hide if necessary.
      selectionImage.fillAmount = 0f;
      Hide();
    }
  }
}