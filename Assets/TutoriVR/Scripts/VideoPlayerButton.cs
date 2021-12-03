using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using RockVR.Video;


// [RequireComponent(typeof(Camera))]
public class VideoPlayerButton : MonoBehaviour, IRunnable
{
    [SerializeField] Material showButton;
    [SerializeField] Material closeButton;
    // [SerializeField] RecordingEvent Event;

    private IAppInfo appInfo;
    private bool currentstate;

    // [SerializeField] VideoCapture VC;
    // Start is called before the first frame update
    void Start()
    {
        SetChildrenActive(false);
        currentstate = false;
         gameObject.GetComponent<Renderer>().material = showButton;   
    }
    
    private void SetChildrenActive(bool setting)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(setting);
        }
    }

    public void Run()
    {
        SetChildrenActive(!currentstate);
        currentstate = !currentstate;
        if (currentstate==false)
        {
         
        gameObject.GetComponent<Renderer>().material = showButton;   
        }
        else
        {gameObject.GetComponent<Renderer>().material = closeButton;   

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
