using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsStopped = false;
    public GameObject OptionMenu;
    public GameObject ButtonMenu;
    public GameObject PauseMenuUI;
    public Text ResolutionShow;
    public Slider volumeSlider;
    public Text volumeCounter;
    public Toggle fullscreenToggle;
    private int currentResolution;
    private string currentPlaySceneName;
    private float currentVolume;
    private bool settingsOpened = false;
    private bool FullScreenMode;
    private GameObject sceneChanger;

    string[] ArrayOfResolutions = { "1280x720", "1600x900","1920x1080" };
    
    void Awake()
    {
        sceneChanger = GameObject.FindGameObjectWithTag("SceneController");
        ShowDefaultSettings();
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsStopped)
            {
                Resume();
            }
            else {
                PauseGame();
            }
        }
        if (GameIsStopped && settingsOpened) {
            SetVolumeOnSlider();
            //ShowCurrentResol();
        }
    }
    private void ShowDefaultSettings()
    {
        string res = Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString();
        fullscreenToggle.isOn = Screen.fullScreen;
        volumeSlider.value = AudioListener.volume;
        volumeCounter.text = (volumeSlider.value * 100).ToString();
        for (int i = 0; i < ArrayOfResolutions.Length; i++)
        {
            if (ArrayOfResolutions[i].Equals(res))
            {
                currentResolution = i;
                ResolutionShow.text = ArrayOfResolutions[currentResolution];
            }
        }
    }
    private void ShowCurrentResol() {
        string res = Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString();
        ResolutionShow.text = res;
    }
    private void SetVolumeOnSlider() {
        currentVolume = volumeSlider.normalizedValue;
        volumeCounter.text = (currentVolume * 100).ToString();
    }
    public void SetFullScreen() {
        FullScreenMode = fullscreenToggle.isOn;
    }
    public bool isStopped() { return GameIsStopped; }
    public void Resume() {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsStopped = false;
        Cursor.visible = false;
    }
    public void PauseGame() {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        
        GameIsStopped = true;
        Cursor.visible = true;
    }
    public void IncreaseResolution() {
        if (currentResolution <= ArrayOfResolutions.Length-1) {
            ++currentResolution;
            ResolutionShow.text = ArrayOfResolutions[currentResolution];
        }
    }
    public void DecreaseResolution()
    {
        if (currentResolution >= 0)
        {
            --currentResolution;
            ResolutionShow.text = ArrayOfResolutions[currentResolution];
        }
    }
    public void OpenSettings() {
        ButtonMenu.SetActive(false);
        OptionMenu.SetActive(true);
        settingsOpened = true;
    }
    public void ApplyChanges() {
        string[] res = ArrayOfResolutions[currentResolution].Split('x');
        Screen.SetResolution(int.Parse(res[0]),int.Parse(res[1]),FullScreenMode,60);
    }
    public void OpenMenu()
    {
        ButtonMenu.SetActive(true);
        OptionMenu.SetActive(false);
        settingsOpened = false;
    }
    public void GoToMainMenu() {
        Resume();
        sceneChanger.GetComponent<SceneChanger>().Fade("MainMenu");
        Cursor.visible = true;
    }
    public void VolumeChange() {
        AudioListener.volume = volumeSlider.value;
    }

}

