/* Copyright (c) 2017-present Evereal. All rights reserved. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Evereal.VRVideoPlayer
{
  [RequireComponent(typeof(VRVideoPlayer))]
  public class VideoPlayerCtrl : MonoBehaviour
  {

        
    #region Properties

    // Video playlist
    public GameObject alertMarkerPrefab;
    public GameObject timeMarkerPrefab;
    private ListLedger alertsData;
    public List<string> playlist = new List<string>();
    public VideoTitle videoTitle;
    public PlayButton playButton;
    public VideoTime currentTime;
    
    public VideoTime currentTimeA;
    public VideoTime totalTime;
    public ProgressBar progressBar;
    public awarenessWidgets awarenessWidgets;
    public Transform startPoint;
    public Transform EndPoint;
        public Transform startPointA;
    public Transform EndPointA;
    public VolumeButton volumeButton;
    public VolumeBar volumeBar;
    public RenderModeButton normalButton;
    public RenderModeButton _180Button;
    public RenderModeButton _360Button;
    public StereoModeButton monoButton;
    public StereoModeButton leftRightButton;
    public StereoModeButton topBottomButton;
    public Fade fade;

    private VRVideoPlayer vrVideoPlayer;
    public bool isVideoJumping { get; private set; }

    public bool isVideoPlaying
    {
      get
      {
        return vrVideoPlayer.isPlaying;
      }
    }

    public bool isVideoPaused
    {
      get
      {
        return vrVideoPlayer.isPaused;
      }
    }

    public bool isAudioMute
    {
      get
      {
        return vrVideoPlayer.IsAudioMute(0);
      }
    }

    public double videoTime
    {
      get
      {
        return vrVideoPlayer.time;
      }
      set
      {
        vrVideoPlayer.time = value;
      }
    }

    public double videoLength
    {
      get
      {
        return vrVideoPlayer.length;
      }
    }

    // Get current video index
    private int videoIndex = 0;

    // Log message format template
    private const string LOG_FORMAT = "[VideoPlayerCtrl] {0}";

    #endregion


        public void loadAlerts(ListLedger a)
        {
            alertsData = a;
            float len = Mathf.Abs(startPoint.localPosition.x - EndPoint.localPosition.x);
            float totalTime = alertsData.totalTime;
            foreach(Alert alert in alertsData.alertList)
            {
                GameObject o = Instantiate(alertMarkerPrefab, progressBar.gameObject.transform);
                Color c;
                if (alert.color == ColoredAlert.Red) { c = Color.red; }
                else if (alert.color == ColoredAlert.Blue) { c = Color.blue; }
                else c = Color.yellow;
                o.GetComponent<Image>().color = c;
                float percent = alert.time / totalTime;
                o.transform.localPosition = new Vector3(startPoint.transform.localPosition.x + (percent * len), o.transform.localPosition.x, o.transform.localPosition.z);
            }

            GameObject o1 = Instantiate(timeMarkerPrefab, awarenessWidgets.gameObject.transform);
                o1.GetComponent<Image>().color = Color.black;
               float percent1 =0.5f;
                o1.transform.localPosition = new Vector3(startPointA.transform.localPosition.x + (percent1 * len), o1.transform.localPosition.x, o1.transform.localPosition.z);
 GameObject o2 = Instantiate(timeMarkerPrefab, awarenessWidgets.gameObject.transform);
                o2.GetComponent<Image>().color = Color.black;
              //  float percent1 =0.5f;
                o2.transform.localPosition = new Vector3(startPointA.transform.localPosition.x + (0.25f * len), o2.transform.localPosition.x, o2.transform.localPosition.z);
 GameObject o3 = Instantiate(timeMarkerPrefab, awarenessWidgets.gameObject.transform);
                o3.GetComponent<Image>().color = Color.black;
              //  float percent1 =0.5f;
                o3.transform.localPosition = new Vector3(startPointA.transform.localPosition.x + (0.75f * len), o3.transform.localPosition.x, o3.transform.localPosition.z);



        }

    #region Events

    private void OnStarted(VRVideoPlayer player)
    {
      isVideoJumping = false;
      // set video title
      videoTitle.SetText(player.GetFileName());
      // set total time
      totalTime.SetTime(player.length);
      // toggle play button
      playButton.Toggle();
      // toggle render button
      SwitchVideoRendererButton(player.renderMode);
      // toggle volume button
      volumeButton.Toggle();
      // set volume bar
      volumeBar.SetProgress(vrVideoPlayer.GetAudioVolume(0));
    }

    private void OnLoopPointReached(VRVideoPlayer player)
    {
      // toggle play button
      playButton.Toggle();
    }

    #endregion

    #region Video Player Ctrl

    public string GetVideo()
    {
      if (playlist.Count == 0 || videoIndex >= playlist.Count)
        return null;

      string nextVideo = playlist[videoIndex];

      return nextVideo;
    }

    public void RestartVideo()
    {
      if (isVideoPlaying)
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
        if (isVideoPlaying)
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
        if (isVideoPlaying)
          vrVideoPlayer.Stop();
        RestartVideo();
      }
    }

    public void PauseVideo()
    {
      vrVideoPlayer.Pause();
      playButton.Toggle();
    }

    public void PlayVideo()
    {
      vrVideoPlayer.Play();
      playButton.Toggle();
    }

    public void ReplayVideo()
    {
      vrVideoPlayer.Replay();
      playButton.Toggle();
    }

    public void ToggleVideoPlay()
    {
      if (isVideoPlaying)
      {
        PauseVideo();
      }
      else
      {
        PlayVideo();
      }
    }

    public void FastForward(double step = 5)
    {
      videoTime += step;
    }

    public void Rewind(double step = 5)
    {
      videoTime -= step;
    }

    public double SkipToAlert(bool forwards)
        {
            //Debug.Log(videoTime);
            List<Alert> all = new List<Alert>();
            all.Add(new Alert(0, ColoredAlert.Red));
            all.AddRange(alertsData.alertList);
            all.Add(new Alert(alertsData.totalTime, ColoredAlert.Red));
            int mid = 0;
            while (all[mid].time < videoTime) mid++;
            mid--;
            //int minAlert = 0;
            //int maxAlert = all.Count-1;
            //int mid = (minAlert + maxAlert) / 2;
            //Debug.Log("Video time is " + videoTime);
            //while(minAlert < maxAlert)
            //{
            //    if (!(all[mid].time < videoTime && all[mid + 1].time > videoTime) || Mathf.Abs((float)videoTime - all[mid].time) < 0.1f) break;
            //    if (all[mid].time < videoTime)
            //    {
            //        maxAlert = mid;
            //    }
            //    else
            //    {
            //        minAlert = mid;
            //    }
            //    mid = (minAlert + maxAlert) / 2;
            //}
            //Debug.Log(videoTime);
            if (forwards)
            {
                Debug.Log(all[mid + 1].time);
                double rTime = all[mid + 1].time;
                Debug.Log(rTime);
                videoTime = rTime;
                Debug.Log(videoTime);
                return rTime;
            } else
            {
                //Debug.Log(videoTime);
                double rTime = all[mid].time;
                videoTime = rTime;
                return rTime;
            }
            //if (videoTime > 0.1f) videoTime -= 0.1f;
        }
    public void SetAlerts()
    {
            if (startPointA != null && EndPointA != null)
            {
                float len = Mathf.Abs(startPointA.localPosition.x - EndPointA.localPosition.x);
                float totalTime = alertsData.totalTime;
                //  List<Alert> all = new List<Alert>();
                //  int m = 0;
                //  int k ;
                // for (int k= 0; k< alertsData.alertList.Count; k++)
                foreach (Alert alert in alertsData.alertList)
                {
                    // Alert alert= alertsData.alertList[k];
                    if (alert.time > videoTime - 30 && alert.time < videoTime + 30)
                    {
                        // m++;
                        GameObject o = Instantiate(alertMarkerPrefab, awarenessWidgets.gameObject.transform);
                        Color c;
                        if (alert.color == ColoredAlert.Red) { c = Color.red; }
                        else if (alert.color == ColoredAlert.Blue) { c = Color.blue; }
                        else c = Color.yellow;
                        o.GetComponent<Image>().color = c;
                        float percent = (alert.time - (float)videoTime + 30) / 60;
                        // float destime = alert.time - videoTime;
                        o.transform.localPosition = new Vector3(startPointA.transform.localPosition.x + (percent * len), o.transform.localPosition.x, o.transform.localPosition.z);

                        Destroy(o, 0.1f);

                    }
                    else if (alert.time > videoTime + 30)
                    {
                        break;
                    }
                }
                // return 
            }
           
    }

    public void ToggleAudioMute()
    {
            Debug.Log(isAudioMute);
            Debug.Log(GetComponent<UnityEngine.Video.VideoPlayer>());
            //GetComponent<UnityEngine.Video.VideoPlayer>().SetDirectAudioMute(0, !isAudioMute);
            Debug.Log("here");
      vrVideoPlayer.SetAudioMute(0, !isAudioMute);
      volumeButton.Toggle();
    }

    public void SetAudioVolume(float volume)
    {
      vrVideoPlayer.SetAudioVolume(0, volume);
      volumeBar.SetProgress(volume);
    }

    public void SetVideoRenderMode(RenderMode mode, ProjectionType proj = ProjectionType.NONE)
    {
      // TODO, support 360 equirectangular & cubemap
      if (mode == RenderMode._360)
      {
        proj = ProjectionType.EQUIRECT;
      }
      vrVideoPlayer.SetRenderMode(mode, proj);
      SwitchVideoRendererButton(mode);
    }

    public void SetVideoStereoMode(StereoMode mode)
    {
      vrVideoPlayer.SetStereoMode(mode);
      SwitchVideoStereoButton(mode);
    }

    #endregion

    #region Internal

    private void RestartPlayAndFadeIn()
    {
      StartCoroutine(RestartPlayAndDelayFadeIn());
    }

    private IEnumerator RestartPlayAndDelayFadeIn()
    {
      if (isVideoPlaying)
        vrVideoPlayer.Stop();
      RestartVideo();
      yield return new WaitForSeconds(0.3f);
      if (fade)
        StartCoroutine(fade.StartFadeIn());
    }

    private void SwitchVideoRendererButton(RenderMode mode)
    {
      switch (mode)
      {
        case RenderMode.NORMAL:
          normalButton.SetEnable();
          _180Button.SetDisable();
          _360Button.SetDisable();
          break;
        case RenderMode._180:
          normalButton.SetDisable();
          _180Button.SetEnable();
          _360Button.SetDisable();
          break;
        case RenderMode._360:
          normalButton.SetDisable();
          _180Button.SetDisable();
          _360Button.SetEnable();
          break;
      }
    }

    private void SwitchVideoStereoButton(StereoMode mode)
    {
      switch (mode)
      {
        case StereoMode.NONE:
          monoButton.SetEnable();
          leftRightButton.SetDisable();
          topBottomButton.SetDisable();
          break;
        case StereoMode.LEFT_RIGHT:
          monoButton.SetDisable();
          leftRightButton.SetEnable();
          topBottomButton.SetDisable();
          break;
        case StereoMode.TOP_BOTTOM:
          monoButton.SetDisable();
          leftRightButton.SetDisable();
          topBottomButton.SetEnable();
          break;
      }
    }

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
      vrVideoPlayer = GetComponent<VRVideoPlayer>();

      if (videoTitle == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "VideoTitle not attached!");
      }
      if (playButton == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "PlayButton not attached!");
      }
      if (currentTime == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "CurrentTime not attached!");
      }
      if (totalTime == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "TotalTime not attached!");
      }
      if (volumeButton == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "VolumeButton not attached!");
      }
      if (volumeBar == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "VolumeBar not attached!");
      }
      if (normalButton == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "NormalButton not attached!");
      }
      if (_180Button == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "180Button not attached!");
      }
      if (_360Button == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "360Button not attached!");
      }
      if (monoButton == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "MonoButton not attached!");
      }
      if (leftRightButton == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "LeftRightButton not attached!");
      }
      if (topBottomButton == null)
      {
        Debug.LogErrorFormat(LOG_FORMAT, "TopBottomButton not attached!");
      }
    }

    private void Start()
    {
      if (vrVideoPlayer.autoPlay)
      {
        RestartVideo();
      }
    }

    private void Update()
    {
        currentTime.SetTime(vrVideoPlayer.time);
         //currentTimeA.SetTime(vrVideoPlayer.time);
        progressBar.SetProgress((float)(vrVideoPlayer.time / vrVideoPlayer.length));
            //progressBar.UpdateMic();
            //SetAlerts();
            SetAlerts();
        }

    private void OnEnable()
    {
      vrVideoPlayer.started += OnStarted;
      vrVideoPlayer.loopPointReached += OnLoopPointReached;
      if (fade)
        fade.fadeOutCompleted += RestartPlayAndFadeIn;
    }

    private void OnDisable()
    {
      vrVideoPlayer.started -= OnStarted;
      vrVideoPlayer.loopPointReached -= OnLoopPointReached;
      if (fade)
        fade.fadeOutCompleted -= RestartPlayAndFadeIn;
    }

    #endregion
  }
}