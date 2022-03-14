using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class buttonLedger
{
    public List<ButtonInstance> ledger;
    public buttonLedger()
    {
        ledger = new List<ButtonInstance>();
    }
}

public class TrackButtons : RecordingEventListener
{
    private buttonLedger captures; //dictionary: frame to button instance class
    private ButtonInstance binstance;
    private float recordingStartTime;
    [SerializeField] GameObject tracker;

    public void fStart()
    {
        //captures = new Dictionary<int, ButtonInstance>();
    }

    public void fUpdate()
    {
        //Debug.Log("HERE!!!");
       // binstance = new ButtonInstance();
        //captures[Time.frameCount] = binstance.createInstance();
        //Debug.Log(Time.frameCount + " " + captures[Time.frameCount].brushtype);
    }

    public override void StartRecording()
    {
        captures = new buttonLedger();
        recordingStartTime = Time.time;
    }

    public override void DuringRecord()
    {
        binstance = new ButtonInstance();
        binstance.createInstance(appInfo, Time.time - recordingStartTime,tracker);
        captures.ledger.Add(binstance);
        //Debug.Log(JsonUtility.ToJson(binstance));
    }

    public override void EndRecording()
    {
        string buttonJSON = JsonUtility.ToJson(captures);
        Debug.Log(buttonJSON);
        ExportJson("inputs", buttonJSON);
    }
}
