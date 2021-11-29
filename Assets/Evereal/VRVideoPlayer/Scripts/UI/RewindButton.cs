/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class RewindButton : ButtonBase, Runnable
  {
		public double seconds = 5;

    protected override void OnClick()
    {
      videoPlayerCtrl.Rewind(seconds);
    }

        public void run(Vector3 currentPoint)
        {
            videoPlayerCtrl.Rewind(seconds);
        }
  }
}