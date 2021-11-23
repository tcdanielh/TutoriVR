using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        ExportJson("alerts", alertJSON);
    }

    private void SetChildrenActive(bool setting)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(setting);
        }
    }
}
