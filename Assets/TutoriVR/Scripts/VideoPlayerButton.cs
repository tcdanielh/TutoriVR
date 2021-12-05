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
    [SerializeField] bool playFromFile;
    [SerializeField] GameObject buttonRecreation;
    // [SerializeField] RecordingEvent Event;

    private IAppInfo appInfo;
    private bool currentstate;
    public GameObject VideoPlayer;

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
            StreamReader reader = new StreamReader(f.FullName + "/inputs.json");
            string inputsJson = reader.ReadToEnd();
            reader.Close();
            buttonLedger inputsData = JsonUtility.FromJson<buttonLedger>(inputsJson);
            GameObject br = GameObject.Instantiate(buttonRecreation);
            br.GetComponent<trackController>().dataLedger = inputsData;
            br.GetComponent<trackController>().Play();
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
