using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationHandler : MonoBehaviour
{

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Stay()
    {
        animator.SetBool("Aiming", false);
        animator.SetFloat("Speed", 0f);
    }

    public void Walk()
    {
        animator.SetBool("Aiming", false);
        animator.SetFloat("Speed", 0.5f);
    }

    public void Run()
    {
        animator.SetBool("Aiming", false);
        animator.SetFloat("Speed", 1f);
    }
}
