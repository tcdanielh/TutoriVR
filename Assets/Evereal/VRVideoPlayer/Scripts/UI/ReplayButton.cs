/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class ReplayButton : ButtonBase, IRunnable
  {
    protected override void OnClick()
    {
      videoPlayerCtrl.ReplayVideo();
    }

        public void Run(Vector3 currentPoint)
        {
            videoPlayerCtrl.ReplayVideo();
        }
  }
}