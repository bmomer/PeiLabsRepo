using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private bool phoneAppeared = false;
    Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(phoneAppeared)
            {
                animator.Play("phoneDisappear");
                phoneAppeared = false;
            }

            else
            {
                animator.Play("phoneAppear");
                phoneAppeared = true;
            }
        }
    }
}
