using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;
    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        velocity += Physics2D.gravity * gravityModifier * Time.fixedDeltaTime;
        Vector2 deltaPosition = velocity * Time.fixedDeltaTime;
        Vector2 movementDirection = deltaPosition * Vector2.up;

        move(movementDirection);
    }

    private void move(Vector2 movementDirection)
    {
        float distance = movementDirection.magnitude;
        
        int count = rb2d.Cast(movementDirection, contactFilter, hitBuffer, distance + shellRadius);

        for (int i = 0; i < count; i++)
        {
            float modifiedDistance = hitBuffer[i].distance - shellRadius;
            distance = modifiedDistance < distance ? modifiedDistance : distance;
        }
        
        rb2d.position = rb2d.position + movementDirection.normalized * distance;
    }
    private void move2(Vector2 move,bool yMovement)
    {
        float distance = move.magnitude;
        

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }
        rb2d.position = rb2d.position + move.normalized * distance;
    }
}

