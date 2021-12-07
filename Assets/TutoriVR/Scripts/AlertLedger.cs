using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Alert
{
    public float time;
    public ColoredAlert color;
    public Alert(float t, ColoredAlert c)
    {
        this.time = t;
        this.color = c;
    }
}

[Serializable]
public class ListLedger
{
    public List<Alert> alertList;
    public float totalTime;
    public ListLedger()
    {
        alertList = new List<Alert>();
    }
}

[CreateAssetMenu]
public class AlertLedger : ScriptableObject
{
    private float recordingStartTime;
    //private List<(float, ColoredAlert)> recordedAlerts;
    [SerializeField] RecordingEvent Event;
    public ListLedger recordedAlerts = new ListLedger();
    public void Restart()
    {
        recordedAlerts = new ListLedger();
        recordingStartTime = Time.time;
    }

    public void RecordNewAlert(ColoredAlert color)
    {
        if (Event.isRecording())
        {
            recordedAlerts.alertList.Add(new Alert(Time.time - recordingStartTime, color));
        }
    }

    public string toJSON()
    {
        Debug.Log("recorded alerts count is " + recordedAlerts.alertList.Count);
        for (int i = 0; i < recordedAlerts.alertList.Count; i++)
        {
            Debug.Log(recordedAlerts.alertList[i]);
        }
        recordedAlerts.totalTime = Time.time - recordingStartTime;
        return JsonUtility.ToJson(recordedAlerts);
    }
}
