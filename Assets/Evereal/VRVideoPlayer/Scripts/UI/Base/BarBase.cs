/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;
using UnityEngine.UI;

namespace Evereal.VRVideoPlayer
{
  [RequireComponent(typeof(VRInteractiveItem))]
  [RequireComponent(typeof(BoxCollider))]
  [RequireComponent(typeof(Image))]
  public class BarBase : MonoBehaviour
  {
    public VideoPlayerCtrl videoPlayerCtrl;
    public Transform startPoint;
    public Transform endPoint;

    protected Image progressBarImg;
    protected float progressBarWidth;

    private VRInteractiveItem interactiveItem;

    private Coroutine selectionFillRoutine;

    protected Vector3 currentPoint;

    private const string LOG_FORMAT = "[BarBase] {0}";

    public void SetProgress(float progress)
    {
      if (progressBarImg == null)
        progressBarImg = GetComponent<Image>();
      progressBarImg.fillAmount = progress;
    }

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

        currentPoint = point;
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

    private void Awake()
    {
      if (videoPlayerCtrl == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "VideoPlayerCtrl not attached!");
      }
      if (startPoint == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "StartPoint not attached!");
      }
      if (endPoint == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "EndPoint not attached!");
      }
      interactiveItem = GetComponent<VRInteractiveItem>();
      progressBarImg = GetComponent<Image>();
      progressBarWidth = Vector3.Distance(startPoint.position, endPoint.position);
    }

    private void OnEnable()
    {
      interactiveItem.OnOver += HandleOver;
      interactiveItem.OnOut += HandleOut;
    }

    private void OnDisable()
    {
      interactiveItem.OnOver -= HandleOver;
      interactiveItem.OnOut -= HandleOut;
    }
  }
}