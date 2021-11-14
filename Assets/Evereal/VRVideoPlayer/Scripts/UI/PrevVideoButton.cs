/* Copyright (c) 2020-present Evereal. All rights reserved. */

namespace Evereal.VRVideoPlayer
{
  public class PrevVideoButton : ButtonBase
  {
    protected override void OnClick()
    {
      videoPlayerCtrl.PlayPrevVideo();
    }
  }
}