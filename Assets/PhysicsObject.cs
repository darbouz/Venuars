using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    [SerializeField] LayerMask colliders;
    public enum Direction {Up = -1,Down = 1};
    public Direction gravityDirection;

    public float gravityModifier = 1f;
    public float targetXVelocity {get;set; }
    
    
    protected Vector2 velocity;
    protected BoxCollider2D boxCollider2D;
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected const float shellRadius = 0.01f;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        targetXVelocity = 0;
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        velocity += Physics2D.gravity * gravityModifier * (float)gravityDirection * Time.fixedDeltaTime;
        velocity.x = targetXVelocity;
        Vector2 deltaPosition = velocity * Time.fixedDeltaTime;
       
        moveX(deltaPosition * Vector2.right);
        moveY(deltaPosition * Vector2.up);
    }

    private void moveX(Vector2 movementDirection)
    {
        float distance = detectCollision(movementDirection);
        rb2d.position = rb2d.position + movementDirection.normalized * distance;
    }
    private void moveY(Vector2 movementDirection)
    {
        float distance = detectCollision(movementDirection);
        rb2d.position = rb2d.position + movementDirection.normalized * distance;
    }

    private float detectCollision(Vector2 movementDirection)
    {
        float distance = movementDirection.magnitude;
        //Check if the next Movement will be a collision with the surface
        int count = rb2d.Cast(movementDirection, contactFilter, hitBuffer, distance + shellRadius);

        for (int i = 0; i < count; i++)
        {
            //Modify the distance to shellRadius
            float modifiedDistance = hitBuffer[i].distance - shellRadius;
            distance = modifiedDistance < distance ? modifiedDistance : distance;
        }
        return distance;
    }
    public bool isGrounded()
    {
        RaycastHit2D castRay = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down * (float)gravityDirection, shellRadius, colliders);
        return castRay.collider != null;
    }

    public void setYVelocity(float Yvelocity)
    {
        velocity.y = Yvelocity * (float)gravityDirection;
    }
}

