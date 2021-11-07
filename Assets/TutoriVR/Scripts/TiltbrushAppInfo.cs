using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TiltbrushAppInfo : MonoBehaviour, IAppInfo
{
    [SerializeField] string leftControllerName;
    [SerializeField] string rightControllerName;
    private Transform leftController;
    private Transform rightController;
    private RaycastHit rightHit;
    private bool rightTrigger;
    private bool leftTrigger;
    private bool rightTriggerDown;
    private bool leftTriggerDown;
    public SteamVR_Action_Single triggerAction;
    // Start is called before the first frame update
    void Start()
    {
        leftController = GameObject.Find(leftControllerName).transform;
        rightController = GameObject.Find(rightControllerName).transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(rightController.position, rightController.forward, Color.red);
        //TODO: figure out how button input works
        bool rtVal = triggerAction.GetAxis(SteamVR_Input_Sources.RightHand) > 0.99f;
        bool ltVal = triggerAction.GetAxis(SteamVR_Input_Sources.LeftHand) > 0.99f;
        rightTriggerDown = rtVal && !rightTrigger;
        leftTriggerDown = ltVal && !leftTrigger;
        rightTrigger = rtVal;
        leftTrigger = ltVal;
    }

    public Transform GetLeftController() => leftController;
    public Transform GetRightController() => rightController;
    public bool GetRightTriggerDown() => rightTriggerDown;
    public bool GetLeftTriggerDown() => leftTriggerDown;
}
