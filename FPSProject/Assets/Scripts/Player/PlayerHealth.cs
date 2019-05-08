using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float flashTime=5f;
    public int maxHealth = 150;
    public int maxArmor = 125;
    public Camera[] camerasOff;
    public Camera deathCam;
    private int currentHealth;
    private int currentArmor;
    private Animator anim;
    private bool dead = false;
    private GameObject bloodEffect;
    private Image bloodScreen;
    private bool damaged;
    private GameObject deadScreen;
    
    
    
    void Start()
    {
        currentHealth = maxHealth;
        currentArmor = 25;
        anim = GetComponent<Animator>();
        bloodScreen = GameObject.FindGameObjectWithTag("UIblood").GetComponent<Image>();
        deadScreen = GameObject.Find("DeadScreen");
        
    }

    
    void Update()
    {

        if (currentHealth <= 0 &&!dead)
        {
            foreach (Camera cam in camerasOff)
            {
                cam.gameObject.SetActive(false);
            }
            deathCam.gameObject.SetActive(true);
            anim.SetTrigger("Death");
            anim.SetLayerWeight(anim.GetLayerIndex("WeaponUp"),0);
            dead = true;
            if (deadScreen != null) {
                deadScreen.GetComponent<Animator>().CrossFade("DeadScreenAnim",0f,0);
            }
            //Destroy(gameObject, 20f);
        }
        if (damaged&& bloodScreen!=null)
        {
            bloodScreen.color = new Color(1f, 1f, 1f, 0.4f);
            damaged = false;
        }
        else {
            if (currentHealth > (maxHealth / 10) * 4 && bloodScreen != null)
            {
                bloodScreen.color = Color.Lerp(bloodScreen.color, Color.clear, flashTime * Time.deltaTime);
            }
            
        }

    }
    public void HealHealth(int amount) {
        if (currentHealth > 0) {
            currentHealth += amount;
        }
    }
    public void RestoreArmor(int amount)
    {
         currentArmor += amount;
    }

    public void GetDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentArmor -= damage / 2;
            if (currentArmor < 0)
            {
                currentHealth -= ((damage / 2) + Mathf.Abs(currentArmor));
                currentArmor = 0;
            }
            else {
                currentHealth -= damage / 2;
            }
            
            damaged = true;
            
        }
    }
   
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public int GetCurrentArmor() {
        return currentArmor;
    }
}
