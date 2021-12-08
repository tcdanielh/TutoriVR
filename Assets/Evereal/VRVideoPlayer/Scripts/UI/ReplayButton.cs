/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class ReplayButton : ButtonBase, IRunnable
  {
        public MicAudioController micAudioController;
        public VideoPlayerCtrl stereo_video_player;
        public trackController trackCon;

        protected override void OnClick()
    {
      videoPlayerCtrl.ReplayVideo();
    }

        public void Run(Vector3 currentPoint)
        {
            videoPlayerCtrl.ReplayVideo();
            stereo_video_player.ReplayVideo();
            trackCon.SkipToTime(0);
            micAudioController.SetTime(0);
            trackCon.Play();
            micAudioController.audioSource.Play();
        }
  }
}