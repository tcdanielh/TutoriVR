using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAppInfo
{
    Transform GetLeftController();

    Transform GetRightController();

    Transform GetSceneRootTransform();

    Transform GetHead();

    ButtonStatus GetRightTriggerStatus();

    ButtonStatus GetLeftTriggerStatus();

    ButtonStatus GetUnusedButtonStatus();

    Vector3 GetRecordButtonPosition();

    Vector3 GetRecordButtonEulerAngles();

    string GetSerializedAdditionalInfo();

    Color GetColor(); //Color of the trail in the 3d controller recreation

}
