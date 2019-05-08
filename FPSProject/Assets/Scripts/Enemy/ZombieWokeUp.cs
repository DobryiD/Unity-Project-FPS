using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWokeUp : MonoBehaviour
{
    public GameController gameController;
    EnemyMovement mov;
    EnemyAttack attack;
    EnemyHealth health;
    // Start is called before the first frame update
    void Start()
    {
        mov = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttack>();
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.firstWaveStarted) {
            mov.enabled = true;
            attack.enabled = true;
            health.enabled = true;
        }
    }
}
