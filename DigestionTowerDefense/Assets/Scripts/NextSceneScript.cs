using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{
    
    /***
     * This function is used by the title screen button to load the next scene
     */
    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;
        
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        if (nextIndex < totalScenes)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
            
        }
    }
}
