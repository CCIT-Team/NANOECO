using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stdioMon : MonoBehaviour
{
    public AnimationClip clip;
    public Animator animator;

    public void Update()
    {
        if (Input.GetKeyDown("L"))
        {
            animator.SetTrigger("T");
        }
    }
}
