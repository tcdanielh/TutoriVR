/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class VolumeBar : BarBase, IRunnable
  {
    protected override void OnClick()
    {
      float currentWidth = Vector3.Distance(startPoint.position, currentPoint);
      float volume = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
			videoPlayerCtrl.SetAudioVolume(volume);
    }

        public void Run(Vector3 ccc)
        {
            float currentWidth = Vector3.Distance(startPoint.position, ccc);
            // Debug.Log(startPoint.position);
            //  Debug.Log(ccc);
            // Debug.Log(currentWidth);
            float volume = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
            // Debug.Log(volume);
            videoPlayerCtrl.SetAudioVolume(volume);
        }
  }
}