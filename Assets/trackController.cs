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

    [SerializeField] GameObject leftControllerMimic;
    [SerializeField] GameObject rightControllerMimic;
    bool brushWasDown;
    [SerializeField] GameObject trailPrefab;
    GameObject trail;

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
            rightControllerMimic.transform.position = buttonList[index].rightControllerPos;
            rightControllerMimic.transform.rotation = buttonList[index].rightControllerRot;
            leftControllerMimic.transform.position = buttonList[index].leftControllerPos;
            leftControllerMimic.transform.rotation = buttonList[index].leftControllerRot;
            if (buttonList[index].rightTriggerDown)
            {
                if (!brushWasDown)
                {
                    //Debug.Log("!brushwasdown");
                    rightControllerMimic.GetComponent<Renderer>().material = buttonDownMat;
                    trail = Instantiate(trailPrefab, rightControllerMimic.transform.position, rightControllerMimic.transform.rotation, rightControllerMimic.transform);
                    trail.GetComponent<TrailRenderer>().colorGradient = GradientFromColor(buttonList[index].color);
                }
            }
            else
            {
                if (brushWasDown)
                {
                    //Debug.Log("brushwasdown");
                    rightControllerMimic.GetComponent<Renderer>().material = buttonUpMat;
                    if (trail != null)
                    {
                        //Debug.Log("brushwasdown2");
                        trail.transform.parent = null;
                        Destroy(trail, 3);
                        trail = null;
                    }
                }
            }
            brushWasDown = buttonList[index].rightTriggerDown;
        }
        
    }

    public void Play()
    {
        buttonList = dataLedger.ledger;
        if (index == buttonList.Count) index = 0;
        playing = true;
        playbackTime = buttonList[index].time;
        playbackStartTime = Time.time;
    }

    public void Stop()
    {
        playing = false;
    }

    public void SkipToTime(float time)
    {
        if (trail != null)
        {
            Destroy(trail);
            trail = null;
        }

        //binary search to find largest timestamp less than given time
        int maxIndex = buttonList.Count;
        int minIndex = 0;
        
        while (minIndex <= maxIndex)
        {
            int bestIndex = (maxIndex + minIndex) / 2;
            if (buttonList[bestIndex].time <= time && buttonList[bestIndex + 1].time >= time)
            {
                index = bestIndex;
                return;
            }
            else if (buttonList[bestIndex].time <= time)
            {
                minIndex = bestIndex;
            }
            else
            {
                maxIndex = bestIndex;
            }
        }

        index = minIndex;
    }

    private Gradient GradientFromColor(Color c)
    {
        var gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        var colorKey = new GradientColorKey[2];
        colorKey[0].color = c;
        colorKey[0].time = 0.0f;
        colorKey[1].color = c;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        var alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
        return gradient;
    }
    
}
