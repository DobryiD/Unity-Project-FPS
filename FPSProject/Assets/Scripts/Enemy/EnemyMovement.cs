using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public float soundFrequency;
    public AudioClip[] moveSound;
    private Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private NavMeshAgent nav;
    private Animator anim;
    private AudioSource source; 
    [SerializeField]
    private bool crawling;
    [SerializeField]
    private bool boss;
    private Vector3 lastPosition;
    private float soundTimer;
    
	
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();

       
        
        if (!crawling&&!boss)
        {
            anim.SetInteger("Walk", Random.Range(1, 6));

            int walk = anim.GetInteger("Walk");
            if (walk == 2)
            {
                nav.acceleration = 2;
            }
            else if (walk == 4)
            {
                nav.acceleration = 4;
            }
        }

	}
    public void StopEnemy() {

        nav.isStopped=true;
    }
    public void MoveEnemy() {

        nav.ResetPath();
    }

	void Update () {

        soundTimer += Time.deltaTime;
        
        if (enemyHealth.GetCurrentHealth() > 0 && playerHealth.GetCurrentHealth() > 0)
        {         
            nav.SetDestination(player.position);
            if (soundTimer >= soundFrequency)
            {
                source.PlayOneShot(moveSound[Random.Range(0, moveSound.Length)]);
                soundTimer = 0f;
            }
        }
        
        else if (playerHealth.GetCurrentHealth() <= 0)
        {
            anim.SetBool("PlayerDead", true);
            StopEnemy();
        }
        else if (enemyHealth.GetCurrentHealth() <= 0)
        {
            StopEnemy();
        }
        
	}
}
