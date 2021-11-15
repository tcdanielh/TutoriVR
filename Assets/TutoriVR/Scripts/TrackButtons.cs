using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackButtons : MonoBehaviour
{
    private Dictionary<int, ButtonInstance> captures; //dictionary: frame to button instance class
    private ButtonInstance binstance;

    // Start is called before the first frame update
    public void Start()
    {
        captures = new Dictionary<int, ButtonInstance>();
    }

    // Update is called once per frame
    public void Update()
    {
        binstance = new ButtonInstance();
        captures[Time.frameCount] = binstance.createInstance();
        Debug.Log(Time.frameCount + " " + captures[Time.frameCount].brushtype);
    }
}
