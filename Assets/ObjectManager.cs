using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectManager : MonoBehaviour
{


    public ObjectMovement objectMovement;
    public ObjectCollisionSystem collision;
    public PlayerAnimation playerAnimation;
    //********** Enum
    public enum Direction {Down=-1,Up=1 }
    //*************

    public Direction gravityDirection;

    private Rigidbody2D objectToPull;
    private bool grounded;
    private int facing = 1;
    private int spaceDirection = 1;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    private void Awake()
    {
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }
   
    private void FixedUpdate()
    {
        if (collision.isGrounded((float)gravityDirection))
        {
            if(!grounded)
                OnLandEvent.Invoke();
            grounded = true;
        }
    }

    public void inversSpace()
    {
        spaceDirection *= -1;
    }

    public void move(float axisRaw, float time)
    {
        if (axisRaw * facing * spaceDirection < 0)
            flip();
        objectMovement.move(time * axisRaw * spaceDirection);
        playerAnimation.runOrStop(axisRaw);
        
        if (objectToPull != null)
            objectToPull.position += Vector2.right * objectMovement.runSpeed * objectMovement.speedModifer * time;
    }

    public void jump()
    {
        
        if (grounded)
        {
            objectMovement.jump();
            playerAnimation.jumpOrStop(true);
            grounded = false;
        }
    }

    //this called from Event
    public void land()
    {
        playerAnimation.jumpOrStop(false);
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


    private void flip()
    {
        facing *= -1;
        transform.localScale *= (Vector2.left + Vector2.up);
    }
}
