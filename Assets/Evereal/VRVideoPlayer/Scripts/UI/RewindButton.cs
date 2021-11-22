/* Copyright (c) 2020-present Evereal. All rights reserved. */

namespace Evereal.VRVideoPlayer
{
  public class RewindButton : ButtonBase, Runnable
  {
		public double seconds = 5;

    protected override void OnClick()
    {
      videoPlayerCtrl.Rewind(seconds);
    }

        public void run()
        {
            videoPlayerCtrl.Rewind(seconds);
        }
  }
}