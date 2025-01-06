using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Make this object persistent across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }



    public void LoadScene(int sceneIndex)
    {
        SceneHandler.instance.LoadScene(sceneIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneHandler.instance.LoadScene(sceneName);
    }
}
