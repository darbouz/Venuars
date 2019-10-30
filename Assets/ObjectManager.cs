using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{


    public ObjectMovement objectMovement;
    public ObjectCollisionSystem collision;
    public enum Direction {Down=-1,Up=1 }
    public Direction gravityDirection;
    

    private Rigidbody2D objectToPull;

    public void move(float time)
    {
        objectMovement.move(time);
        if (objectToPull != null)
            objectToPull.position += Vector2.right * objectMovement.runSpeed * objectMovement.speedModifer * time;
    }

    public void jump()
    {
        if (collision.isGrounded((float)gravityDirection))
            objectMovement.jump();
    }

    public void pull()
    {
        objectToPull = collision.getDraggablesRigidBody();
        if (objectToPull != null)
            objectMovement.speedModifer *= objectMovement.rb2d.mass / (objectToPull.mass + objectMovement.rb2d.mass);
    }
    public void release()
    {
        objectToPull = null;
        objectMovement.speedModifer = 1;
    }
}
