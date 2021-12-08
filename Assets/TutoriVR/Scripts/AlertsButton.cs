using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertsButton : MonoBehaviour, IRunnable
{
    public GameObject awareness_widget;
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
            awareness_widget.SetActive(false);
        }
        else
        {
            awareness_widget.SetActive(true);

        }
    }

   }
