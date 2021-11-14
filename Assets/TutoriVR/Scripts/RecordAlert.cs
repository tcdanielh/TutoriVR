using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColoredAlert
{
    Red,
    Blue,
    Yellow
}

public class RecordAlert : MonoBehaviour, IRunnable
{
    [SerializeField] Record recorder;
    [SerializeField] ColoredAlert alertColor;
    public void Run()
    {
        recorder.RecordNewAlert(alertColor);
    }

}
