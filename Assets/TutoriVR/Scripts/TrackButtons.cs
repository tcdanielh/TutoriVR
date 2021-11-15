using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackButtons : MonoBehaviour
{
    private Dictionary<int, ButtonInstance> captures; //dictionary: frame to button instance class
    // Start is called before the first frame update
    void Start()
    {
        captures = new Dictionary<int, ButtonInstance>();
    }

    // Update is called once per frame
    void Update()
    {
        captures[Time.frameCount] = ButtonInstance.createInstance();
    }
}
