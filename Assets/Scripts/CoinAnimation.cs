using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    public float rotatinSpeed = 1f;
    
    private void Update()
    {
        transform.Rotate(0, rotatinSpeed * Time.deltaTime,0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (InstructionHandler.instance.coinCollectPanel != null)
            {
                InstructionHandler.instance.ShowAndHidePanel();
            }

            Destroy(gameObject);
        }
    }
}
