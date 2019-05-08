using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveAlarm : MonoBehaviour {

    public Text text;
    private CanvasGroup canvas;
    private GameObject gameControllerObject;
    private GameController gameControllerScript;
    private bool NoGameController=true;
    private Animator animator;

    // Use this for initialization
    void Start () {
        canvas = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if (gameControllerObject == null&&NoGameController)
        {
            gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
            if (gameControllerObject == null)
            {
                NoGameController = true;
            }
            else
            {
                gameControllerScript = gameControllerObject.GetComponent<GameController>();
                NoGameController = false;
            }
        }
        else if(gameControllerObject!=null && !NoGameController)
        {
            if (gameControllerScript.GetRestingTime() <= 8)
            {

                text.text = "Wave incoming!";
                StartCoroutine(Fade());
                
            }
            else if (gameControllerScript.GetWave() >= gameControllerScript.MaxWaves && gameControllerScript.GetNumberZombie() <= 0) {
                
                
                text.text = "You survived!";
                StartCoroutine(Fade());
                
            }
        }
    }
    
    IEnumerator Fade() {
        animator.SetBool("Active", true);
        yield return new WaitForSeconds(4f);
        animator.SetBool("Active", false);
        yield return null;
    }
    
}
