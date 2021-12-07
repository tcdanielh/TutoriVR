/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class StereoModeButton : ButtonBase, IRunnable
  {
    public StereoMode stereoMode = StereoMode.NONE;
        public GameObject mono_quad;
        public GameObject stereo_quad;
        public GameObject videoplayerbutton;

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
        {   if (videoplayerbutton.GetComponent<VideoPlayerButton>().readTutori)
            {
                if (stereoMode == StereoMode.NONE && mono_quad.activeInHierarchy == false)
                {
                    mono_quad.SetActive(true);
                    stereo_quad.SetActive(false);
                    mono_quad.transform.position = stereo_quad.transform.position;
                    mono_quad.transform.rotation = stereo_quad.transform.rotation;
                }
                else if (stereoMode == StereoMode.LEFT_RIGHT && stereo_quad.activeInHierarchy == false)
                {
                    mono_quad.SetActive(false);
                    stereo_quad.SetActive(true);
                    stereo_quad.transform.position = mono_quad.transform.position;
                    stereo_quad.transform.rotation = mono_quad.transform.rotation;
                }
            } else
            {
                videoPlayerCtrl.SetVideoStereoMode(stereoMode);
            }
        }
  }
}