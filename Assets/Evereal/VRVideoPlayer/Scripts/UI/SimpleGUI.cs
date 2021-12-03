/* Copyright (c) 2020-present Evereal. All rights reserved. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  [RequireComponent(typeof(VRVideoPlayer))]
  public class SimpleGUI : MonoBehaviour
  {
    #region Properties

    // VRVideoPlayer instance
    private VRVideoPlayer vrVideoPlayer;

    // Video playlist
    [SerializeField]
    public List<string> playlist = new List<string>();

    // Use fade effect
    [SerializeField]
    public Fade fade;

    // Get current video index
    private int videoIndex = 0;
    // If video player is paused
    private bool isPaused;

    #endregion

    #region Video Player GUI

    public string GetVideo()
    {
      if (playlist.Count == 0 || videoIndex >= playlist.Count)
        return null;

      string nextVideo = playlist[videoIndex];

      return nextVideo;
    }

    public void RestartVideo()
    {
      if (vrVideoPlayer.isPlaying)
      {
        vrVideoPlayer.Stop();
      }

      string videoUrl = GetVideo();
      vrVideoPlayer.Load(videoUrl, true);
    }

    public void NextVideo()
    {
      if (playlist.Count == 0 || videoIndex >= playlist.Count)
        return;

      videoIndex += 1;
      videoIndex %= playlist.Count;
    }

    public void PrevVideo()
    {
      if (playlist.Count == 0 || videoIndex < 0)
        return;

      videoIndex -= 1;
      if (videoIndex < 0)
        videoIndex = playlist.Count - 1;
    }

    public void PlayNextVideo()
    {
      // move to next video index
      NextVideo();
      if (fade)
      {
        StartCoroutine(fade.StartFadeOut());
      }
      else
      {
        if (vrVideoPlayer.isPlaying)
          vrVideoPlayer.Stop();
        RestartVideo();
      }
    }

    public void PlayPrevVideo()
    {
      // move to prev video index
      PrevVideo();
      if (fade)
      {
        StartCoroutine(fade.StartFadeOut());
      }
      else
      {
        if (vrVideoPlayer.isPlaying)
          vrVideoPlayer.Stop();
        RestartVideo();
      }
    }

    private void RestartPlayAndFadeIn()
    {
      StartCoroutine(RestartPlayAndDelayFadeIn());
    }

    private IEnumerator RestartPlayAndDelayFadeIn()
    {
      if (vrVideoPlayer.isPlaying)
        vrVideoPlayer.Stop();
      RestartVideo();
      yield return new WaitForSeconds(0.3f);
      if (fade)
        StartCoroutine(fade.StartFadeIn());
    }

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
      vrVideoPlayer = GetComponent<VRVideoPlayer>();
    }

    private void Start()
    {
      if (vrVideoPlayer.autoPlay)
      {
        RestartVideo();
      }
    }

    private void OnEnable()
    {
      if (fade)
        fade.fadeOutCompleted += RestartPlayAndFadeIn;
    }

    private void OnDisable()
    {
      if (fade)
        fade.fadeOutCompleted -= RestartPlayAndFadeIn;
    }

    private void OnGUI()
    {
      if (vrVideoPlayer.isPlaying)
      {
        if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "Stop Play"))
        {
          vrVideoPlayer.Stop();
        }
        if (GUI.Button(new Rect(170, Screen.height - 60, 150, 50), "Pause Play"))
        {
          vrVideoPlayer.Pause();
          isPaused = true;
        }
      }
      else
      {
        if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), isPaused ? "Resume Play" : "Start Play"))
        {
          if (vrVideoPlayer.isPrepared)
          {
            vrVideoPlayer.Play();
          }
          else
          {
            RestartVideo();
          }
          isPaused = false;
        }
      }

      if (GUI.Button(new Rect(Screen.width - 160, Screen.height - 60, 150, 50), "Next Video"))
      {
        PlayNextVideo();
        isPaused = false;
      }
      if (GUI.Button(new Rect(Screen.width - 320, Screen.height - 60, 150, 50), "Prev Video"))
      {
        PlayPrevVideo();
        isPaused = false;
      }

      GUI.Label(new Rect(15, 20, 200, 20), "Render Mode: ");
      if (GUI.Button(new Rect(10, 50, 150, 50), "Normal"))
      {
        vrVideoPlayer.SetRenderMode(RenderMode.NORMAL);
      }
      if (GUI.Button(new Rect(170, 50, 150, 50), "360 Equirect"))
      {
        vrVideoPlayer.SetRenderMode(RenderMode._360, ProjectionType.EQUIRECT);
      }
      if (GUI.Button(new Rect(330, 50, 150, 50), "360 Cubemap"))
      {
        vrVideoPlayer.SetRenderMode(RenderMode._360, ProjectionType.CUBEMAP);
      }
      if (GUI.Button(new Rect(490, 50, 150, 50), "180"))
      {
        vrVideoPlayer.SetRenderMode(RenderMode._180);
      }

      GUI.Label(new Rect(15, 120, 200, 20), "Stereo Mode: ");
      if (GUI.Button(new Rect(10, 150, 150, 50), "None"))
      {
        vrVideoPlayer.SetStereoMode(StereoMode.NONE);
      }
      if (GUI.Button(new Rect(170, 150, 150, 50), "Side by Side"))
      {
        vrVideoPlayer.SetStereoMode(StereoMode.LEFT_RIGHT);
      }
      if (GUI.Button(new Rect(330, 150, 150, 50), "Top Bottom"))
      {
        vrVideoPlayer.SetStereoMode(StereoMode.TOP_BOTTOM);
      }
    }

    #endregion
  }
}