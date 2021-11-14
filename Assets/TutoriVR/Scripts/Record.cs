using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour, IRunnable
{
    [SerializeField] Material recordButton;
    [SerializeField] Material stopButton;
    private bool recording;

    private float recordingStartTime;
    private List<(float, ColoredAlert)> recordedAlerts;

    private IAppInfo appInfo;
    // Start is called before the first frame update
    void Start()
    {
        recording = false;
        SetChildrenActive(false);
        appInfo = GetComponentInParent<IAppInfo>();
        transform.parent = appInfo.GetLeftController();
        transform.localPosition = appInfo.GetRecordButtonPosition();
        transform.localEulerAngles = appInfo.GetRecordButtonEulerAngles();
    }

    // Update is called once per frame
    void Update()
    {
        if (recording)
        {

        }
    }

    public void Run()
    {
        if (recording)
        {
            recording = false;
            gameObject.GetComponent<Renderer>().material = recordButton;
            Debug.Log("Save & Export");
            string alertsJSON = JsonUtility.ToJson(recordedAlerts);
            SetChildrenActive(false);
        }
        else
        {
            recording = true;
            gameObject.GetComponent<Renderer>().material = stopButton;
            Debug.Log("Start Recording");
            recordedAlerts = new List<(float, ColoredAlert)>();
            recordingStartTime = Time.time;
            SetChildrenActive(true);
        }
    }

    private void SetChildrenActive(bool setting)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(setting);
        }
    }

    public bool IsRecording() => recording;

    public void RecordNewAlert(ColoredAlert color)
    {
        if (recording)
        {
            recordedAlerts.Add((Time.time-recordingStartTime, color));
        }
    }
}
