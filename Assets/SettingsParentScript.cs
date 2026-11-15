using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsParentScript : MonoBehaviour
{
    public GameObject SettingsUI;
    public Texture2D[] backgroundTextures;
    public RawImage BackgroundRawImage;
    int currentBackgroundIndex = 0;

    public void cycleBackground()
    {
        currentBackgroundIndex++;
        if (currentBackgroundIndex >= backgroundTextures.Length)
        {
            currentBackgroundIndex = 0;
            Debug.Log("currentBackgroundIndex reset to 0");
        }
        BackgroundRawImage.texture = backgroundTextures[currentBackgroundIndex];
    }

    public void toggleSettingsUI()
    {
        SettingsUI.SetActive(!SettingsUI.activeSelf);
    }
}
