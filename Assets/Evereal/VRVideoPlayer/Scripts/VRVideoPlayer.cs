/* Copyright (c) 2017-present Evereal. All rights reserved. */

// #define VRVIDEOPLAYER_EASYMOVIETEXTURE
// #define VRVIDEOPLAYER_AVPROVIDEO

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  /// <summary>
  /// Play VR videos by supported format, such as 360, 180 or stereo video.
  /// </summary>
  public class VRVideoPlayer : MonoBehaviour
  {
    #region Properties

    [Tooltip("Video file source type, supports Absolute Path, Streaming Assets and Project Folder.")]
    [SerializeField]
    public VideoSource videoSource = VideoSource.ABSOLUTE_URL;
    [Tooltip("Video render mode type, supports Normal, 360 and 180 video.")]
    [SerializeField]
    public RenderMode renderMode = RenderMode.NORMAL;
    [Tooltip("360 video projection type, supports Equirectangular and Cube Map.")]
    public ProjectionType projectionType = ProjectionType.EQUIRECT;
    [Tooltip("Video stereo mode type, supports Mono, Side by Side and Top Bottom.")]
    [SerializeField]
    public StereoMode stereoMode = StereoMode.NONE;
    [Tooltip("Video file url, based on video source.")]
    [SerializeField]
    public string videoUrl = "";
    [Tooltip("Start video playback as soon as the game starts or new video url loaded.")]
    [SerializeField]
    public bool autoPlay = true;
    [Tooltip("Start video playback at the beginning when the end is reached.")]
    [SerializeField]
    public bool loop;
    [Tooltip("Video renderer for regular video.")]
    [SerializeField]
    public GameObject quadRenderer;
    [Tooltip("Video renderer for 360 equirectangular video.")]
    [SerializeField]
    public GameObject sphereRenderer;
    [Tooltip("Video renderer for 360 cubemap video.")]
    [SerializeField]
    public GameObject cubeRenderer;
    [Tooltip("Video renderer for 360 equi-angular cubemap video.")]
    public GameObject eacRenderer;
    [Tooltip("Video renderer for 180 video.")]
    [SerializeField]
    public GameObject domeRenderer;
        [SerializeField]
        public GameObject videoPlayerButton;

#if VRVIDEOPLAYER_AVPROVIDEO || VRVIDEOPLAYER_AVPROVIDEO_V2
    // Init media player with AVProMediaPlayer
    private IMediaPlayer mediaPlayer = new AVProMediaPlayer();
#elif VRVIDEOPLAYER_EASYMOVIETEXTURE
    // Init media player with EMTMediaPlayer
    private IMediaPlayer mediaPlayer = new EMTMediaPlayer();
#else
        // Init media player with UnityMediaPlayer
        private IMediaPlayer mediaPlayer = new UnityMediaPlayer();
#endif

    // Video player target renderer
    private GameObject targetRenderer;
    // Video full path
    private string videoFullPath;

    // The file or HTTP URL that the VideoPlayer reads content from.
    public string url
    {
      get { return mediaPlayer.url; }
      set { mediaPlayer.url = value; }
    }

    // Whether content is being played. (Read Only)
    public bool isPlaying
    {
      get { return mediaPlayer.isPlaying; }
    }

    // Whether the media player has successfully prepared the content to be played. (Read Only)
    public bool isPrepared
    {
      get { return mediaPlayer.isPrepared; }
    }

    // Whether playback is paused. (Read Only)
    public bool isPaused
    {
      get { return mediaPlayer.isPaused; }
    }

    // The VideoPlayer current time in seconds.
    public double time
    {
      get { return mediaPlayer.time; }
      set { mediaPlayer.time = value; }
    }

    // The length of the VideoClip, or the URL, in seconds. (Read Only)
    public double length
    {
      get { return mediaPlayer.length; }
    }

    // The frame rate of the clip or URL in frames/second. (Read Only).
    public float frameRate
    {
      get { return mediaPlayer.frameRate; }
    }

    // The width of the images in the VideoClip, or URL, in pixels. (Read Only)
    public int width
    {
      get { return mediaPlayer.width; }
    }

    // The height of the images in the VideoClip, or URL, in pixels. (Read Only)
    public int height
    {
      get { return mediaPlayer.height; }
    }

    // Internal texture in which video content is placed. (Read Only)
    public Texture texture
    {
      get { return mediaPlayer.texture; }
    }

    // Log message format template
    private const string LOG_FORMAT = "[VRVideoPlayer] {0}";

    #endregion

    #region Events

    // Invoked immediately after Play is called.
    public event StartedEvent started = delegate { };
    private void OnStarted(IMediaPlayer player)
    {
      started(this);
    }

    // Invoked when first frame is ready.
    public event FirstFrameReadyEvent firstFrameReady = delegate { };
    private void OnFirstFrameReady(IMediaPlayer player)
    {
      SwitchStereoRenderer();
      if (renderMode == RenderMode.NORMAL)
      {
        ResizeRenderer();
      }
      firstFrameReady(this);
    }

    // Invoked when the VideoPlayer reaches the end of the content to play.
    public event LoopPointReachedEvent loopPointReached = delegate { };
    private void OnLoopPointReached(IMediaPlayer player)
    {
      loopPointReached(this);
    }

    // Invoked when the <c>VideoPlayer</c> preparation is complete.
    public event PrepareCompletedEvent prepareCompleted = delegate { };
    private void OnPrepareCompleted(IMediaPlayer player)
    {
      prepareCompleted(this);
    }

    #endregion

    #region VR Video Player

    // Get internal media player instance.
    public IMediaPlayer GetMediaPlayer()
    {
      return mediaPlayer;
    }

    // Get video file path or url.
    public string GetFileName()
    {
      return System.IO.Path.GetFileName(url);
    }

    // Set where is video load from.
    public void SetSource(VideoSource source)
    {
      videoSource = source;
    }

    // Load a new video file or url.
    public void Load(string url, bool play)
    {
      autoPlay = play;
      if (!string.IsNullOrEmpty(url))
      {
        videoUrl = url;
        videoFullPath = Utils.GetFilePath(url, videoSource);
        mediaPlayer.Load(videoFullPath, play);
      }
    }

    // Starts video playback.
    public void Play()
    {
      mediaPlayer.Play();
    }

    // Restarts video playback.
    public void Replay()
    {
      Stop();
      Play();
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

    // Set video render mode.
    public void SetRenderMode(RenderMode rm, ProjectionType pt = ProjectionType.NONE)
    {
      renderMode = rm;
      projectionType = pt;
      SwitchStereoRenderer();
    }

    // Set video stereo mode.
    public void SetStereoMode(StereoMode sm)
    {
      stereoMode = sm;
      SwitchStereoRenderer();
    }

    // Gets the audio mute status for the specified track.
    public bool IsAudioMute(ushort track)
    {
      return mediaPlayer.IsAudioMute(track);
    }

    // Set the audio mute status for the specified track.
    public void SetAudioMute(ushort track, bool mute)
    {
            Debug.Log(track);
      mediaPlayer.SetAudioMute(track, mute);
    }

    // Return the direct-output volume for specified track.
    public float GetAudioVolume(ushort track)
    {
      return mediaPlayer.GetAudioVolume(track);
    }

    // Set the audio volume for the specified track.
    public void SetAudioVolume(ushort track, float volume)
    {
      mediaPlayer.SetAudioVolume(track, volume);
    }

    #endregion

    #region Internal

    private void SwitchStereoRenderer()
    {
      switch (renderMode)
      {
        case RenderMode.NORMAL:
                    if (videoPlayerButton.GetComponent<VideoPlayerButton>().readTutori == true)
                    {
                        if (stereoMode == StereoMode.NONE)
                        {
                            quadRenderer.SetActive(true);
                        }
                        else if (stereoMode == StereoMode.LEFT_RIGHT)
                        {
                            quadRenderer.SetActive(false);
                        }
                    } else
                    {
                        quadRenderer.SetActive(true);
                    }

          sphereRenderer.SetActive(false);
          cubeRenderer.SetActive(false);
          eacRenderer.SetActive(false);

          domeRenderer.SetActive(false);

          targetRenderer = quadRenderer;

          break;
        case RenderMode._360:
          if (projectionType == ProjectionType.NONE)
          {
            Debug.LogFormat(LOG_FORMAT, "Default set projection type to equirectangular.");
            projectionType = ProjectionType.EQUIRECT;
          }

          quadRenderer.SetActive(false);
          domeRenderer.SetActive(false);

          if (projectionType == ProjectionType.CUBEMAP)
          {
            sphereRenderer.SetActive(false);
            eacRenderer.SetActive(false);
            cubeRenderer.SetActive(true);

            targetRenderer = cubeRenderer;
          }
          else if (projectionType == ProjectionType.EQUIRECT)
          {
            cubeRenderer.SetActive(false);
            eacRenderer.SetActive(false);
            sphereRenderer.SetActive(true);

            targetRenderer = sphereRenderer;
          }
          else if (projectionType == ProjectionType.EAC)
          {
            cubeRenderer.SetActive(false);
            eacRenderer.SetActive(true);
            sphereRenderer.SetActive(false);

            EACMapping mapping = eacRenderer.GetComponent<EACMapping>();
            if (mapping != null)
            {
              mapping.SetStereoMode(stereoMode);
            }

            targetRenderer = eacRenderer;
          }

          break;
        case RenderMode._180:
          quadRenderer.SetActive(false);
          sphereRenderer.SetActive(false);
          cubeRenderer.SetActive(false);
          domeRenderer.SetActive(true);

          targetRenderer = domeRenderer;

          break;
      }
      Renderer targetMaterialRenderer = targetRenderer.GetComponent<Renderer>();
      // Set stereo mode
      targetMaterialRenderer.material.SetInt("_StereoMode", (int)stereoMode);
      // Set renderer
      mediaPlayer.SetTargetRenderer(targetMaterialRenderer);
    }

    private void ResizeRenderer()
    {
      // Keep the video surface ratio when rendering
      float scaleY = targetRenderer.transform.localScale.y;
      float scaleX = (width / (float)height) * scaleY;
      float scaleZ = targetRenderer.transform.localScale.z;
      // Debug.LogFormat(LOG_FORMAT, "x: " + scaleX + ", y: " + scaleY + ", z: " + scaleZ);
      targetRenderer.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
      mediaPlayer.SetGameObject(gameObject);
      // Init media player
      mediaPlayer.Init();
      // Set default renderer
      mediaPlayer.SetTargetRenderer(quadRenderer.GetComponent<Renderer>());
      // Set load video
      if (!string.IsNullOrEmpty(videoUrl))
      {
        Load(videoUrl, autoPlay);
      }
      mediaPlayer.SetLooping(loop);
    }

    private void OnEnable()
    {
      mediaPlayer.OnEnable();
      mediaPlayer.prepareCompleted += OnPrepareCompleted;
      mediaPlayer.started += OnStarted;
      mediaPlayer.firstFrameReady += OnFirstFrameReady;
      mediaPlayer.loopPointReached += OnLoopPointReached;
    }

    private void OnDisable()
    {
      mediaPlayer.OnDisable();
      mediaPlayer.prepareCompleted -= OnPrepareCompleted;
      mediaPlayer.started -= OnStarted;
      mediaPlayer.firstFrameReady -= OnFirstFrameReady;
      mediaPlayer.loopPointReached -= OnLoopPointReached;
    }

    #endregion
  }
}