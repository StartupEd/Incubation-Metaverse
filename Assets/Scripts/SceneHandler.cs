using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler instance;

    [SerializeField] SceneData[] sceneData;
    //[SerializeField] private int loadingPanelSceneIndex; // Define the scene index for the loading panel

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

    public void LoadingPanel()
    {
       
            SceneLoader.instance.LoadScene("LoadingPanel");
    }

    public void LoadScene(int sceneIndex)
    {
        bool sceneFound = false;

        for (int i = 0; i < sceneData.Length; i++)
        {
            if (sceneData[i].sceneIndex == sceneIndex)
            {
                Debug.Log("Local scene- " + sceneIndex);
                SceneManager.LoadScene(sceneIndex);
                sceneFound = true;
                break;
            }
        }

        if (!sceneFound)
        {
            Debug.LogWarning("Scene with index " + sceneIndex + " not found in sceneData.");
        }
    }

    public void LoadScene(string sceneName)
    {
        bool sceneFound = false;

        for (int i = 0; i < sceneData.Length; i++)
        {
            if (sceneData[i].sceneName == sceneName)
            {
                Debug.Log("Local scene- " + sceneName);
                SceneManager.LoadScene(sceneName);
                sceneFound = true;
                break;
            }
        }

        if (!sceneFound)
        {
            Debug.LogWarning("Scene with name " + sceneName + " not found in sceneData.");
        }
    }
}

[System.Serializable]
public class SceneData
{
    public int sceneIndex;
    public string sceneName;
}
