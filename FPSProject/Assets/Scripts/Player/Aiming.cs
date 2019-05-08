using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour {

    public Camera fpsCamera;
    public Camera aimingCamera;
    public bool aimingShoot;

    private Transform aiming;
    private GameObject aimSight;
    private PlayerHealth health;
    
	// Use this for initialization
	void Start () {
        health = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void LateUpdate() {
        if (health.GetCurrentHealth() <= 0) {
            return;
        }

        if (aimSight = GameObject.FindGameObjectWithTag("AimSight"))
        {
            aiming = aimSight.transform;
            Vector3 aimingPos = aiming.position;
            Quaternion aimingRot = aiming.rotation;

            if (Input.GetButton("Fire2"))
            {
                fpsCamera.gameObject.SetActive(false);

                aimingCamera.gameObject.SetActive(true);

                aimingShoot = true;

            }
            else
            {
                fpsCamera.gameObject.SetActive(true);
                aimingCamera.gameObject.SetActive(false);
                aimingShoot = false;
            }
            aimingCamera.transform.position = aimingPos;
            aimingCamera.transform.rotation = aimingRot;

        }
    }
}
