using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingConsumables : MonoBehaviour {
    
    public bool ammo;
    public bool primary;
    public bool secondary;
    public bool aidKit;
    
    private int healthPoints=25;
    
    private int ammoNumber= 90;
    private PlayerHealth playerHealth;
    private MeshRenderer render;
    private GameObject[] playersWeapon;
    
    private bool PackActive=true;

    void Awake()
    {
        render = GetComponent<MeshRenderer>();
        playersWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeaponChange>().getWeapons();

    }

    
    public void Spawned() {
        render.enabled = true;
        PackActive = true;
    }
    public bool isActive() {
        return PackActive;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && PackActive && aidKit)
        {
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.HealHealth(healthPoints);

            render.enabled = false;
            PackActive = false;
            
        }
        else if (other.gameObject.tag == "Player" && PackActive && ammo) {
            if (playersWeapon.Length != 0) {
                foreach (GameObject weap in playersWeapon)
                {
                    if (primary && weap.tag == "Rifle")
                    {
                        weap.GetComponent<Fire>().SetBulletSupplies(ammoNumber);
                        render.enabled = false;
                        PackActive = false;
                        
                    }
                    else if (secondary && weap.tag == "Pistol") {
                        weap.GetComponent<Fire>().SetBulletSupplies(ammoNumber);
                        render.enabled = false;
                        PackActive = false;
                        
                    }
                }
            }
        }
    }

}
