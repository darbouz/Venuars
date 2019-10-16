using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    public bool disabled;
    public float runSpeed = 2f;
    public float jumpVelocity = 5f;
    public PhysicsObject controlledObject;

    void Update()
    {
        if (!disabled)
        {
            controlledObject.targetXVelocity = Input.GetAxisRaw("Horizontal") * runSpeed;
            if (Input.GetButtonDown("Jump") && controlledObject.isGrounded())
                controlledObject.setYVelocity(jumpVelocity);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            disabled = !disabled;
            controlledObject.targetXVelocity = 0;
        }
    }
}
