using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifeTime;
    float birth;
    // Start is called before the first frame update
    void Start()
    {
        birth = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= birth + lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
