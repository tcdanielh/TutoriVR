using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwarenessWidget : MonoBehaviour
{
    private IAppInfo appInfo;
    [SerializeField] private Vector3 canvasPos;
    [SerializeField] private Vector3 canvasRot;

    // Start is called before the first frame update
    void Start()
    {
        appInfo = transform.parent.GetComponentInParent<IAppInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (appInfo.GetHead() != null && !transform.IsChildOf(appInfo.GetHead()))
        {
            transform.parent = appInfo.GetHead();
            transform.GetComponent<RectTransform>().localPosition = canvasPos;
            transform.GetComponent<RectTransform>().localEulerAngles = canvasRot;
        }
    }
}
