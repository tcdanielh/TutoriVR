using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// [RequireComponent(typeof(Camera))]
public class SetPlayerPos : MonoBehaviour
{

    // private IAppInfo appInfo;

    // [SerializeField] VideoCapture VC;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3 (1,0,-80);
    }
    
}
