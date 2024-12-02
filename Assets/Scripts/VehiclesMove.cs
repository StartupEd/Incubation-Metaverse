using System.Collections;
using UnityEngine;

public class VehiclesMove : MonoBehaviour
{
    public Transform startPoint;    
    public Transform endPoint;      
    public float speed ;       
    public float turnSpeed = 2f;    

    private bool movingToEnd = true; 
    private bool isTurning = false;
    private bool isStopped = false;

    void Update()
    {
        if (isStopped) return;
        if (!isTurning) 
        {
            if (movingToEnd)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
                {
                    StartCoroutine(TurnTowards(startPoint.position)); 
                    movingToEnd = false; 
                }
            }
            else
            {
               
                transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, startPoint.position) < 0.1f)
                {
                    StartCoroutine(TurnTowards(endPoint.position)); 
                    movingToEnd = true; 
                }
            }
        }
    }

    private IEnumerator TurnTowards(Vector3 targetPosition)
    {
        isTurning = true; 
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation; 
        isTurning = false; 
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isStopped = true; // Stop movement when colliding with the player
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isStopped = false; // Resume movement when the player exits the collision
        }
    }
}
