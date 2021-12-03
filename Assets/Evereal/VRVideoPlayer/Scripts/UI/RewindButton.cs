/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class RewindButton : ButtonBase, IRunnable
  {
		public double seconds = 5;

    protected override void OnClick()
    {
      videoPlayerCtrl.Rewind(seconds);
    }

        public void Run(Vector3 currentPoint)
        {
            videoPlayerCtrl.Rewind(seconds);
        }
  }
}