using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// [RequireComponent(typeof(Camera))]
public class SetPlayerPos : MonoBehaviour,IRunnable
{
    [SerializeField] Material recordButton;
    [SerializeField] Material stopButton;
    // [SerializeField] RecordingEvent Event;
    public Text txt;
    private IAppInfo appInfo;

    // private IAppInfo appInfo;

    // [SerializeField] VideoCapture VC;
    // Start is called before the first frame update
    void Start()
    {
        appInfo = GetComponentInParent<IAppInfo>();
        gameObject.GetComponent<Renderer>().material = stopButton;   
        // transform.position = new Vector3 (1,0,-80);
    }


    IEnumerator EnableEffect()
        {
            // Debug.Log("EnableEffect 1");
            //  txt.text="4s";
            yield return new WaitForSeconds(1f);
            txt.text="3s";
            yield return new WaitForSeconds(1f);
            txt.text="2s";
            yield return new WaitForSeconds(1f);
            txt.text="1s";
            yield return new WaitForSeconds(1f);
            txt.text="0s";
            transform.parent.parent =GameObject.Find("TutoriWidgets").transform;
            gameObject.GetComponent<Renderer>().material = stopButton;   
            // Debug.Log("EnableEffect 2");
           //RealMethod();
        }
    public void Run(Vector3 currentpoint)
    {
        // appInfo = GetComponentInParent<IAppInfo>();
        // if (rController == null) rController = appInfo.GetRightController();
        // if (lController == null) lController = appInfo.GetLeftController();
        gameObject.GetComponent<Renderer>().material = recordButton;   
        txt.text="4s";
        transform.parent.parent = appInfo.GetRightController();
        transform.parent.localPosition = new Vector3(0,2,0);
        transform.parent.localEulerAngles = new Vector3(0,3,0);
       StartCoroutine( EnableEffect());
       txt.text="Move";
        // yield return new WaitForSeconds(5);
        
    }
}
