/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class ProgressBar : BarBase, IRunnable
  {
        public MicAudioController micAudioController;
        public VideoPlayerCtrl stereo_video_player;
        public trackController trackCon;

    protected override void OnClick()
    {
      float currentWidth = Vector3.Distance(startPoint.position, currentPoint);
      float progress = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
      videoPlayerCtrl.videoTime = videoPlayerCtrl.videoLength * progress;
    }
        public void Run(Vector3 currentPoint)
        {
            float currentWidth = Vector3.Distance(startPoint.position, currentPoint);
            float progress = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
            //             Debug.Log(startPoint.position);
            //Debug.Log(currentPoint);
            //Debug.Log(currentWidth);
            //Debug.Log(progress);
            //Debug.Log(micAudioController.audioSource.time);
            videoPlayerCtrl.videoTime = videoPlayerCtrl.videoLength * progress;
            stereo_video_player.videoTime = videoPlayerCtrl.videoLength * progress;
            trackCon.SkipToTime((float)videoPlayerCtrl.videoLength * progress);
            micAudioController.SetTime((float)videoPlayerCtrl.videoLength * progress);
            if (videoPlayerCtrl.isVideoPlaying)
            {
                trackCon.Play();
                micAudioController.audioSource.Play();
            } else if (videoPlayerCtrl.isVideoPaused)
            {
                trackCon.Stop();
                micAudioController.audioSource.Stop();
            }
            //Debug.Log(videoPlayerCtrl.videoLength * progress);
            //Debug.Log(trackCon.buttonList[trackCon.index].time);
            SetProgress(progress);
        }

        void Update()
        {
            //Debug.Log(videoPlayerCtrl.videoTime);
        }
  }
}