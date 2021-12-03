/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class StereoModeButton : ButtonBase, IRunnable
  {
    public StereoMode stereoMode = StereoMode.NONE;

    public void SetEnable()
    {
      SpriteRenderer renderer = GetComponent<SpriteRenderer>();
      renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
    }

    public void SetDisable()
    {
      SpriteRenderer renderer = GetComponent<SpriteRenderer>();
      renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);
    }

    protected override void OnClick()
    {
      videoPlayerCtrl.SetVideoStereoMode(stereoMode);
    }

        public void Run(Vector3 currentPoint)
        {
            videoPlayerCtrl.SetVideoStereoMode(stereoMode);
        }
  }
}