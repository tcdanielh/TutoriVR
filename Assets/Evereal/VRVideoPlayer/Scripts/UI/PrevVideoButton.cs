/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class PrevVideoButton : ButtonBase, Runnable
  {
    protected override void OnClick()
    {
      videoPlayerCtrl.PlayPrevVideo();
    }

        public void run(Vector3 currentPoint)
        {
            videoPlayerCtrl.PlayPrevVideo();
        }
  }
}