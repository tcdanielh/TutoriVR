/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class RenderModeButton : ButtonBase
  {
    public RenderMode renderMode = RenderMode.NORMAL;

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
      videoPlayerCtrl.SetVideoRenderMode(renderMode);
    }
  }
}