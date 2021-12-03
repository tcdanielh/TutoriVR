/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class FastForwardButton : ButtonBase, IRunnable
  {
		public double seconds = 5;

    protected override void OnClick()
    {
      videoPlayerCtrl.FastForward(seconds);
    }

        public void Run(Vector3 currentPoint)
        {
            videoPlayerCtrl.FastForward(seconds);
        }
  }
}