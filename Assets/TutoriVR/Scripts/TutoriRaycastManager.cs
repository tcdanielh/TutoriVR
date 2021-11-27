using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoriRaycastManager : MonoBehaviour
{
    [SerializeField] IAppInfo appInfo;
    [SerializeField] LineRenderer lLine;
    [SerializeField] LineRenderer rLine;
    private Transform rController;
    private Transform lController;
    private bool rClicked;
    private bool lClicked;
    private ButtonStatus rStat;
    private ButtonStatus lStat;
    // Start is called before the first frame update
    void Start()
    {
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
        lStat = appInfo.GetLeftTriggerStatus();
        // checkRay(rController, rStat, line);
        // rClicked = appInfo.GetRightTriggerDown();
        // lClicked = appInfo.GetLeftTriggerDown();
                // checkRay(rController, rStat, line);
        checkRay(rController, rStat, rLine);
        checkRay(lController, lStat, lLine);
    }
    void checkRay(Transform controller, ButtonStatus status, LineRenderer line)
    {
        RaycastHit hit;
        Debug.DrawRay(controller.position, controller.forward, Color.red);
        if (Physics.Raycast(controller.position, controller.forward, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Runnable>() != null)
            {
                line.SetPosition(0, controller.position);
                line.SetPosition(1, hit.point);
                line.enabled = true;
                // if (status == ButtonStatus.Held)
                // {

                // } 
                // else
                 if (status == ButtonStatus.Up)
                {
                    hit.collider.gameObject.GetComponent<Runnable>().run();
                }
                // if (clicked)
                // {
                //     hit.collider.gameObject.GetComponent<Runnable>().run();
                // }
            }
        }
        else
        {
            line.enabled = false;
        }
    }
}
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TutoriRaycastManager : MonoBehaviour
// {
//     [SerializeField] IAppInfo appInfo;
//     [SerializeField] LineRenderer lLine;
//     [SerializeField] LineRenderer rLine;
//     private Transform rController;
//     private Transform lController;
//     private bool rClicked;
//     private bool lClicked;
//     // Start is called before the first frame update
//     void Start()
//     {
//         appInfo = GetComponent<IAppInfo>();
//         rController = appInfo.GetRightController();
//         lController = appInfo.GetLeftController();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (rController == null) rController = appInfo.GetRightController();
//         if (lController == null) lController = appInfo.GetLeftController();
//         rClicked = appInfo.GetRightTriggerDown();
//         lClicked = appInfo.GetLeftTriggerDown();
//         checkRay(rController, rClicked, rLine);
//         checkRay(lController, lClicked, lLine);
//     }
//     void checkRay(Transform controller, bool clicked, LineRenderer line)
//     {
//         RaycastHit hit;
//         Debug.DrawRay(controller.position, controller.forward, Color.red);
//         if (Physics.Raycast(controller.position, controller.forward, out hit))
//         {
//             if (hit.collider.gameObject.GetComponent<Runnable>() != null)
//             {
//                 line.SetPosition(0, controller.position);
//                 line.SetPosition(1, hit.point);
//                 line.enabled = true;
//                 if (clicked)
//                 {
//                     hit.collider.gameObject.GetComponent<Runnable>().run();
//                 }
//             }
//         }
//         else
//         {
//             line.enabled = false;
//         }
//     }
// }