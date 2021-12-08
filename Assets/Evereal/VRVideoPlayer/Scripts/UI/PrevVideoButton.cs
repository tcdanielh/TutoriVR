/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class PrevVideoButton : ButtonBase, IRunnable
  {
    protected override void OnClick()
    {
      videoPlayerCtrl.PlayPrevVideo();
    }

        public void Run(Vector3 currentPoint)
        {
            //videoPlayerCtrl.PlayPrevVideo();
            videoPlayerCtrl.SkipToAlert(false);
        }
  }
}