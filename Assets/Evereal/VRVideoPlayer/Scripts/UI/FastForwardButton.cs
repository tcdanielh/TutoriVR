/* Copyright (c) 2020-present Evereal. All rights reserved. */

namespace Evereal.VRVideoPlayer
{
  public class FastForwardButton : ButtonBase
  {
		public double seconds = 5;

    protected override void OnClick()
    {
      videoPlayerCtrl.FastForward(seconds);
    }
  }
}