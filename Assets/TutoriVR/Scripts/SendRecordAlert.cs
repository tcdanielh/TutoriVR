using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColoredAlert
{
    Red,
    Blue,
    Yellow
}

public class SendRecordAlert : MonoBehaviour, IRunnable
{
    [SerializeField] AlertLedger recorder;
    [SerializeField] ColoredAlert alertColor;
    public void Run(Vector3 currentpoint)
    {
        recorder.RecordNewAlert(alertColor);
    }

}
