/* Copyright (c) 2020-present Evereal. All rights reserved. */

namespace Evereal.VRVideoPlayer
{
  public class PrevVideoButton : ButtonBase, Runnable
  {
    protected override void OnClick()
    {
      videoPlayerCtrl.PlayPrevVideo();
    }

        public void run()
        {
            videoPlayerCtrl.PlayPrevVideo();
        }
  }
}