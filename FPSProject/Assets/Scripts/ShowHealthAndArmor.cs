using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHealthAndArmor : MonoBehaviour {
    [SerializeField]
    private Text healthPoints;
    [SerializeField]
    private Text armorPoints;
    private GameObject player;
    private PlayerHealth playerHealth;

    
	
	
	void Update () {
        if (player = GameObject.FindGameObjectWithTag("Player")) {
            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null) {
                if (playerHealth.GetCurrentHealth() < 0)
                {
                    healthPoints.text = "0";
                }
                else {
                    healthPoints.text = playerHealth.GetCurrentHealth().ToString();
                }
                armorPoints.text = playerHealth.GetCurrentArmor().ToString();
            }
        }
	}
}
