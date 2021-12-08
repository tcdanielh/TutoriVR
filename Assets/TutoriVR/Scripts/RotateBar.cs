using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evereal.VRVideoPlayer;

public class RotateBar : BarBase, IRunnable
{
    private IAppInfo appInfo;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        appInfo = transform.parent.parent.parent.GetComponentInParent<IAppInfo>();
        //Debug.Log(appInfo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    public void Run(Vector3 currentPoint)
    {
        float currentWidth = Vector3.Distance(startPoint.position, currentPoint);
        float progress = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
        float ydegree = 360.0f * progress;
        Debug.Log(ydegree);
        target.transform.localEulerAngles = new Vector3(target.transform.localEulerAngles.x, ydegree, target.transform.localEulerAngles.z);
        SetProgress(progress);

    }
}
