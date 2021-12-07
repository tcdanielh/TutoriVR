using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Evereal.VRVideoPlayer;


// [RequireComponent(typeof(Camera))]
public class VideoPlayerButton : MonoBehaviour, IRunnable
{
    [SerializeField] Material showButton;
    [SerializeField] Material closeButton;
    [SerializeField] bool playFromFile;
    public bool readTutori;
    [SerializeField] GameObject buttonRecreation;
    // [SerializeField] RecordingEvent Event;

    private IAppInfo appInfo;
    private bool currentstate;
    public GameObject VideoPlayer;
    public GameObject VideoPlayer_stereo;
    public GameObject microphone_audio;

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
        VideoPlayer.SetActive(setting);
        VideoPlayer_stereo.SetActive(setting);
        if (setting && microphone_audio.GetComponent<MicAudioController>().audioSource.clip != null)
        {
            microphone_audio.GetComponent<MicAudioController>().PlayAudioFile();
        }
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     transform.GetChild(i).gameObject.SetActive(setting);
        // }
    }

    public void Run(Vector3 currentpoint)
    {
        if (!playFromFile)
        {
            SetChildrenActive(!currentstate);
        } 
        else
        { 
            string d = Application.persistentDataPath + "/recording";
            Debug.Log(d);
            var info = new DirectoryInfo(d);
            var fileInfo = info.GetDirectories();
            var f = fileInfo[0];
            var fileList = f.GetFiles();
            foreach (FileInfo fi in fileList)
            {
                Debug.Log(fi.Name);
            }
            if (readTutori)
            {
                VideoPlayer.GetComponent<VRVideoPlayer>().SetSource(VideoSource.ABSOLUTE_URL);
                VideoPlayer.GetComponent<VideoPlayerCtrl>().playlist[0] = f.FullName + "/MONO.mp4";

                VideoPlayer_stereo.GetComponent<VRVideoPlayer>().SetSource(VideoSource.ABSOLUTE_URL);
                VideoPlayer_stereo.GetComponent<VideoPlayerCtrl>().playlist[0] = f.FullName + "/STEREO.mp4";

                //mic handled in MicAudioController

                StreamReader readerAlerts = new StreamReader(f.FullName + "/alerts.json");
                string alertsJson = readerAlerts.ReadToEnd();
                readerAlerts.Close();

                ListLedger alertsData = JsonUtility.FromJson<ListLedger>(alertsJson);


                StreamReader readerInputs = new StreamReader(f.FullName + "/inputs.json");
                string inputsJson = readerInputs.ReadToEnd();
                readerInputs.Close();

                buttonLedger inputsData = JsonUtility.FromJson<buttonLedger>(inputsJson);
                GameObject br = Instantiate(buttonRecreation, Vector3.zero, Quaternion.identity, null);
                br.GetComponent<trackController>().dataLedger = inputsData;
                br.GetComponent<trackController>().Play();

                SetChildrenActive(!currentstate);
            } else //read from file, only 1 video
            {
                VideoPlayer.GetComponent<VRVideoPlayer>().SetSource(VideoSource.ABSOLUTE_URL);
                VideoPlayer.GetComponent<VideoPlayerCtrl>().playlist[0] = f.FullName + "/" + fileList[0].Name;
                SetChildrenActive(!currentstate);
            }
        }
        
        currentstate = !currentstate;
        if (currentstate==false)
        {
        gameObject.GetComponent<Renderer>().material = showButton;   
        }
        else
        {gameObject.GetComponent<Renderer>().material = closeButton;   
        }
    }
}
