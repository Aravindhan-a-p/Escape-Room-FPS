using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private Animator animator;
    private ButtonManager buttonManager;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void openDoor()
    {
        animator.SetTrigger("OPEN_DOOR");
    }
}
