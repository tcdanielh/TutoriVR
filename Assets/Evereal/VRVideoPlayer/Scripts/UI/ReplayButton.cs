/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class ReplayButton : ButtonBase, Runnable
  {
    protected override void OnClick()
    {
      videoPlayerCtrl.ReplayVideo();
    }

        public void run(Vector3 currentPoint)
        {
            videoPlayerCtrl.ReplayVideo();
        }
  }
}