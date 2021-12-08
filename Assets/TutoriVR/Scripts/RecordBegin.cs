using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Evereal.VideoCapture;


[RequireComponent(typeof(Camera))]
public class RecordBegin : MonoBehaviour, IRunnable
{
    [SerializeField] Material recordButton;
    [SerializeField] Material stopButton;
    [SerializeField] RecordingEvent Event;
    //public GameObject video_begin_button;
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
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("RecordCam"))
            {
                o.GetComponent<VideoCapture>().StopCapture();
            }
            gameObject.GetComponent<Renderer>().material = recordButton;
            Debug.Log("Save & Export");
            //video_begin_button.SetActive(true);
        }
        else
        {
            RecordingEventListener.recordID = Time.time.ToString();
            foreach (VideoCapture vc in VC)
            {
                vc.saveFolder = RecordingEventListener.ExportPath();
                
            }

            //foreach (VideoCapture vc in VC) vc.customPathFolder = RecordingEventListener.ExportPath();
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("RecordCam"))
            {
                if (o.GetComponent<VideoCapture>().captureSource == CaptureSource.CAMERA)
                {
                    o.GetComponent<VideoCapture>().SetCustomFileName("MONO");
                } else if (o.GetComponent<VideoCapture>().captureSource == CaptureSource.SCREEN) {
                    o.GetComponent<VideoCapture>().SetCustomFileName("STEREO");
                }
                Debug.Log("in here");
                o.GetComponent<VideoCapture>().StartCapture();
                o.GetComponent<FollowHMD>().enabled = true;
            }


            // VideoPlayer.instance.SetRootFolder();
            // VideoPlayer.instance.NextVideo();
            // VideoPlayer.instance.PlayVideo();
            //video_begin_button.SetActive(false);
            gameObject.GetComponent<Renderer>().material = stopButton;
            Debug.Log("Start Recording");
        }
    }
}
