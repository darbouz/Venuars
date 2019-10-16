using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public float runSpeed = 2f;
    public float jumpVelocity = 5f;
    public PhysicsObject controlledObject;
 
    void Update()
    {
        controlledObject.targetXVelocity = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump") && controlledObject.isGrounded())
            controlledObject.setYVelocity(jumpVelocity);        
    }
}
