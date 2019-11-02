using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;

    public void runOrStop(float axisRaw)
    {
        animator.SetFloat("speed", Mathf.Abs(axisRaw));
    }

    public void jumpOrStop(bool isJumping)
    {
        //Hna fin dir dak l3ob dyal animator.setBool()
        Debug.Log("Hey I'am jumping ? " + isJumping);
    }

}
