using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{

    
    public AudioClip bossHurtSound;
    public AudioClip bossRoarSound;
    public AudioClip bossDeathSound;

    
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && alive)
        {

            anim.SetLayerWeight(anim.GetLayerIndex("Attacks"), 0);

            anim.SetTrigger("Death");
            source.PlayOneShot(bossDeathSound, 1.5f);

            alive = false;
            
            Destroy(gameObject, 20f);
        }
    }

    private IEnumerator BossHurt()
    {
        EnemyMovement move = GetComponent<EnemyMovement>();
        AudioSource source = GetComponent<AudioSource>();
        move.StopEnemy();
        source.PlayOneShot(bossHurtSound);
        anim.SetTrigger("GotHurt");
        source.PlayOneShot(bossRoarSound);
        yield return new WaitForSecondsRealtime(3f);
        move.MoveEnemy();

    }
    public override void GetDamage(string bodyPart, int damage)
    {
        if (currentHealth <= maxHealth / 2 && !boostUp)
        {
            StartCoroutine(BossHurt());
            BossBoostUp();
        }
        base.GetDamage(bodyPart, damage);
        
    }
    private void BossBoostUp()
    {
        if (currentHealth <= maxHealth / 2 && !boostUp)
        {
            StartCoroutine(BossHurt());
            EnemyAttack attack = GetComponent<EnemyAttack>();
            int tempAttackDamage = attack.damage;
            attack.damage = tempAttackDamage + (tempAttackDamage * 15) / 100;
            boostUp = true;
        }
    }
}
