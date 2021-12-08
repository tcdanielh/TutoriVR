/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class RewindButton : ButtonBase, IRunnable
  {
		public double seconds = 5;
        public MicAudioController micAudioController;
        public VideoPlayerCtrl stereo_video_player;
        public trackController trackCon;

        protected override void OnClick()
    {
            videoPlayerCtrl.Rewind(seconds);
            //videoPlayerCtrl.SkipToAlert(false);
        }

        public void Run(Vector3 currentPoint)
        {
            double currVideoTime = (float)videoPlayerCtrl.videoTime;
            videoPlayerCtrl.Rewind(seconds);
            stereo_video_player.Rewind(seconds);
            if ((currVideoTime - seconds) < 0)
            {
                currVideoTime = 0;
            } else
            {
                currVideoTime = currVideoTime - seconds;
            }

            trackCon.SkipToTime((float)(currVideoTime));
            micAudioController.SetTime((float)(currVideoTime));
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
            //videoPlayerCtrl.SkipToAlert(false);
        }
  }
}