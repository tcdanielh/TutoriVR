using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Evereal;

public class TiltbrushAppInfo : MonoBehaviour, IAppInfo
{
    [SerializeField] string leftControllerName;
    [SerializeField] string rightControllerName;
    private Transform leftController;
    private Transform rightController;
    private RaycastHit rightHit;
    [SerializeField] private Transform head;
    // private bool rightTrigger;
    // private bool leftTrigger;
    // private bool rightTriggerDown;
    // private bool leftTriggerDown;
    private ButtonStatus rightTriggerStatus;
    private ButtonStatus leftTriggerStatus;
    private ButtonStatus unusedButtonStatus;
    public SteamVR_Action_Single triggerAction;
    public SteamVR_Action_Boolean uAction;
    // Start is called before the first frame update
    void Start()
    {
        leftController = GameObject.Find(leftControllerName).transform;
        rightController = GameObject.Find(rightControllerName).transform;
        if (rightController.gameObject.GetComponent<Evereal.VRVideoPlayer.VRHandRaycaster>() == null)
        {
            rightController.gameObject.AddComponent<Evereal.VRVideoPlayer.VRHandRaycaster>();
        }
        //rightController.gameObject.AddComponent<LineRenderer>();
        //rightController.gameObject.GetComponent<LineRenderer>().startWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(rightController.position, rightController.forward, Color.red);
        //TODO: figure out how button input works
        bool rtVal = triggerAction.GetAxis(SteamVR_Input_Sources.RightHand) > 0.99f;
        bool ltVal = triggerAction.GetAxis(SteamVR_Input_Sources.LeftHand) > 0.99f;
        // rightTriggerDown = rtVal && !rightTrigger;
        // leftTriggerDown = ltVal && !leftTrigger;
        // rightTrigger = rtVal;
        // leftTrigger = ltVal;
        bool uval = uAction.GetState(SteamVR_Input_Sources.RightHand);
        rightTriggerStatus = UpdatedButtonStatus(rightTriggerStatus, rtVal);
        leftTriggerStatus = UpdatedButtonStatus(leftTriggerStatus, ltVal);
        unusedButtonStatus = UpdatedButtonStatus(unusedButtonStatus, uval);
    }
    private ButtonStatus UpdatedButtonStatus(ButtonStatus current, bool isPressed)
    {
        if (isPressed)
        {
            if (current == ButtonStatus.None) 
                return ButtonStatus.Down;
            return ButtonStatus.Held;
        }
        else
        {
            if (current == ButtonStatus.Held || current == ButtonStatus.Down)
            {
                return ButtonStatus.Up;
            } 
            else
            {
                return ButtonStatus.None;
            }
        }
    }
    public Transform GetLeftController() => leftController;
    public Transform GetRightController() => rightController;
    public Transform GetHead() => head;
    // public bool GetRightTriggerDown() => rightTriggerDown;
    // public bool GetLeftTriggerDown() => leftTriggerDown;
    public ButtonStatus GetRightTriggerStatus() => rightTriggerStatus;
    public ButtonStatus GetLeftTriggerStatus() => leftTriggerStatus;

    public ButtonStatus GetUnusedButtonStatus() => unusedButtonStatus;
}
