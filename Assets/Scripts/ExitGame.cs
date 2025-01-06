using System.Collections;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    
    public void ExitApplication()
    {
       
        Application.Quit();

      
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
