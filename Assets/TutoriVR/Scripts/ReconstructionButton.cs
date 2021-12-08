using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReconstructionButton : MonoBehaviour, IRunnable
{
    public GameObject perspective_widget;
    private bool currentstate;
    // Start is called before the first frame update
    void Start()
    {

        currentstate = false;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void Run(Vector3 currentPoint)
    {
        currentstate = !currentstate;
        if (currentstate == false)
        {
            perspective_widget.SetActive(false);
        }
        else
        {
            perspective_widget.SetActive(true);

        }
    }
}
