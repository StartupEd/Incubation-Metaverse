using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideAnimation : MonoBehaviour
{
  
    public Animator animator;

    public GameObject instructionPanel;
    public GameObject officeInstructionPanle;
    //public string animationTrigger = "talk";

    private string animationStateBool = "talk";


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collide");

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("aayaa");
            instructionPanel.SetActive(true);
            animator.SetBool(animationStateBool, true);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("collide exit");

        if (collision.gameObject.CompareTag("Player"))
        {
            instructionPanel.SetActive(false);
            Debug.Log("gaya");
            animator.SetBool(animationStateBool, false);
        }
    }
}
