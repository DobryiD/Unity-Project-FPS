using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounting : MonoBehaviour {
    public Text waveCounter;
    public Text enemyCounter_timer;
    public Image biohazardSign;
    private CanvasGroup group;
    private GameObject gameControllerObject;
    private GameController gameControllerScript;
    private bool NoGameController = true;
    private bool WavesActivated = false;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update () {
        if (WavesActivated) {
            group.alpha = 1f;
        }
        if (gameControllerObject == null && NoGameController)
        {
            gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
            if (gameControllerObject == null)
            {
                NoGameController = true;
                group.alpha = 0f;
            }
            else
            {
                gameControllerScript = gameControllerObject.GetComponent<GameController>();
                NoGameController = false;
            }
        }
        else if (gameControllerObject != null)
        {
            waveCounter.text = gameControllerScript.GetWave() + "/" + gameControllerScript.MaxWaves;
            WavesActivated = gameControllerScript.firstWaveStarted;
            if (gameControllerScript.isWaveActive())
            {
                biohazardSign.color = new Color(1f, 1f, 1f, 1f);
                enemyCounter_timer.text = gameControllerScript.GetNumberZombie().ToString();
            }
            else if (!gameControllerScript.firstWaveStarted)
            {
                biohazardSign.color = new Color(1f, 1f, 1f, 0f);
                enemyCounter_timer.text = "";
            }
            else
            {
                biohazardSign.color = new Color(1f, 1f, 1f, 0f);
                int minutes = (int)gameControllerScript.GetRestingTime() / 60;
                int seconds = (int)gameControllerScript.GetRestingTime() % 60;
                enemyCounter_timer.text = minutes + " : " + seconds;
            }
        }
	}
}
