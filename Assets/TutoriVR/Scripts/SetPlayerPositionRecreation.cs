using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPositionRecreation : MonoBehaviour, IRunnableHold
{
    private IAppInfo appInfo;
    private bool held;
    // Start is called before the first frame update
    void Start()
    {
        appInfo = GetComponentInParent<IAppInfo>();

        held = false;
        // transform.position = new Vector3 (1,0,-80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Held()
    {
        //Debug.Log("held 1");
        while (appInfo.GetUnusedButtonStatus() == ButtonStatus.Held)
        {
            transform.parent.parent = appInfo.GetRightController();
            yield return null;
            transform.parent.parent = GameObject.Find("TutoriWidgets").transform;

        }
    }

    IEnumerator UnHeld()
    {
        while (appInfo.GetUnusedButtonStatus() != ButtonStatus.Held)
        {
            transform.parent = GameObject.Find("TutoriWidgets").transform;
            yield return null;
        }
    }

    //public void Run(Vector3 currentpoint)
    //{
    //    held = !held;
    //    // appInfo = GetComponentInParent<IAppInfo>();
    //    // if (rController == null) rController = appInfo.GetRightController();
    //    // if (lController == null) lController = appInfo.GetLeftController();
    //    if (held)
    //    {
    //        transform.parent.parent = appInfo.GetRightController();
    //    } else
    //    {
    //        transform.parent.parent = GameObject.Find("TutoriWidgets").transform;
    //    }

    //    // yield return new WaitForSeconds(5);

    //}

    public void RunHold(Vector3 currentpoint)
    {

        Debug.Log("rstat held");
        StartCoroutine(Held());

    }
}
