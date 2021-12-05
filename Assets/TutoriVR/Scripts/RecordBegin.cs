using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using RockVR.Video;


[RequireComponent(typeof(Camera))]
public class RecordBegin : MonoBehaviour, IRunnable
{
    [SerializeField] Material recordButton;
    [SerializeField] Material stopButton;
    [SerializeField] RecordingEvent Event;

    private IAppInfo appInfo;

    VideoCapture[] VC;
    // Start is called before the first frame update
    void Start()
    {
        // appInfo = GetComponentInParent<IAppInfo>();
        // transform.parent = appInfo.GetLeftController();
        // transform.localPosition = appInfo.GetRecordButtonPosition();
        // transform.localEulerAngles = appInfo.GetRecordButtonEulerAngles();
        GameObject[] cams = GameObject.FindGameObjectsWithTag("RecordCam");
        VC = new VideoCapture[cams.Length];
        for (int i = 0; i < cams.Length; i++)
        {
            VC[i] = cams[i].GetComponent<VideoCapture>();
        }
    }

    public void Run(Vector3 currentpoint)
    {
        Debug.Log("Clicked");
        Event.Raise();
        if (!Event.isRecording())
        {
            VideoCaptureCtrl.instance.StopCapture();
            gameObject.GetComponent<Renderer>().material = recordButton;
            Debug.Log("Save & Export");
        }
        else
        {
            RecordingEventListener.recordID = Time.time.ToString();
            PathConfig.SaveFolder = RecordingEventListener.ExportPath();
            foreach (VideoCapture vc in VC) vc.customPathFolder = RecordingEventListener.ExportPath();
            VideoCaptureCtrl.instance.StartCapture();
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("RecordCam"))
            {
                o.GetComponent<FollowHMD>().enabled = true;
            }
            // VideoPlayer.instance.SetRootFolder();
            // VideoPlayer.instance.NextVideo();
            // VideoPlayer.instance.PlayVideo();
            gameObject.GetComponent<Renderer>().material = stopButton;
            Debug.Log("Start Recording");
        }
    }
}
