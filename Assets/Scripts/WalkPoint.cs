using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPoint : MonoBehaviour
{
    public Transform[] points; 
    public float moveSpeed = 2f; 
    public float arrivalThreshold = 0.1f; 
    public float rotationSpeed = 5f; 

    private int currentPointIndex = 0;
    public Transform player;
    public float detectionRange = 10f;
    public GameObject descriptionPanel;
    public Transform bodyTransform;
    public Animator animator;

    void Update()
    {
        if (IsPlayerInRange())
        {
            RotateBodyTowardPlayer();
            ShowDescriptionPanel(true);
            SetAnimation(false);
        }
        else
        {
            MoveToNextPoint();
            ShowDescriptionPanel(false);
            SetAnimation(true);
        }
    }
    bool IsPlayerInRange()
    {
        if (player == null)
        {
            return false;
        }

        return Vector3.Distance(transform.position, player.position) <= detectionRange;
    }

    void MoveToNextPoint()
    {
       
        Transform targetPoint = points[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        if (direction.magnitude > 0.01f) 
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

       
        if (Vector3.Distance(transform.position, targetPoint.position) <= arrivalThreshold)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Length;
        }
    }

    void RotateBodyTowardPlayer()
    {
        if (bodyTransform == null)
        {
            Debug.LogError("Body Transform is not assigned!");
            return;
        }

        Vector3 direction = (player.position - transform.position).normalized;
        if (direction.magnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void ShowDescriptionPanel(bool isVisible)
    {
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(isVisible);
        }
    }

    void SetAnimation(bool isPlaying)
    {
        if (animator != null)
        {
            animator.SetBool("walk", isPlaying); // Assume the animation is controlled by a parameter "isWalking"
        }
    }
}
