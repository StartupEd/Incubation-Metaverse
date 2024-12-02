using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionHandler : MonoBehaviour
{

    public GameObject IntroPanel;
    public GameObject coinCollectPanel;


    public static InstructionHandler instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        IntroPanel.SetActive(true);
    }

  
    void Update()
    {
        
    }

    public void closeIntroPanel()
    {
        IntroPanel.SetActive(false);
    }

   

    public void ShowAndHidePanel()
    {
        coinCollectPanel.SetActive(true);
        StartCoroutine(HidePanelAfterDelay());
    }

    IEnumerator HidePanelAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        coinCollectPanel.SetActive(false);
    }

}
