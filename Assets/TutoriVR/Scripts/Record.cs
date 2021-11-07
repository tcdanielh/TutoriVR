using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour, Runnable
{
    [SerializeField] Material recordButton;
    [SerializeField] Material stopButton;
    private bool recording;
    // Start is called before the first frame update
    void Start()
    {
        recording = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (recording)
        {

        }
    }

    public void run()
    {
        if (recording)
        {
            recording = false;
            gameObject.GetComponent<Renderer>().material = recordButton;
            Debug.Log("Save & Export");
        }
        else
        {
            recording = true;
            gameObject.GetComponent<Renderer>().material = stopButton;
            Debug.Log("Start Recording");
        }
    }
}
