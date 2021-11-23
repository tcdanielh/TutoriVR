using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RecordingEvent : ScriptableObject
{
    private List<RecordingEventListener> listeners = new List<RecordingEventListener>();
    private bool recording = false;

    private void OnEnable()
    {
        recording = false;
    }

    public void Raise()
    {
        recording = !recording;
        foreach (RecordingEventListener listener in listeners)
        {
            listener.OnRecordRaised();
        }
    }

    public void RegisterListener(RecordingEventListener l)
    {
        listeners.Add(l);
    }

    public void UnregisterListener(RecordingEventListener l)
    {
        listeners.Remove(l);
    }

    public bool isRecording() => recording;

    
}
