/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;
using UnityEngine.Video;

namespace Evereal.VRVideoPlayer
{
  public class UnityMediaPlayer : IMediaPlayer
  {
    // Unity video player instance
    private VideoPlayer videoPlayer;
    // Video player audio source instance
    private AudioSource audioSource;
    // Play after prepare
    private bool autoPlay;

    // Game object media player will attached to.
    public GameObject gameObject
    {
      get; private set;
    }

    // The file or HTTP URL that the MediaPlayer reads content from.
    public string url
    {
      get { return videoPlayer.url; }
      set { videoPlayer.url = value; }
    }

    // Whether content is being played. (Read Only)
    public bool isPlaying
    {
      get { return videoPlayer.isPlaying; }
    }

    // Whether the media player has successfully prepared the content to be played. (Read Only)
    public bool isPrepared
    {
      get { return videoPlayer.isPrepared; }
    }

    // Whether playback is paused. (Read Only)
    public bool isPaused
    {
      get;
      // TODO, use isPaused in supported Unity version
      private set;
    }

    // The MediaPlayer current time in seconds.
    public double time
    {
      get { return videoPlayer.time; }
      set { videoPlayer.time = value; }
    }

    // The length of the media, or the URL, in seconds. (Read Only)
    public double length
    {
      get
      {
        // TODO set video player length in supported Unity version
        if (frameRate > 0)
          return frameCount / frameRate;
        return 0;
      }
    }

    // The frame index currently being displayed by the MediaPlayer.
    public long frame
    {
      get
      {
        return videoPlayer.frame;
      }
    }

    // Number of frames in the current video content.
    public ulong frameCount
    {
      get
      {
        return videoPlayer.frameCount;
      }
    }

    // The frame rate of the clip or URL in frames/second. (Read Only)
    public float frameRate
    {
      get
      {
        return videoPlayer.frameRate;
      }
    }

    // The width of the images in the media, or URL, in pixels. (Read Only)
    public int width
    {
      get
      {
#if UNITY_2018_3_OR_NEWER
        return (int)videoPlayer.width;
#else
        return videoPlayer.texture.width;
#endif
      }
    }

    // The height of the images in the media, or URL, in pixels. (Read Only)
    public int height
    {
      get
      {
#if UNITY_2018_3_OR_NEWER
        return (int)videoPlayer.height;
#else
        return videoPlayer.texture.height;
#endif
      }
    }

    // Internal texture in which video content is placed. (Read Only)
    public Texture texture
    {
      get
      {
        return videoPlayer.texture;
      }
    }

    // Invoked when the media player preparation is complete.
    public event MediaEventHandler prepareCompleted;
    private void OnPrepareCompleted(VideoPlayer player)
    {
      if (autoPlay)
      {
        Play();
      }
      prepareCompleted(this);
    }

    // Invoked immediately after Play is called.
    public event MediaEventHandler started;
    // Invoked when first frame is ready.
    public event MediaEventHandler firstFrameReady;
    private void OnStarted(VideoPlayer player)
    {
      started(this);
      // Unity VideoPlayer not provide firstFrameReady event
      firstFrameReady(this);
    }

    // Invoked when the VideoPlayer reaches the end of the content to play.
    public event MediaEventHandler loopPointReached;
    private void OnLoopPointReached(VideoPlayer player)
    {
      loopPointReached(this);
    }

    // Init media player instance.
    public void Init()
    {
      // Init audio source
      audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("init");
      audioSource.playOnAwake = false;

      // Init video player
      videoPlayer = gameObject.AddComponent<VideoPlayer>();
      videoPlayer.playOnAwake = false;
      videoPlayer.waitForFirstFrame = true;
      videoPlayer.skipOnDrop = false;

      // Set the source mode first, before changing audio settings
      // as setting the source mode will reset those
      videoPlayer.source = UnityEngine.Video.VideoSource.Url;
      // Set audio output mode to AudioSource
      videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
      videoPlayer.controlledAudioTrackCount = 1;
      // TODO, add set audio track function
      videoPlayer.EnableAudioTrack(0, true);
      videoPlayer.SetTargetAudioSource(0, audioSource);
      // Set video render mode
      videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
    }

    // Load a new video file or url.
    public void Load(string url, bool play)
    {
      videoPlayer.url = url;
      autoPlay = play;
      videoPlayer.Prepare();
    }

    // Starts media playback.
    public void Play()
    {
      if (isPrepared)
      {
        videoPlayer.Play();
      }
      else
      {
        videoPlayer.Prepare();
      }
      isPaused = false;
    }

    // Pauses the playback and leaves the current time intact.
    public void Pause()
    {
      videoPlayer.Pause();
      isPaused = true;
    }

    // Stops the playback and sets the current time to 0.
    public void Stop()
    {
      videoPlayer.Stop();
      isPaused = false;
    }

    // Set media playback looping.
    public void SetLooping(bool loop)
    {
      videoPlayer.isLooping = loop;
    }

    // Determines whether the media player restarts from the beginning when it reaches the end of the clip.
    public bool IsLooping()
    {
      return videoPlayer.isLooping;
    }

    // Gets the audio mute status for the specified track.
    public bool IsAudioMute(ushort track)
    {
      if (videoPlayer.audioOutputMode == VideoAudioOutputMode.Direct)
      {
        return videoPlayer.GetDirectAudioMute(track);
      }
      else if (videoPlayer.audioOutputMode == VideoAudioOutputMode.AudioSource)
      {
        return videoPlayer.GetTargetAudioSource(track).mute;
      }
      return false;
    }

    // Set the audio mute status for the specified track.
    public void SetAudioMute(ushort track, bool mute)
    {
      if (videoPlayer.audioOutputMode == VideoAudioOutputMode.Direct)
      {
        videoPlayer.SetDirectAudioMute(track, mute);
      }
      else if (videoPlayer.audioOutputMode == VideoAudioOutputMode.AudioSource)
      {
        videoPlayer.GetTargetAudioSource(track).mute = mute;
      }
    }

    // Return the direct-output volume for specified track.
    public float GetAudioVolume(ushort track)
    {
      if (videoPlayer.audioOutputMode == VideoAudioOutputMode.Direct)
      {
        return videoPlayer.GetDirectAudioVolume(track);
      }
      else if (videoPlayer.audioOutputMode == VideoAudioOutputMode.AudioSource)
      {
        return videoPlayer.GetTargetAudioSource(track).volume;
      }
      return 0f;
    }

    // Set the audio volume for the specified track.
    public void SetAudioVolume(ushort track, float volume)
    {
      if (videoPlayer.audioOutputMode == VideoAudioOutputMode.Direct)
      {
        videoPlayer.SetDirectAudioVolume(track, volume);
      }
      else if (videoPlayer.audioOutputMode == VideoAudioOutputMode.AudioSource)
      {
        videoPlayer.GetTargetAudioSource(track).volume = volume;
      }
    }

    // Set media target renderer.
    public void SetTargetRenderer(Renderer renderer)
    {
      videoPlayer.targetMaterialRenderer = renderer;
    }

    // Set player game object.
    public void SetGameObject(GameObject obj)
    {
      gameObject = obj;
    }

    // Should be called when game object becomes enabled.
    public void OnEnable()
    {
      videoPlayer.prepareCompleted += OnPrepareCompleted;
      videoPlayer.started += OnStarted;
      videoPlayer.loopPointReached += OnLoopPointReached;
    }

    // Should be called when game object becomes disabled.
    public void OnDisable()
    {
      videoPlayer.prepareCompleted -= OnPrepareCompleted;
      videoPlayer.started -= OnStarted;
      videoPlayer.loopPointReached -= OnLoopPointReached;
    }
  }
}