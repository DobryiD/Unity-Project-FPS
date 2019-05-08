using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public int damage = 20;
    public GameObject shell;
    public float fireRate;
    public float maxBullet;
    public float bulletSupplies;
    public float weaponRange;
    public float recoilHorizSpread;
    public float recoilVerticalSpread;
    public Transform barrelEnd;
    public Transform shellBlock;
    public bool semiAuto;
    public Camera[] fpsCamera;
    public float VerticalrecoilAmount = 3f;
    public float maxRecoil = 50f;
    public float negativeRecoil = 2;
    public float currRecoil;
    public float recoilSmoothness = 0.6f;
    public GameObject bulletHole;
    public AudioClip gunShotSound;
    public AudioClip gunReload;
    public GameObject flames;

    private float currentBullets;
    private WaitForSeconds shotDuration = new WaitForSeconds(.1f);
    private AudioSource gunAudio;
    
    private float nextFire;
    private Animator weaponAnim;
    private Aiming aiming;
    private PlayerMovement mov;
    private PauseMenu pause;
    private PlayerHealth health;
    

	void Awake () {
        health = GetComponentInParent<PlayerHealth>();
        
        weaponAnim = GetComponent<Animator>();
        gunAudio = GetComponent<AudioSource>();
        aiming = GetComponentInParent<Aiming>();
        mov = GetComponentInParent<PlayerMovement>();
        pause = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        currentBullets = maxBullet;
    }
    public float GetCurrentBullets()
    {
        return currentBullets;
    }
    public float GetBulletSupplies() {
        return bulletSupplies;
    }

    public void SetBulletSupplies(float amount)
    {
        bulletSupplies+=amount;
    }

    public void RemoveRecoil()
    {
        currRecoil = 0;
    }

    void AddRecoil(float amount)
    {
        currRecoil += amount;
    }

    void BulletDecrease()
    {
        if (bulletSupplies > 0)
        {
            float number;
            if (bulletSupplies >= 31)
            {
                number = maxBullet - currentBullets;
            }
            else
            {
                number = bulletSupplies - currentBullets;
            }
            currentBullets += number;
            bulletSupplies -= number;
        }
    }
    void Reload()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (currentBullets < maxBullet)
            {
                gunAudio.pitch = 1f;
                gunAudio.PlayOneShot(gunReload);
                BulletDecrease();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (health.GetCurrentHealth() <= 0) {
            return;
        }
        Camera camera=fpsCamera[0];

        foreach (Camera item in fpsCamera)
        {
            if (item.gameObject.activeSelf && item.gameObject.tag == "MainCamera" && item!= camera)
                camera = item;
        }
        
        Reload();
        if (Input.GetButton("Fire1") && Time.time >= nextFire) {
            if (currentBullets > 0 && !mov.Running && !pause.isStopped())
            {
                if (semiAuto)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Shoot(camera);
                    }
                }
                else
                {
                        Shoot(camera);
                }
            }
        }
	}

    void Shoot(Camera camera)
    {

        nextFire = Time.time + 1/fireRate;

        RaycastHit hit;
        

        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 direction;
        if (!aiming.aimingShoot)
        {
            float horiz = Mathf.Clamp(Random.Range(-recoilHorizSpread, recoilHorizSpread), -recoilHorizSpread, recoilHorizSpread);
            float vertical = Mathf.Clamp(Random.Range(-recoilVerticalSpread, recoilVerticalSpread), -recoilVerticalSpread, recoilVerticalSpread);
             direction = new Vector3(horiz, vertical, 0f);
        }
        else {
            
            float newHorizPower = recoilHorizSpread / 2;
            float newVertPower = recoilVerticalSpread / 2;
            float horiz = Mathf.Clamp(Random.Range(-newHorizPower, newHorizPower), -newHorizPower, newHorizPower);
            float vertical = Mathf.Clamp(Random.Range(-newVertPower, newVertPower), -newVertPower, newVertPower);
            direction = new Vector3(horiz, vertical, 0f);
        }
       
        if (Physics.Raycast(rayOrigin, camera.transform.forward+ direction, out hit,weaponRange, Physics.DefaultRaycastLayers,QueryTriggerInteraction.Ignore))
        {
            weaponAnim.SetTrigger("Shot");
            AddRecoil(VerticalrecoilAmount);
            StartCoroutine(shotEffect());
            EnemyHealth target;

            if (hit.collider.gameObject.tag == "Boss")
            {
                target = hit.transform.GetComponent<BossHealth>();
            }
            else {
                target = hit.transform.GetComponent<EnemyHealth>();
            }
            
            if (target != null)
            {
                string part = hit.collider.gameObject.tag;
               
                target.GetDamage(part,damage);
            }

            currentBullets--;

            GameObject shellClone = Instantiate(shell, shellBlock.position, shellBlock.rotation);
            shellClone.GetComponent<Rigidbody>().AddForce(shellClone.transform.right + shellClone.transform.up * 10f, ForceMode.Force);

            
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {

                target.Bleeding(hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            }
            else if(hit.collider.gameObject.tag=="ShootingTarget")
            {
                ShootingTarget temp = hit.collider.gameObject.GetComponentInParent<ShootingTarget>();

                GameObject hole = Instantiate(temp.GetBulletHole(),hit.point,Quaternion.FromToRotation(-Vector3.forward, hit.normal),hit.collider.transform);
                temp.ImpactAction(hole.transform.position, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                temp.GetShot();
                Destroy(hole, 10f);

            }
            else
            {
                GameObject bulletClone = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(-Vector3.forward, hit.normal));
                Destroy(bulletClone, 10f);
            }


            Destroy(shellClone, 3f);
        }
    }

    private IEnumerator shotEffect()
    {
        float range = Random.Range(0.5f, 1f);
        gunAudio.pitch = 0.8f;
        gunAudio.PlayOneShot(gunShotSound,range);

        
        flames.GetComponentInChildren<ParticleSystem>().Play(); ;
        yield return shotDuration;
    }
    
}
