﻿/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class VolumeBar : BarBase, Runnable
  {
    protected override void OnClick()
    {
      float currentWidth = Vector3.Distance(startPoint.position, currentPoint);
      float volume = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
			videoPlayerCtrl.SetAudioVolume(volume);
    }

        public void run(Vector3 currentpoint)
        {
            float currentWidth = Vector3.Distance(startPoint.position, currentPoint);
            float volume = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
            videoPlayerCtrl.SetAudioVolume(volume);
        }
  }
}