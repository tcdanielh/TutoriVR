using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecordingEventListener : MonoBehaviour
{
    public RecordingEvent Event;


    private void OnEnable()
    {
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
        if (Event.isRecording())
        {

        }
    }

    public abstract void StartRecording();

    public abstract void DuringRecord();

    public abstract void EndRecording();
}
