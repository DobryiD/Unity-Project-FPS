using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    public int damage;
    public float timeBetweenAttack = 0.5f;
    public bool crawling;
    public bool boss;
    public Collider[] rangeOfAttack;
    public AudioClip[] attackSounds;
    public LayerMask target;
    private AudioSource audioSource;
    private Animator animator;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;

    private GameObject player;
    private bool targetInRange;
    private float attackTimer;



    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();

    }

    public PlayerHealth GetPlayerHealth() {
        return playerHealth;
    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject == player) {

            targetInRange = true;
            animator.SetBool("ReadyForAttack", targetInRange);
        }
        

    }
    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject == player && playerHealth.GetCurrentHealth() >= 0)
        {

            targetInRange = true;
            animator.SetBool("ReadyForAttack", targetInRange);
        }
        else if (playerHealth.GetCurrentHealth() <= 0) {
            targetInRange = false;
            animator.SetBool("ReadyForAttack", targetInRange);
        }


    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject == player)
        {  
            targetInRange = false;
            animator.SetBool("ReadyForAttack", targetInRange);
        }
    }

    public bool IsTargetInRange() {
        return targetInRange;
    }
    

    
    void Update () {
        
        attackTimer += Time.deltaTime;
        
        if (attackTimer >= timeBetweenAttack && targetInRange && enemyHealth.GetCurrentHealth() > 0) {

            StartCoroutine(AttackPlayer());
            
        }
        if (playerHealth.GetCurrentHealth() <= 0) {
            animator.SetBool("PlayerDead",true);
        }
	}
    
    IEnumerator AttackPlayer()
    {
        attackTimer = 0f;
        if (playerHealth.GetCurrentHealth() > 0)
        {
            foreach (Collider coll in rangeOfAttack)
            {
                StartCoroutine(LaunchAttack(coll, damage));
            }
            
            if (!crawling&&!boss)
            {

                int rand = Random.Range(1, 5);
                animator.SetTrigger("Attack" + rand);
            }
            else if (boss) {
                int rand = Random.Range(1, 4);
                animator.SetTrigger("Attack" + rand);
            }
            else
            {
                animator.SetTrigger("Attack");
            }

            audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length)], 1f);

            yield return null;
        }
        yield return null;
    }
    
    IEnumerator LaunchAttack(Collider col, int damage) {
        Collider[] colliders = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, target);
        foreach (Collider item in colliders)
        {
            if (item.gameObject.tag == "Player") {
                
                playerHealth.GetDamage(damage);
                yield return null;
            }

        }
    }
}
