using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ButtonInstance
{
    public float time;
    // private ?? script = script being run; --> scripts/brushcontroller ActiveBrush for brush
    public Vector3 leftControllerPos;
    public Vector3 rightControllerPos;
    public Quaternion leftControllerRot;
    public Quaternion rightControllerRot;
    public bool rightTriggerDown;
    public bool leftTriggerDown;
    private IAppInfo instance;
    public string additionalInfo;

    public Color color; //the trail color for the recreation video

    //public TiltBrush.BaseTool.ToolType tool;
    //public TiltBrush.BrushDescriptor brushtype;
    //private TiltBrush.ColorController colorcon;
    //private TiltBrush.SketchSurfacePanel toolobj;
    //public Color brushcolor; 

    public GameObject tracker;

    // gets data of current frame
    public ButtonInstance createInstance(IAppInfo appInfo, float timeStamp, GameObject tracker)
    {
        time = timeStamp;
        instance = appInfo;
        //brushtype = null;
        //colorcon = GameObject.Find("App").GetComponent<TiltBrush.BrushColorController>();
        //color = colorcon.CurrentColor;
        // script = check script that is running
        //if (TiltBrush.BrushController.m_Instance != null)
        //{
        //    brushtype = TiltBrush.BrushController.m_Instance.ActiveBrush;
        //}
        //Debug.Log(brushtype);
        //toolobj = GameObject.Find("SketchSurface").GetComponent<TiltBrush.SketchSurfacePanel>();
        //tool = toolobj.ActiveToolType;
        //Debug.Log(tool);
        //brushcolor = TiltBrush.ColorController.trackColor;
        //Debug.Log(brushcolor);
        additionalInfo = instance.GetSerializedAdditionalInfo();
        color = instance.GetColor();
        Transform lc = instance.GetLeftController();
        Transform rc = instance.GetRightController();
        Transform root = instance.GetSceneRootTransform();
        (leftControllerPos, leftControllerRot) = getRelativePosRot(lc, root, tracker);
        (rightControllerPos, rightControllerRot) = getRelativePosRot(rc, root, tracker);
        //leftControllerPos = instance.GetLeftController().position;
        //rightControllerPos = instance.GetRightController().position;
        //leftControllerRot = instance.GetLeftController().rotation;
        //rightControllerRot = instance.GetRightController().rotation;
        ButtonStatus r = instance.GetRightTriggerStatus();
        ButtonStatus l = instance.GetLeftTriggerStatus();
        rightTriggerDown = (r == ButtonStatus.Down || r == ButtonStatus.Held);
        leftTriggerDown = (l == ButtonStatus.Down || l == ButtonStatus.Held);
        //Debug.Log("Default: " + leftControllerPos + " | " + leftControllerRot);
        return this;
    }

    private (Vector3, Quaternion) getRelativePosRot(Transform t, Transform root, GameObject tracker)
    {
        //return (Vector3.zero, Quaternion.identity);
        GameObject obj = GameObject.Instantiate(tracker, t.position, t.rotation);
        obj.transform.SetParent(root, true);
        Vector3 pos = obj.transform.localPosition;
        Quaternion rot = obj.transform.localRotation;
        GameObject.DestroyImmediate(obj);
        //Debug.Log(pos + " " + rot);
        return (pos, rot);
    }
}
