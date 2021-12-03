/* Copyright (c) 2020-present Evereal. All rights reserved. */

namespace Evereal.VRVideoPlayer
{
  public class NextVideoButton : ButtonBase
  {
    protected override void OnClick()
    {
      videoPlayerCtrl.PlayNextVideo();
    }
  }
}