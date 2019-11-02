using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisionSystem : MonoBehaviour
{

    private BoxCollider2D objectBoxCollider;
    private float shellRadius = 0.255f;

    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask draggables;

    void OnEnable()
    {
        objectBoxCollider = GetComponent<BoxCollider2D>();
    }

    public bool isGrounded(float direction)
    {
        return detectBoxCollision(Vector2.up * direction, ground).collider != null;
    }

    public Rigidbody2D getDraggablesRigidBody()
    {
        return detectBoxCollision(Vector2.right, draggables).rigidbody;
    }
    private RaycastHit2D detectBoxCollision(Vector2 direction,LayerMask layer)
    {
       
        Vector2 size = objectBoxCollider.bounds.extents - (Vector3.right * 0.1f);
        Debug.DrawRay(objectBoxCollider.bounds.center, Vector2.up * (size.y + shellRadius), Color.red);
        Debug.DrawRay(objectBoxCollider.bounds.center, Vector2.right * (size.x), Color.red);
        return Physics2D.BoxCast(objectBoxCollider.bounds.center, size, 0f, direction, shellRadius, layer);
    }
}
