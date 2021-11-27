using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAppInfo
{
    Transform GetLeftController();

    Transform GetRightController();
    Transform GetHead();
    ButtonStatus GetRightTriggerStatus();

    ButtonStatus GetLeftTriggerStatus();

    ButtonStatus GetUnusedButtonStatus();

    // bool GetRightTriggerDown();

    // bool GetLeftTriggerDown();
}
