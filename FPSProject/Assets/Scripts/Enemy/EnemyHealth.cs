using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour {
    public int maxHealth=100;
    public bool crawler;
    public bool isTriggerZombie;
    public GameObject bloodSystem;
    protected int currentHealth;
    protected Animator anim;
    protected bool alive = true;
    protected int deathPose;
    protected bool boostUp=false;
    protected AudioSource source;
    protected GameController gameController;
    protected Collider[] colliders;


    // Use this for initialization
    protected void Awake () {
        currentHealth = maxHealth;
        colliders = GetComponentsInChildren<Collider>();
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        deathPose = Random.Range(1, 5);
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
    }
	
	// Update is called once per frame
	void Update () {
        
         if (currentHealth <= 0 && alive) {
            Dying();
        }
        
        
        
	}
    private void Dying() {
        anim.SetLayerWeight(anim.GetLayerIndex("Attacks"), 0);
        if (crawler)
        {
            anim.SetTrigger("Death");

        }
        else
        {
            anim.SetTrigger("Death" + deathPose);
        }
        if (!isTriggerZombie)
        {
            gameController.EnemyDies();
        }
        alive = false;
        foreach (var coll in colliders)
        {
            Destroy(coll);
        }
        Destroy(gameObject, 12f);
    }
    public virtual void GetDamage(string bodyPart,int damage)
    {
        
        if (currentHealth > 0)
        {
            if (bodyPart == "EnemyLegs")
            {
                currentHealth -= damage / 2;

            }
            else if (bodyPart == "EnemyHead")
            {
                currentHealth -= damage * 2;
            }
            else if (bodyPart == "EnemyBody") {
                currentHealth -= damage;
            }
            else if (bodyPart == "EnemyArms")
            {
                currentHealth -= damage / 2;
            }
            
            
        }

    }

    public int GetCurrentHealth() {
        return currentHealth;
    }
    public void Bleeding(Vector3 position,Quaternion rotat)
    {
        GameObject blood = Instantiate(bloodSystem, position,rotat);
        blood.GetComponentInChildren<ParticleSystem>().Play();
        Destroy(blood, 1.5f);
    }

}
