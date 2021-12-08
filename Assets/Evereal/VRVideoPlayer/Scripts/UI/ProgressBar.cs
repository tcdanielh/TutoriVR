/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class ProgressBar : BarBase, IRunnable
  {
        public MicAudioController micAudioController;
        public VideoPlayerCtrl stereo_video_player;

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
            //Debug.Log("here" + videoPlayerCtrl.currentTime);
            micAudioController.SetTime((float)videoPlayerCtrl.videoLength * progress);
            //Debug.Log(micAudioController.audioSource.time);
            SetProgress(progress);
        }

        void Update()
        {
            //Debug.Log(videoPlayerCtrl.videoTime);
        }
  }
}