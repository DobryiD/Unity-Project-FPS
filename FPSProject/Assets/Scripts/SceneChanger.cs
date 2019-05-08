using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public static SceneChanger Instance;
    public string startingSceneName = "MainMenu";
    public Animator fade;
    private string sceneName;
    

    void Awake() {
        
            StartCoroutine(LoadSceneAndSetActive(startingSceneName));
    }
    public void Fade(string name)
    {
        sceneName = name;
        fade.SetTrigger("FadeOut");
    }

    public IEnumerator FadeAndSwitchScene() {
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        fade.SetTrigger("FadeIn");
        
    }

    private IEnumerator LoadSceneAndSetActive(string name)
    {
        yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);

    }
    public string SetActiveScene(string name) {
        string currentPlayScene = SceneManager.GetActiveScene().name;
        Scene myScene = SceneManager.GetSceneByName(name);
        SceneManager.SetActiveScene(myScene);
        return currentPlayScene;
    }

}
