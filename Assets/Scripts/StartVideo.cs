using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVideo : MonoBehaviour
{
    public static StartVideo instance;

    public GameObject gamevideoActive;

    public float activationDelay = 11f;

    void Awake()
    {
        instance = this;
    }

    public void PlayStart()
    {

        StartCoroutine(ActivatePanelAfterDelay());
    }

    IEnumerator ActivatePanelAfterDelay()
    {

        yield return new WaitForSeconds(activationDelay);


        if (gamevideoActive != null)
        {
            gamevideoActive.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Panel to activate is not assigned!");
        }
    }



}
