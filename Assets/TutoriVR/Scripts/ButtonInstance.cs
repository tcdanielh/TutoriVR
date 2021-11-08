using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInstance : MonoBehaviour
{
    // private ?? script = script being run;
    private Transform leftControllerPos;
    private Transform rightControllerPos;
    private bool rightTriggerDown;
    private bool leftTriggerDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // gets data of current frame
    void createInstance()
    {
        // script = check script that is running
        leftControllerPos = TiltbrushAppInfo.GetLeftController();
        rightControllerPos = TiltbrushAppInfo.GetRightController();
        rightTriggerDown = TiltbrushAppInfo.GetRightTriggerDown();
        leftTriggerDown = TiltbrushAppInfo.GetLeftTriggerDown();
    }
}
