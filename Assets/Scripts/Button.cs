using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isActive;
    public Material buttonPressed;
    private Animator animator;

    void Start()
    {
        isActive = false;
        animator = GetComponent<Animator>();
    }
 
    void Update()
    {
        if(isActive)
        {
            gameObject.GetComponent<Renderer>().material = buttonPressed;
            animator.SetTrigger("BUTTON_PRESS");
        }
    }
}
