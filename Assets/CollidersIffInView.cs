using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersIffInView : MonoBehaviour
{
    private IAppInfo appInfo;
    Transform h;
    // Start is called before the first frame update
    void Start()
    {
        appInfo = GetComponentInParent<IAppInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (h != null)
        {
            Vector3 headToSelf = transform.position - h.position;
            float angle = Vector3.Angle(headToSelf, transform.forward) % 360;
            //Debug.Log(angle);
            SetCollidersEnabled(angle < 90 || angle > 270);
        } else
        {
            appInfo = GameObject.Find("TutoriWidgets").GetComponent<IAppInfo>();
            Debug.Log("appinfo = " + appInfo);
            h = appInfo.GetHead();
        }
    }

    void SetCollidersEnabled(bool setting)
    {
        if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = setting;
        //foreach (Collider c in GetComponentsInChildren<Collider>())
        //{
        //    c.enabled = setting;
        //}
    }
}
