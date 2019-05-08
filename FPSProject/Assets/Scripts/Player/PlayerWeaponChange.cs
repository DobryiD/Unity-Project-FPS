using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponChange : MonoBehaviour {

    public GameObject[] weapons;
    PlayerInventory inventory;
    GameObject currentWeapon;
   
    void Start () {
        
        inventory = GetComponent<PlayerInventory>();
        
    }
    public GameObject[] getWeapons() {
        return weapons;
    }
    
	void Update () {
        Changing();
    }
    void Changing()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            
            if (GameObject.FindGameObjectWithTag("Pistol") &&
                    GameObject.FindGameObjectWithTag("Pistol").name == inventory.secondaryWeapon.weaponPrefab.name)
            {

                currentWeapon = GameObject.FindGameObjectWithTag("Pistol");
                foreach (GameObject item in weapons)
                {
                    if (item.name == inventory.primaryWeapon.weaponPrefab.name)
                    {
                        currentWeapon.SetActive(false);
                        item.SetActive(true);
                        break;
                    }

                }


            }
            else if (GameObject.FindGameObjectWithTag("Rifle") &&
                GameObject.FindGameObjectWithTag("Rifle").name == inventory.primaryWeapon.weaponPrefab.name)
            {
                currentWeapon = GameObject.FindGameObjectWithTag("Rifle");
                foreach (GameObject item in weapons)
                {
                    if (item.name == inventory.secondaryWeapon.weaponPrefab.name)
                    {
                        currentWeapon.SetActive(false);
                        item.SetActive(true);
                        break;
                    }

                }


            }
        }
    }
}
