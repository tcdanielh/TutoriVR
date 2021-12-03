using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHMD : MonoBehaviour
{
    public Transform HMD;
    public float xOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = HMD.position;
        transform.rotation = HMD.rotation;

        transform.position += transform.right * xOffset;
    }
}
