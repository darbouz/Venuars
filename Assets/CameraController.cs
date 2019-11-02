using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 rotation;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)){
            transform.Rotate(0, 0, 180, Space.Self);
        }
    }
}
