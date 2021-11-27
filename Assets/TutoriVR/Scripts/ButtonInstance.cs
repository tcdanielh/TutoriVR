using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInstance : MonoBehaviour
{
    // private ?? script = script being run; --> scripts/brushcontroller ActiveBrush for brush
    public TiltBrush.BrushDescriptor brushtype;
    public Color brushcolor; //consider putting all the brush stuff in a list
    public GameObject tool;
    private Transform leftControllerPos;
    private Transform rightControllerPos;
    private bool rightTriggerDown;
    private bool leftTriggerDown;
    private TiltbrushAppInfo instance;
    private TiltBrush.BaseSelectionTool.SelectionObject toolobj;
    private TiltBrush.ColorController colorcon;

    // Start is called before the first frame update
    void Start()
    {
        instance = new TiltbrushAppInfo();
        brushtype = null;
        //wdata = new TiltBrush.GrabWidgetData();
        //widgetcommand = new TiltBrush.MoveWidgetCommand();
        //gwidget = new TiltBrush.GrabWidget();
        toolobj = new TiltBrush.BaseSelectionTool.SelectionObject();
        colorcon = new TiltBrush.ColorController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // gets data of current frame
    public ButtonInstance createInstance()
    {
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
        //tool = toolobj.m_Object;
        //Debug.Log(tool);
        //brushcolor = TiltBrush.ColorController.trackColor;
        Debug.Log(brushcolor);
        leftControllerPos = instance.GetLeftController();
        rightControllerPos = instance.GetRightController();
        // rightTriggerDown = instance.GetRightTriggerDown();
        // leftTriggerDown = instance.GetLeftTriggerDown();
        return this;
    }
}
