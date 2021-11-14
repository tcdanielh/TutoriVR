/* Copyright (c) 2020-present Evereal. All rights reserved. */

// #define VRVIDEOPLAYER_AVPROVIDEO

#if VRVIDEOPLAYER_AVPROVIDEO || VRVIDEOPLAYER_AVPROVIDEO_V2

using UnityEngine;
using RenderHeads.Media.AVProVideo;

namespace Evereal.VRVideoPlayer
{
  public class AVProMediaPlayer : IMediaPlayer
  {
    // AVPro video player instance
    private MediaPlayer mediaPlayer;
    // Display video to material
    private ApplyToMaterial applyToMaterial;

    // Game object media player will attached to.
    public GameObject gameObject
    {
      get; private set;
    }

    // The file or HTTP URL that the MediaPlayer reads content from.
    public string url
    {
      get {
#if VRVIDEOPLAYER_AVPROVIDEO_V2
        return mediaPlayer.MediaPath.Path;
#else
        return mediaPlayer.m_VideoPath;
#endif
      }
      set {
#if VRVIDEOPLAYER_AVPROVIDEO_V2
        mediaPlayer.MediaPath.Path = value;
#else
        mediaPlayer.m_VideoPath = value;
#endif
      }
    }

    // Whether content is being played. (Read Only)
    public bool isPlaying
    {
      get { return mediaPlayer.Control.IsPlaying(); }
    }

    // Whether the media player has successfully prepared the content to be played. (Read Only)
    public bool isPrepared
    {
      get { return mediaPlayer.Control.CanPlay(); }
    }

    // Whether playback is paused. (Read Only)
    public bool isPaused
    {
      get { return mediaPlayer.Control.IsPaused(); }
    }

    // The MediaPlayer current time in seconds.
    public double time
    {
      get {
#if VRVIDEOPLAYER_AVPROVIDEO_V2
        return mediaPlayer.Control.GetCurrentTime();
#else
        return (double)(mediaPlayer.Control.GetCurrentTimeMs() / 1000);
#endif
      }
      set { mediaPlayer.Control.Seek((float)(value * 1000)); }
    }

    // The length of the media, or the URL, in seconds. (Read Only)
    public double length
    {
      get {
#if VRVIDEOPLAYER_AVPROVIDEO_V2
        return mediaPlayer.Info.GetDuration();
#else
        return (double)(mediaPlayer.Info.GetDurationMs() / 1000);
#endif
      }
    }

    // The frame index currently being displayed by the MediaPlayer.
    public long frame
    {
      get { return (long)mediaPlayer.TextureProducer.GetTextureFrameCount(); }
    }

    // Number of frames in the current video content.
    public ulong frameCount
    {
      get { return (ulong)mediaPlayer.TextureProducer.GetTextureCount(); }
    }

    // The frame rate of the clip or URL in frames/second. (Read Only)
    public float frameRate
    {
      get { return mediaPlayer.Info.GetVideoFrameRate(); }
    }

    // The width of the images in the media, or URL, in pixels. (Read Only)
    public int width
    {
      get { return mediaPlayer.Info.GetVideoWidth(); }
    }

    // The height of the images in the media, or URL, in pixels. (Read Only)
    public int height
    {
      get { return mediaPlayer.Info.GetVideoHeight(); }
    }

    // Internal texture in which video content is placed. (Read Only)
    public Texture texture
    {
      get { return mediaPlayer.TextureProducer.GetTexture(); }
    }

    // Invoked when the media player preparation is complete.
    public event MediaEventHandler prepareCompleted;

    // Invoked immediately after Play is called.
    public event MediaEventHandler started;

    // Invoked when first frame is ready.
    public event MediaEventHandler firstFrameReady;

    // Invoked when the VideoPlayer reaches the end of the content to play.
    public event MediaEventHandler loopPointReached;

    // Callback function to handle AVPro media player events.
    public void OnMediaPlayerEvent(MediaPlayer player, MediaPlayerEvent.EventType evt, ErrorCode errorCode)
    {
      switch (evt)
      {
        case MediaPlayerEvent.EventType.ReadyToPlay:
          break;
        case MediaPlayerEvent.EventType.Started:
          started(this);
          break;
        case MediaPlayerEvent.EventType.FirstFrameReady:
          firstFrameReady(this);
          break;
        case MediaPlayerEvent.EventType.MetaDataReady:
          prepareCompleted(this);
          break;
        case MediaPlayerEvent.EventType.ResolutionChanged:
          break;
        case MediaPlayerEvent.EventType.FinishedPlaying:
          loopPointReached(this);
          break;
      }
    }

    // Init media player instance.
    public void Init()
    {
      // Init video player
      mediaPlayer = gameObject.AddComponent<MediaPlayer>();
#if VRVIDEOPLAYER_AVPROVIDEO_V2
      mediaPlayer.AutoOpen = false;
      mediaPlayer.AutoStart = false;
#else
      mediaPlayer.m_AutoOpen = false;
      mediaPlayer.m_AutoStart = false;
#endif
      // Init apply to material
      applyToMaterial = gameObject.AddComponent<ApplyToMaterial>();
      applyToMaterial.Player = mediaPlayer;
    }

    // Load a new video file or url.
    public void Load(string url, bool play)
    {
#if VRVIDEOPLAYER_AVPROVIDEO_V2
      mediaPlayer.OpenMedia(MediaPathType.AbsolutePathOrURL, url, play);
#else
      mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, url, play);
#endif
    }

    // Starts media playback.
    public void Play()
    {
      mediaPlayer.Control.Play();
    }

    // Pauses the playback and leaves the current time intact.
    public void Pause()
    {
      mediaPlayer.Control.Pause();
    }

    // Stops the playback and sets the current time to 0.
    public void Stop()
    {
      mediaPlayer.Control.Stop();
    }

    // Set media playback looping.
    public void SetLooping(bool loop)
    {
#if VRVIDEOPLAYER_AVPROVIDEO_V2
      mediaPlayer.Loop = loop;
#else
      // mediaPlayer.Control.SetLooping(loop);
      mediaPlayer.m_Loop = loop;
#endif
    }

    // Determines whether the media player restarts from the beginning when it reaches the end of the clip.
    public bool IsLooping()
    {
      return mediaPlayer.Control.IsLooping();
    }

    // Gets the audio mute status for the specified track.
    public bool IsAudioMute(ushort track)
    {
      return mediaPlayer.Control.IsMuted();
    }

    // Set the audio mute status for the specified track.
    public void SetAudioMute(ushort track, bool mute)
    {
      mediaPlayer.Control.MuteAudio(mute);
    }

    // Return the direct-output volume for specified track.
    public float GetAudioVolume(ushort track)
    {
      return mediaPlayer.Control.GetVolume();
    }

    // Set the audio volume for the specified track.
    public void SetAudioVolume(ushort track, float volume)
    {
      mediaPlayer.Control.SetVolume(volume);
    }

    // Set media target renderer.
    public void SetTargetRenderer(Renderer renderer)
    {
      applyToMaterial.Material = renderer.material;
    }

    // Set player game object.
    public void SetGameObject(GameObject obj)
    {
      gameObject = obj;
    }

    // Should be called when game object becomes enabled.
    public void OnEnable()
    {
      mediaPlayer.Events.AddListener(OnMediaPlayerEvent);
    }

    // Should be called when game object becomes disabled.
    public void OnDisable()
    {
      mediaPlayer.Events.RemoveListener(OnMediaPlayerEvent);
    }
  }
}

#endif