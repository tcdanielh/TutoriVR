using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackController : MonoBehaviour
{
    public buttonLedger dataLedger;
    private List<ButtonInstance> buttonList;
    private bool playing;
    private int index = 0;
    private float playbackTime;
    private float playbackStartTime;

    [SerializeField] Material buttonDownMat;
    [SerializeField] Material buttonUpMat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            float timePassed = Time.time - playbackStartTime;
            float currentPlaybackTime = playbackTime + timePassed;
            while (buttonList[index + 1].time < currentPlaybackTime)
            {
                index++;
                if (index == buttonList.Count - 1)
                {
                    playing = false;
                    break;
                }
            }
            transform.position = buttonList[index].rightControllerPos;
            transform.rotation = buttonList[index].rightControllerRot;
            if (buttonList[index].rightTriggerDown)
            {
                GetComponent<Renderer>().material = buttonDownMat;
            }
            else
            {
                GetComponent<Renderer>().material = buttonUpMat;
            }
        }
        
    }

    public void Play()
    {
        buttonList = dataLedger.ledger;
        playing = true;
        playbackTime = buttonList[index].time;
        playbackStartTime = Time.time;
    }
}
