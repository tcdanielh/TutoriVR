/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class PlayButton : ButtonBase, IRunnable
  {
    public GameObject playIcon;
    public GameObject pauseIcon;
        public MicAudioController micAudioController;
        public VideoPlayerCtrl stereo_video_player;
        public trackController trackCon;

        protected new const string LOG_FORMAT = "[PlayButton] {0}";

    protected override void OnClick()
    {
      videoPlayerCtrl.ToggleVideoPlay();
      Toggle();
    }

        public void  Run(Vector3 currentPoint)
        {
            videoPlayerCtrl.ToggleVideoPlay();
            stereo_video_player.ToggleVideoPlay();
            if (videoPlayerCtrl.isVideoPlaying)
            {
                trackCon.SkipToTime((float)videoPlayerCtrl.videoTime);
                trackCon.Play();
                micAudioController.SetTime((float)videoPlayerCtrl.videoTime);
                micAudioController.audioSource.Play();
            }
            else if (videoPlayerCtrl.isVideoPaused)
            {
                trackCon.Stop();
                micAudioController.audioSource.Stop();
            }
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