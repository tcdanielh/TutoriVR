using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour, IRunnable
{
    [SerializeField] Material recordButton;
    [SerializeField] Material stopButton;
    [SerializeField] RecordingEvent Event;

    private IAppInfo appInfo;
    // Start is called before the first frame update
    void Start()
    {
        appInfo = GetComponentInParent<IAppInfo>();
        transform.parent = appInfo.GetLeftController();
        transform.localPosition = appInfo.GetRecordButtonPosition();
        transform.localEulerAngles = appInfo.GetRecordButtonEulerAngles();
    }

    public void Run()
    {
        Event.Raise();

        if (!Event.isRecording())
        {
            gameObject.GetComponent<Renderer>().material = recordButton;
            Debug.Log("Save & Export");
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = stopButton;
            Debug.Log("Start Recording");
        }
    }
}
