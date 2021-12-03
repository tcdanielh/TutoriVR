using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using RockVR.Video;


// [RequireComponent(typeof(Camera))]
public class Record : MonoBehaviour, IRunnable
{
    [SerializeField] Material recordButton;
    [SerializeField] Material stopButton;
    [SerializeField] RecordingEvent Event;

    private IAppInfo appInfo;
    private bool currentstate;

    // [SerializeField] VideoCapture VC;
    // Start is called before the first frame update
    void Start()
    {
        appInfo = GetComponentInParent<IAppInfo>();
        transform.parent = appInfo.GetLeftController();
        transform.localPosition = appInfo.GetRecordButtonPosition();
        transform.localEulerAngles = appInfo.GetRecordButtonEulerAngles();
        SetChildrenActive(false);
        currentstate = false;
         gameObject.GetComponent<Renderer>().material = recordButton;   
    }
    
    private void SetChildrenActive(bool setting)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(setting);
        }
    }

    public void Run(Vector3 currentpoint)
    {
        SetChildrenActive(!currentstate);
        currentstate = !currentstate;
        if (currentstate==false)
        {
         
        gameObject.GetComponent<Renderer>().material = recordButton;   
        }
        else
        {gameObject.GetComponent<Renderer>().material = stopButton;   

        }
        // Event.Raise();
        // if (!Event.isRecording())
        // {
        //     VideoCaptureCtrl.instance.StopCapture();
        //     gameObject.GetComponent<Renderer>().material = recordButton;
        //     Debug.Log("Save & Export");
        // }
        // else
        // {
        //     RecordingEventListener.recordID = Time.time.ToString();
        //     PathConfig.SaveFolder = RecordingEventListener.ExportPath();
        //     VC.customPathFolder = RecordingEventListener.ExportPath();
        //     VideoCaptureCtrl.instance.StartCapture();
        //     // VideoPlayer.instance.SetRootFolder();
        //     // VideoPlayer.instance.NextVideo();
        //     // VideoPlayer.instance.PlayVideo();
        //     gameObject.GetComponent<Renderer>().material = stopButton;
        //     Debug.Log("Start Recording");
        // }
    }
}
