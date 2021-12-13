using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    public GameObject L;
    public GameObject R;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 avgPosition = R.transform.position;
        //Debug.Log(avgPosition.z);
        //Debug.Log(avgPosition);
        transform.localPosition = new Vector3(avgPosition.x, avgPosition.y, avgPosition.z);
    }
}
