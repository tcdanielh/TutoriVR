/* Copyright (c) 2020-present Evereal. All rights reserved. */
using UnityEngine;
namespace Evereal.VRVideoPlayer
{
  public class FastForwardButton : ButtonBase, IRunnable
  {
		public double seconds = 5;
        public MicAudioController micAudioController;
        public VideoPlayerCtrl stereo_video_player;
        public trackController trackCon;

        protected override void OnClick()
    {
            videoPlayerCtrl.FastForward(seconds);
            //videoPlayerCtrl.SkipToAlert(true);
    }

        public void Run(Vector3 currentPoint)
        {
            double currVideoTime = (float)videoPlayerCtrl.videoTime;
            videoPlayerCtrl.FastForward(seconds);
            stereo_video_player.FastForward(seconds);
            if ((currVideoTime + seconds) > videoPlayerCtrl.videoLength)
            {
                currVideoTime = videoPlayerCtrl.videoLength;
            }
            else
            {
                currVideoTime = currVideoTime + seconds;
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
            //videoPlayerCtrl.SkipToAlert(true);
        }
  }
}