using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public abstract class RecordingEventListener : MonoBehaviour
{
    public RecordingEvent Event;
    public IAppInfo appInfo;
    public static void ExportJson(string name, string json) 
    {
        string d = Application.dataPath + "/CaptureAt" + Time.time;
        if (!Directory.Exists(d)) Directory.CreateDirectory(d);
        string p = d + "/" + name + ".txt";
        Debug.Log(p);
        //if (!File.Exists(p)) File.Create(p);
        StreamWriter writer = new StreamWriter(p);
        writer.Write(json);
        writer.Close();
    } 

    private void OnEnable()
    {
        appInfo = GameObject.Find("TutoriWidgets").GetComponent<IAppInfo>();
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }


    public void OnRecordRaised()
    {
        if (Event.isRecording())
        {
            StartRecording();
        } else
        {
            EndRecording();
        }
    }

    private void Update()
    {
        //Debug.Log(Event.isRecording());
        if (Event.isRecording())
        {
            DuringRecord();
        }
    }

    public abstract void StartRecording();

    public abstract void DuringRecord();

    public abstract void EndRecording();
}
