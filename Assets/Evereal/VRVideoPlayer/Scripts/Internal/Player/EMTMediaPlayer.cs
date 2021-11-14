/* Copyright (c) 2020-present Evereal. All rights reserved. */

// #define VRVIDEOPLAYER_EASYMOVIETEXTURE

#if VRVIDEOPLAYER_EASYMOVIETEXTURE

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class EMTMediaPlayer : IMediaPlayer
  {
    // EasyMovieTexture video player instance
    private MediaPlayerCtrl mediaPlayer;

    // EasyMovieTexture not provide function to access volume
    private float volume = 1.0f;

    // Game object media player will attached to.
    public GameObject gameObject
    {
      get; private set;
    }

    // The file or HTTP URL that the MediaPlayer reads content from.
    public string url
    {
      get { return mediaPlayer.m_strFileName; }
      set { mediaPlayer.m_strFileName = value; }
    }

    // Whether content is being played. (Read Only)
    public bool isPlaying
    {
      get { return mediaPlayer.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING; }
    }

    // Whether the media player has successfully prepared the content to be played. (Read Only)
    public bool isPrepared
    {
      get { return mediaPlayer.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY; }
    }

    // Whether playback is paused. (Read Only)
    public bool isPaused
    {
      get { return mediaPlayer.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED; }
    }

    // The MediaPlayer current time in seconds.
    public double time
    {
      get { return (double)mediaPlayer.GetSeekPosition() / 1000; }
      set { mediaPlayer.SeekTo((int)(value * 1000)); }
    }

    // The length of the media, or the URL, in seconds. (Read Only)
    public double length
    {
      get { return (double)(mediaPlayer.GetDuration() / 1000); }
    }

    // The frame rate of the clip or URL in frames/second. (Read Only)
    public float frameRate
    {
      // No frame rate function for EasyMovieTexture.
      get { return 30; }
    }

    // The width of the images in the media, or URL, in pixels. (Read Only)
    public int width
    {
      get { return mediaPlayer.GetVideoWidth(); }
    }

    // The height of the images in the media, or URL, in pixels. (Read Only)
    public int height
    {
      get { return mediaPlayer.GetVideoHeight(); }
    }

    // Internal texture in which video content is placed. (Read Only)
    public Texture texture
    {
      get { return mediaPlayer.GetVideoTexture(); }
    }

    // Invoked when the media player preparation is complete.
    public event MediaEventHandler prepareCompleted;
    private void OnPrepareCompleted()
    {
      prepareCompleted(this);
    }

    // Invoked immediately after Play is called.
    // EasyMovieTexture not provide started event
    public event MediaEventHandler started;

    // Invoked when first frame is ready.
    public event MediaEventHandler firstFrameReady;
    private void OnFirstFrameReady()
    {
      firstFrameReady(this);
    }

    // Invoked when the VideoPlayer reaches the end of the content to play.
    public event MediaEventHandler loopPointReached;
    private void OnLoopPointReached()
    {
      loopPointReached(this);
    }

    // Init media player instance.
    public void Init()
    {
      // Init video player
      mediaPlayer = gameObject.AddComponent<MediaPlayerCtrl>();
      mediaPlayer.m_bAutoPlay = false;
      mediaPlayer.m_bSupportRockchip = true;
      mediaPlayer.m_TargetMaterial = new GameObject[1];
      mediaPlayer.m_shaderYUV = Shader.Find("Unlit/Unlit_YUV");
    }

    // Load a new video file or url.
    public void Load(string url, bool play)
    {
      mediaPlayer.m_bAutoPlay = play;
      if (url.StartsWith("http"))
      {
        mediaPlayer.Load(url);
      }
      else
      {
        mediaPlayer.Load("file://" + url);
      }
    }

    // Starts media playback.
    public void Play()
    {
      mediaPlayer.Play();
    }

    // Pauses the playback and leaves the current time intact.
    public void Pause()
    {
      mediaPlayer.Pause();
    }

    // Stops the playback and sets the current time to 0.
    public void Stop()
    {
      mediaPlayer.Stop();
    }

    // Set media playback looping.
    public void SetLooping(bool loop)
    {
      mediaPlayer.m_bLoop = loop;
    }

    // Determines whether the media player restarts from the beginning when it reaches the end of the clip.
    public bool IsLooping()
    {
      return mediaPlayer.m_bLoop;
    }

    // Gets the audio mute status for the specified track.
    public bool IsAudioMute(ushort track)
    {
      return volume == 0f;
    }

    // Set the audio mute status for the specified track.
    public void SetAudioMute(ushort track, bool mute)
    {
      if (mute)
      {
        mediaPlayer.SetVolume(0);
      }
      else
      {
        mediaPlayer.SetVolume(volume);
      }
    }

    // Return the direct-output volume for specified track.
    public float GetAudioVolume(ushort track)
    {
      return volume;
    }

    // Set the audio volume for the specified track.
    public void SetAudioVolume(ushort track, float volume)
    {
      this.volume = volume;
      mediaPlayer.SetVolume(volume);
    }

    // Set media target renderer.
    public void SetTargetRenderer(Renderer renderer)
    {
      // Flip Y
      renderer.material.SetInt("_FlipY", 1);
      mediaPlayer.m_TargetMaterial[0] = renderer.gameObject;
    }

    // Set player game object.
    public void SetGameObject(GameObject obj)
    {
      gameObject = obj;
    }

    // Should be called when game object becomes enabled.
    public void OnEnable()
    {
      mediaPlayer.OnReady += OnPrepareCompleted;
      mediaPlayer.OnVideoFirstFrameReady += OnFirstFrameReady;
      mediaPlayer.OnEnd += OnLoopPointReached;
    }

    // Should be called when game object becomes disabled.
    public void OnDisable()
    {
      mediaPlayer.OnReady -= OnPrepareCompleted;
      mediaPlayer.OnVideoFirstFrameReady -= OnFirstFrameReady;
      mediaPlayer.OnEnd -= OnLoopPointReached;
    }
  }
}

#endif