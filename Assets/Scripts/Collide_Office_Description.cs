using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide_Office_Description : MonoBehaviour
{
    public Animator animator;

   
    public GameObject officeInstructionPanle;
    //public string animationTrigger = "talk";

    private string animationStateBool = "talk";


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collide");

        if (collision.gameObject.CompareTag("Player"))
        {
            officeInstructionPanle.SetActive(true);
            Debug.Log("aayaa");
            animator.SetBool(animationStateBool, true);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("collide exit");

        if (collision.gameObject.CompareTag("Player"))
        {
            officeInstructionPanle.SetActive(false);
            Debug.Log("gaya");
            animator.SetBool(animationStateBool, false);
        }
    }
}
