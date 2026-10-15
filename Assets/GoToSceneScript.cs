using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSceneScript : MonoBehaviour
{
    void GoToScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
