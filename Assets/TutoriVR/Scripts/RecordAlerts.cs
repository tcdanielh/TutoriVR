using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordAlerts : RecordingEventListener
{
    [SerializeField] private AlertLedger ledger;

    private void Start()
    {
        SetChildrenActive(false);
    }

    public override void StartRecording()
    {
        ledger.Restart();
        SetChildrenActive(true);
    }

    public override void DuringRecord()
    {
        
    }

    public override void EndRecording()
    {
        string alertJSON = ledger.toJSON();
        Debug.Log(alertJSON);
        SetChildrenActive(false);
    }

    private void SetChildrenActive(bool setting)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(setting);
        }
    }
}
