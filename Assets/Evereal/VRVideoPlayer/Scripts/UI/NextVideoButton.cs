/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class NextVideoButton : ButtonBase, IRunnable
  {
    protected override void OnClick()
    {
      videoPlayerCtrl.PlayNextVideo();
    }

        public void  Run(Vector3 currentPoint)
        {
            videoPlayerCtrl.PlayNextVideo();
        }
  }
}