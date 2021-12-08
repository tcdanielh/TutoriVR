/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class PrevVideoButton : ButtonBase, IRunnable
  {
        public MicAudioController micAudioController;
        public VideoPlayerCtrl stereo_video_player;
        public trackController trackCon;

        protected override void OnClick()
    {
      videoPlayerCtrl.PlayPrevVideo();
    }

        public void Run(Vector3 currentPoint)
        {
            //videoPlayerCtrl.PlayPrevVideo();
            double rTime = videoPlayerCtrl.SkipToAlert(false);
            stereo_video_player.videoTime = rTime;
            Debug.Log(rTime);
            trackCon.SkipToTime((float)rTime);
            micAudioController.SetTime((float)rTime);
            if (videoPlayerCtrl.isVideoPlaying)
            {
                trackCon.Play();
                micAudioController.audioSource.Play();
            }
            else if (videoPlayerCtrl.isVideoPaused)
            {
                trackCon.Stop();
                micAudioController.audioSource.Stop();
            }
        }
  }
}