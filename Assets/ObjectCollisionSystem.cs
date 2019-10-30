﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisionSystem : MonoBehaviour
{

    private BoxCollider2D objectBoxCollider;
    private float shellRadius = 0.01f;

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
        return Physics2D.BoxCast(objectBoxCollider.bounds.center, objectBoxCollider.bounds.size, 0f, direction, shellRadius, layer);
    }
}
