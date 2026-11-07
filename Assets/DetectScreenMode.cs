using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectScreenMode : MonoBehaviour
{
    public static bool currentlyPortrait = false;
    public bool IsPortrait()
    {
        // If height is greater than width, the screen is in portrait layout.
        return Screen.height > Screen.width;
    }

    void Update()
    {
        if (IsPortrait() && !currentlyPortrait)
        {
            DetectScreenMode.currentlyPortrait = true;
            // load the portrait scene
            SceneManager.LoadScene(2);
            Debug.Log("The screen layout is Portrait.");
        }
        else if (!IsPortrait() && currentlyPortrait)
        {
            DetectScreenMode.currentlyPortrait = false;
            // load the landscape scene
            SceneManager.LoadScene(0);
            Debug.Log("The screen layout is Landscape.");
        }
            
    }
}
