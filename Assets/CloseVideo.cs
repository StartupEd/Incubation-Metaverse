using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CloseVideo : MonoBehaviour
{
    public GameObject videoPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeVideo()
    {
        videoPanel.SetActive(false);
    }
}
