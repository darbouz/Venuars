using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    public bool disabled;
    public ObjectManager objectManager;
    
    private float axisRaw;
    void Update()
    {
        if (!disabled)
        {
            axisRaw = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
                objectManager.jump();

            if (Input.GetKeyDown(KeyCode.E))
                objectManager.pull();
            if (Input.GetKeyUp(KeyCode.E))
                objectManager.release();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            disabled = !disabled;
            axisRaw = 0;
        }
    }

  

    void FixedUpdate()
    {
        objectManager.move(axisRaw,Time.fixedDeltaTime);
    }
}
