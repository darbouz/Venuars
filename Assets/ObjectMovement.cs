using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{

    public float runSpeed = 8f;
    public float jumpForce = 250f;

    public Rigidbody2D rb2d { set; get; }
    public float speedModifer { set; get; } = 1; 

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void move(float time)
    {
        rb2d.position += runSpeed * time * Vector2.right * speedModifer;
    }
    public void jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce);
    }
    
}

