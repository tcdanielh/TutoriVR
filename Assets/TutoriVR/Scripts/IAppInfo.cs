using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAppInfo
{
    Transform GetLeftController();

    Transform GetRightController();

    bool GetRightTriggerDown();

    bool GetLeftTriggerDown();
}
