using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using RockVR.Video;

[RequireComponent(typeof(Camera))]
public class Record : MonoBehaviour, Runnable
{
    [SerializeField] Material recordButton;
    [SerializeField] Material stopButton;
    private bool recording;

    // public RenderTexture videoRenderTexture;
    // public TextureFormat saveFormat = TextureFormat.RGB24;

    // private Camera captureCamera;
    // public float deltaFrameTime;
    // public string filePath;

    // public VideoCapture vidcapture;

    // protected void Awake()
    // {
    //     captureCamera = GetComponent<Camera>();
    //     deltaFrameTime = 1f / 30;
    // }

    // Start is called before the first frame update
    void Start()
    {
        // vidcapture = new VideoCapture();
        recording = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (recording)
        {
            // vidcapture.StartCapture();
        }
    }

    public void run()
    {
        if (recording)
        {
            recording = false;
            VideoCaptureCtrl.instance.StopCapture();
            gameObject.GetComponent<Renderer>().material = recordButton;
            Debug.Log("Save & Export");
        }
        else
        {
            recording = true;

            VideoCaptureCtrl.instance.StartCapture();
            // VideoPlayer.instance.SetRootFolder();
            // VideoPlayer.instance.NextVideo();
            // VideoPlayer.instance.PlayVideo();

            gameObject.GetComponent<Renderer>().material = stopButton;
            Debug.Log("Start Recording");
        }
    }
}
