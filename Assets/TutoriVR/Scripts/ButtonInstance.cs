using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ButtonInstance
{
    public float time;
    // private ?? script = script being run; --> scripts/brushcontroller ActiveBrush for brush
    public TiltBrush.BrushDescriptor brushtype;
    public Color brushcolor; //consider putting all the brush stuff in a list
    public TiltBrush.BaseTool.ToolType tool;
    public Vector3 leftControllerPos;
    public Vector3 rightControllerPos;
    public Quaternion leftControllerRot;
    public Quaternion rightControllerRot;
    public bool rightTriggerDown;
    public bool leftTriggerDown;
    public Color color;
    private IAppInfo instance;
    //private TiltBrush.BaseSelectionTool.SelectionObject toolobj;
    private TiltBrush.ColorController colorcon;
    private TiltBrush.SketchSurfacePanel toolobj;

    // gets data of current frame
    public ButtonInstance createInstance(IAppInfo appInfo, float timeStamp)
    {
        time = timeStamp;
        instance = appInfo;
        brushtype = null;
        //wdata = new TiltBrush.GrabWidgetData();
        //widgetcommand = new TiltBrush.MoveWidgetCommand();
        //gwidget = new TiltBrush.GrabWidget();
        //toolobj = new TiltBrush.BaseSelectionTool.SelectionObject();
        colorcon = GameObject.Find("App").GetComponent<TiltBrush.BrushColorController>();
        color = colorcon.CurrentColor;
        // script = check script that is running
        if (TiltBrush.BrushController.m_Instance != null)
        {
            brushtype = TiltBrush.BrushController.m_Instance.ActiveBrush;
        }
        Debug.Log(brushtype);
        /* if (TiltBrush.ColorController.CurrentColor.get() != null)
        {
            brushcolor = TiltBrush.ColorController.CurrentColor.get();
        }
        */
        toolobj = GameObject.Find("SketchSurface").GetComponent<TiltBrush.SketchSurfacePanel>();
        tool = toolobj.ActiveToolType;
        Debug.Log(tool);
        brushcolor = TiltBrush.ColorController.trackColor;
        Debug.Log(brushcolor);
        Transform lc = instance.GetLeftController();
        Transform rc = instance.GetRightController();
        Transform root = instance.GetSceneRootTransform();
        (leftControllerPos, leftControllerRot) = getRelativePosRot(lc, root);
        (rightControllerPos, rightControllerRot) = getRelativePosRot(rc, root);
        //leftControllerPos = instance.GetLeftController().position;
        //rightControllerPos = instance.GetRightController().position;
        //leftControllerRot = instance.GetLeftController().rotation;
        //rightControllerRot = instance.GetRightController().rotation;
        ButtonStatus r = instance.GetRightTriggerStatus();
        ButtonStatus l = instance.GetLeftTriggerStatus();
        rightTriggerDown = (r == ButtonStatus.Down || r == ButtonStatus.Held);
        leftTriggerDown = (l == ButtonStatus.Down || l == ButtonStatus.Held);
        Debug.Log("Default: " + leftControllerPos + " | " + leftControllerRot);
        return this;
    }

    private (Vector3, Quaternion) getRelativePosRot(Transform t, Transform root)
    {
        GameObject obj = GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), t.position, t.rotation);
        obj.transform.SetParent(root, true);
        Vector3 pos = obj.transform.localPosition;
        Quaternion rot = obj.transform.localRotation;
        GameObject.Destroy(obj);
        return (pos, rot);
    }
}
