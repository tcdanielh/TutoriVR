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

    Vector3 GetRecordButtonPosition();

    Vector3 GetRecordButtonEulerAngles();

}
