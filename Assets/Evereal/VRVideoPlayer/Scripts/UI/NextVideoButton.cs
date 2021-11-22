/* Copyright (c) 2020-present Evereal. All rights reserved. */

namespace Evereal.VRVideoPlayer
{
  public class NextVideoButton : ButtonBase, Runnable
  {
    protected override void OnClick()
    {
      videoPlayerCtrl.PlayNextVideo();
    }

        public void run()
        {
            videoPlayerCtrl.PlayNextVideo();
        }
  }
}