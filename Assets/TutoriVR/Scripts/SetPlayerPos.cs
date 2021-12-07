using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Evereal.VRVideoPlayer;


// [RequireComponent(typeof(Camera))]
public class SetPlayerPos : MonoBehaviour, IRunnableHold
{

    // [SerializeField] RecordingEvent Event;
    // public Text txt;
    private IAppInfo appInfo;
    private bool held;
    //public GameObject videoPlayer;

    // private IAppInfo appInfo;

    // [SerializeField] VideoCapture VC;
    // Start is called before the first frame update
    void Start()
    {
        appInfo = GetComponentInParent<IAppInfo>();
 
        held = false;
        // transform.position = new Vector3 (1,0,-80);
    }

    void Update()
    {
        //Debug.Log(appInfo.GetUnusedButtonStatus());
    }


    ///*IEnumerator EnableEffect()*/
    //    {
    //        // Debug.Log("EnableEffect 1");
    //        //  txt.text="4s";
    //        transform.parent.parent =GameObject.Find("TutoriWidgets").transform;
    //        gameObject.GetComponent<Renderer>().material = stopButton;   
    //        // Debug.Log("EnableEffect 2");
    //       //RealMethod();
    //    }


    IEnumerator Held()
    {
        Debug.Log("held 1");
        while (appInfo.GetUnusedButtonStatus() == ButtonStatus.Held)
        {
            if (transform.parent.GetComponent<VRVideoPlayer>().stereoMode == StereoMode.NONE)
            {
                transform.parent.parent = appInfo.GetRightController();
                yield return null;
                transform.parent.parent = GameObject.Find("VRVideoPlayer_UI (1)").transform;
            } else if (transform.parent.GetComponent<VRVideoPlayer>().stereoMode == StereoMode.LEFT_RIGHT)
            {
                transform.parent.parent = appInfo.GetRightController();
                yield return null;
                transform.parent.parent = GameObject.Find("TutoriWidgets").transform;
            }
        }
    }

    IEnumerator UnHeld()
    {
        while (appInfo.GetUnusedButtonStatus() != ButtonStatus.Held)
        {
            transform.parent.parent = GameObject.Find("TutoriWidgets").transform;
            yield return null;
        }
    }

    //public void Run(Vector3 currentpoint)
    //{
    //    held = !held;
    //    // appInfo = GetComponentInParent<IAppInfo>();
    //    // if (rController == null) rController = appInfo.GetRightController();
    //    // if (lController == null) lController = appInfo.GetLeftController();
    //    if (held)
    //    {
    //        transform.parent.parent = appInfo.GetRightController();
    //    } else
    //    {
    //        transform.parent.parent = GameObject.Find("TutoriWidgets").transform;
    //    }

    //    // yield return new WaitForSeconds(5);

    //}

    public void RunHold(Vector3 currentpoint)
    {

            Debug.Log("rstat held");
            StartCoroutine(Held());

    }
}
