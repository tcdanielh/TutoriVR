/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class ProgressBar : BarBase, Runnable
  {
    protected override void OnClick()
    {
      float currentWidth = Vector3.Distance(startPoint.position, currentPoint);
      float progress = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
      videoPlayerCtrl.videoTime = videoPlayerCtrl.videoLength * progress;
    }
        public void run(Vector3 currentPoint)
        {
            float currentWidth = Vector3.Distance(startPoint.position, currentPoint);
            float progress = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
            //             Debug.Log(startPoint.position);
            //  Debug.Log(currentPoint);
            // Debug.Log(currentWidth);
            videoPlayerCtrl.videoTime = videoPlayerCtrl.videoLength * progress;
        }
  }
}