using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ButtonStatus
{
    Down,
    Held,
    Up,
    None
}
public class RunOnRaycast : MonoBehaviour
{
    [SerializeField] IAppInfo appInfo;
    [SerializeField] LineRenderer line;
    [SerializeField] Runnable function;
    private Transform rController;
    private Transform lController;
    private ButtonStatus rStat;
    private ButtonStatus lStat;
    // Start is called before the first frame update
    void Start()
    {
        if (function == null)
        {
            function = gameObject.GetComponent<Runnable>();
        }
        appInfo = GetComponent<IAppInfo>();
        rController = appInfo.GetRightController();
        lController = appInfo.GetLeftController();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rController == null) rController = appInfo.GetRightController();
        if (lController == null) lController = appInfo.GetLeftController();
        rStat = appInfo.GetUnusedButtonStatus();
        // lStat = appInfo.GetLeftTriggerStatus();
        checkRay(rController, rStat, line);
        // GameObject o2 = checkRay(lController, lStat, lLine);

    }

    void checkRay(Transform controller, ButtonStatus status, LineRenderer line)
    {
        RaycastHit hit;
        if (Physics.Raycast(controller.position, controller.forward, out hit)){
            if (hit.collider.gameObject.Equals(gameObject))
            {
                line.SetPosition(0, appInfo.GetRightController().position);
                line.SetPosition(1, hit.point);
                line.enabled = true;
                if (status == ButtonStatus.Held)
                {

                } 
                else if (status == ButtonStatus.Up)
                {
                     function.run();
                }
                // if (appInfo.GetRightTriggerDown())
                // {
                //     function.run();
                // }
            }
        }
        else
        {
            line.enabled = false;
        }
    }
}
