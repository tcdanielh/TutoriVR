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

public class TutoriRaycastManager : MonoBehaviour
{
    [SerializeField] IAppInfo appInfo;
    [SerializeField] LineRenderer lLine;
    [SerializeField] LineRenderer rLine;
    private Transform rController;
    private Transform lController;
    private ButtonStatus rStat;
    private ButtonStatus lStat;

    private Dictionary<GameObject, Color> regColor = new Dictionary<GameObject, Color>();
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
        //lStat = appInfo.GetLeftTriggerStatus();
        GameObject o1 = checkRay(rController, rStat, rLine);
        //GameObject o2 = checkRay(lController, lStat, lLine);

        List<GameObject> toBeRemoved = new List<GameObject>();
        foreach (GameObject o in regColor.Keys)
        {
            //if (!o.Equals(o1) && !o.Equals(o2))
            //{
            //    o.GetComponent<Renderer>().material.color = regColor[o];
            //    toBeRemoved.Add(o);
            //}
            if (!o.Equals(o1))
            {
                o.GetComponent<Renderer>().material.color = regColor[o];
                toBeRemoved.Add(o);
            }
        }
        foreach (GameObject o in toBeRemoved)
        {
            regColor.Remove(o);
        }
    }
    GameObject checkRay(Transform controller, ButtonStatus status, LineRenderer line)
    {
        RaycastHit hit;
        Debug.DrawRay(controller.position, controller.forward, Color.red);
        if (Physics.Raycast(controller.position, controller.forward, out hit))
        {
            if (hit.collider.gameObject.GetComponent<IRunnable>() != null || hit.collider.gameObject.GetComponent<IRunnableHold>() != null)
            {
                line.SetPosition(0, controller.position);
                line.SetPosition(1, hit.point);
                line.enabled = true;
                GameObject obj = hit.collider.gameObject;
                if (obj.GetComponent<Renderer>() != null)
                {
                    Color c = obj.GetComponent<Renderer>().material.color;
                    if (!regColor.ContainsKey(obj))
                    {
                        regColor.Add(obj, c);
                    }
                    Color d = new Color(Mathf.Max(0, regColor[obj].r - .2f), Mathf.Max(0, regColor[obj].g - .2f), Mathf.Max(0, regColor[obj].b - .2f), regColor[obj].a);
                    obj.GetComponent<Renderer>().material.color = d;
                    if (status == ButtonStatus.Held)
                    {
                        d = new Color(Mathf.Max(0, regColor[obj].r - .4f), Mathf.Max(0, regColor[obj].g - .4f), Mathf.Max(0, regColor[obj].b - .4f), regColor[obj].a);
                        obj.GetComponent<Renderer>().material.color = d;
                    }
                }

                if (status == ButtonStatus.Held)
                {
                    Debug.Log(obj.name + "  " + obj.GetComponent<IRunnable>());
                    obj.GetComponent<IRunnableHold>().RunHold(hit.point);
                }
                else if (status == ButtonStatus.Up)
                {
                    Debug.Log(obj.name + "  " + obj.GetComponent<IRunnable>());
                    obj.GetComponent<IRunnable>().Run(hit.point);
                }
                return obj;
            }
        }
        else
        {
            line.enabled = false;
        }
        return null;
    }
}
