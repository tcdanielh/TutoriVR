/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class VideoTime : TextBase
  {
    public void SetTime(double time)
    {
      int hours = (int)Mathf.Floor((float)time / 3600);
      int minutes = (int)Mathf.Floor((float)time / 60);
      int seconds = (int)Mathf.Floor((float)time % 60);

      string timeText = string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));
      if (hours > 0)
      {
        timeText = string.Format("{0}:{1}", hours, timeText);
      }

      SetText(timeText);
    }
  }
}
