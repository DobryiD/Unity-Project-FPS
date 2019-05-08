using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuActions : MonoBehaviour
{
    private bool buttonPressed=false;
    
    public void SceneChange(string sceneName) {
        if (!buttonPressed)
        {
            SceneChanger changer = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneChanger>();
            buttonPressed = true;
            changer.Fade(sceneName);
        }
    }
    public void QuitButtonPressed() {
        Application.Quit();
    }
}
