/* Copyright (c) 2020-present Evereal. All rights reserved. */

namespace Evereal.VRVideoPlayer
{
  public class ReplayButton : ButtonBase, Runnable
  {
    protected override void OnClick()
    {
      videoPlayerCtrl.ReplayVideo();
    }

        public void run()
        {
            videoPlayerCtrl.ReplayVideo();
        }
  }
}