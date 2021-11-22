using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOnRaycast : MonoBehaviour
{
    [SerializeField] IAppInfo appInfo;
    [SerializeField] LineRenderer line;
    [SerializeField] Runnable function;
    private Transform rController;
    private Transform lController;
    // Start is called before the first frame update
    void Start()
    {
        if (function == null)
        {
            function = gameObject.GetComponent<Runnable>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkRay(Transform controller)
    {
        RaycastHit hit;
        if (Physics.Raycast(controller.position, controller.forward, out hit)){
            if (hit.collider.gameObject.Equals(gameObject))
            {
                line.SetPosition(0, appInfo.GetRightController().position);
                line.SetPosition(1, hit.point);
                line.enabled = true;
                if (appInfo.GetRightTriggerDown())
                {
                    function.run();
                }
            }
        }
        else
        {
            line.enabled = false;
        }
    }
}
