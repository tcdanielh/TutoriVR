/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class VolumeButton : ButtonBase
  {

    public GameObject volumeIcon;
    public GameObject muteIcon;

    protected new const string LOG_FORMAT = "[VolumeButton] {0}";

    protected override void OnClick()
    {
      videoPlayerCtrl.ToggleAudioMute();
    }

    public void Toggle()
    {
      if (volumeIcon == null)
      {
        Debug.LogWarningFormat(LOG_FORMAT, "VolumeIcon not attached!");
      }
      if (muteIcon == null)
      {
        Debug.LogWarningFormat(LOG_FORMAT, "MuteIcon not attached!");
      }

      volumeIcon.SetActive(!videoPlayerCtrl.isAudioMute);
      muteIcon.SetActive(videoPlayerCtrl.isAudioMute);
    }
  }
}