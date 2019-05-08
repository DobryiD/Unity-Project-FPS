using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUIControl : MonoBehaviour {
    
    public Text currentBullets;
    public Text supplies;
    public GameObject[] imageWeapons;
    private CanvasGroup group;

    private PlayerInventory inventory;


    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    void Update() {
        GameObject player;
        if (player=GameObject.FindGameObjectWithTag("Player"))
        {
            
            inventory = player.GetComponent<PlayerInventory>();
            IconChange();
            Fire gun = player.GetComponentInChildren<Fire>();
            if (gun != null)
            {
                group.alpha = 1f;
                currentBullets.text = gun.GetCurrentBullets().ToString();
                supplies.text = gun.bulletSupplies.ToString();
            }
            else {
                group.alpha = 0f;
            }
        }
    }
    void IconChange()
    {
        if (GameObject.FindGameObjectWithTag("Rifle"))
        {
            
            foreach (GameObject item in imageWeapons)
            {
                if (item.name == inventory.primaryWeapon.weaponPrefab.name)
                    item.SetActive(true);
                else
                    item.SetActive(false);
            }
        }
        else if (GameObject.FindGameObjectWithTag("Pistol"))
        {
           
            foreach (GameObject item in imageWeapons)
            {
                if (item.name == inventory.secondaryWeapon.weaponPrefab.name)
                    item.SetActive(true);
                else
                    item.SetActive(false);
            }
        }
    }
}
