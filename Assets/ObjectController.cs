using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    public Animator animator;

    public bool disabled;
    public string person;
    public ObjectManager objectManager;
    
    private float axisRaw;
    void Update()
    {
        if (!disabled)
        {
            axisRaw = Input.GetAxisRaw("Horizontal");
            if (person == "boy") {
                animator.SetFloat("speedBoy", Mathf.Abs(axisRaw));
            } else {
                animator.SetFloat("speedGirl", Mathf.Abs(axisRaw));
            }

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
            if (person == "boy") {
                animator.SetFloat("speedBoy", 0);
            } else {
                animator.SetFloat("speedGirl", 0);
            }
        }
    }

    public void OnLand()
    {
        Debug.Log("Landed");
    }

    void FixedUpdate()
    {
        objectManager.move(axisRaw * Time.fixedDeltaTime);
    }
}
