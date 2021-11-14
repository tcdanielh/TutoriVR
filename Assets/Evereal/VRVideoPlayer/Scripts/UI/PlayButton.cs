/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class PlayButton : ButtonBase
  {
    public GameObject playIcon;
    public GameObject pauseIcon;

    protected new const string LOG_FORMAT = "[PlayButton] {0}";

    protected override void OnClick()
    {
      videoPlayerCtrl.ToggleVideoPlay();
      Toggle();
    }

    public void Toggle()
    {
      if (playIcon == null)
      {
        Debug.LogWarningFormat(LOG_FORMAT, "PlayIcon not attached!");
      }
      if (pauseIcon == null)
      {
        Debug.LogWarningFormat(LOG_FORMAT, "PauseIcon not attached!");
      }

      playIcon.SetActive(!videoPlayerCtrl.isVideoPlaying);
      pauseIcon.SetActive(videoPlayerCtrl.isVideoPlaying);
    }
  }
}