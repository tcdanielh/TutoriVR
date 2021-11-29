/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class NextVideoButton : ButtonBase, Runnable
  {
    protected override void OnClick()
    {
      videoPlayerCtrl.PlayNextVideo();
    }

        public void  run(Vector3 currentPoint)
        {
            videoPlayerCtrl.PlayNextVideo();
        }
  }
}