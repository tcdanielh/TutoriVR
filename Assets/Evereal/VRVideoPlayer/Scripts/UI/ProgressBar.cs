/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class ProgressBar : BarBase
  {
    protected override void OnClick()
    {
      float currentWidth = Vector3.Distance(startPoint.position, currentPoint);
      float progress = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
      videoPlayerCtrl.videoTime = videoPlayerCtrl.videoLength * progress;
    }
  }
}