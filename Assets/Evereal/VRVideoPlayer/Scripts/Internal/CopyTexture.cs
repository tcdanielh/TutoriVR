/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  [RequireComponent(typeof(Renderer))]
  public class CopyTexture : MonoBehaviour
  {
    public VRVideoPlayer vrPlayer;

    private void Update()
    {
      // Video source not ready yet.
      if (vrPlayer == null ||
        !vrPlayer.isPlaying ||
        vrPlayer.texture == null)
      {
        return;
      }

      if (GetComponent<Renderer>().material.mainTexture == null ||
          !GetComponent<Renderer>().material.mainTexture.Equals(vrPlayer.texture))
      {
        GetComponent<Renderer>().material.mainTexture = vrPlayer.texture;
      }
    }
  }
}
