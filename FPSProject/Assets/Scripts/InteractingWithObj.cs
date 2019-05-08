using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractingWithObj : MonoBehaviour {

    private Animator anim;
    public Camera[] cameras;
    public float distance;
    private GameObject tip;
    private PlayerHealth health;

    // Use this for initialization
    void Start () {
        health = GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if (health.GetCurrentHealth() <= 0) {
            return;
        }
        Control(cameras);
	}
    void Control(Camera[] cameras) {
        Camera camera=null;
        foreach (var cam in cameras)
        {
            if (cam.gameObject.activeSelf) {
                camera = cam;
            }
        }
        
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        ActionWithObjects(rayOrigin, camera);
        ShowTip(rayOrigin, camera);
        
        
    }
    void ActionWithObjects(Vector3 rayOrigin,Camera camera)
    {
        RaycastHit hit;
        int mask = (1 << LayerMask.NameToLayer("InteractiveObjects"));
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(rayOrigin, camera.transform.forward, out hit, distance, mask, QueryTriggerInteraction.Collide))
            {
                if ( hit.collider.tag == "Door")
                {

                    anim = hit.collider.gameObject.GetComponent<Animator>();
                    bool doorStatus = anim.GetBool("Open");
                    anim.SetBool("Open", !doorStatus);
                }
                else if ( hit.collider.tag == "Button")
                {
                    SetNewTriggerUp setTrigger = hit.collider.gameObject.GetComponent<SetNewTriggerUp>();
                    bool buttonStatus = setTrigger.ButtonPressed;
                    if (!buttonStatus)
                        setTrigger.ButtonPressed = true;
                }
                else if ( hit.collider.tag == "GameController")
                {
                    hit.collider.gameObject.GetComponent<GameController>().StartFirstWave();
                }
                else if (hit.collider.tag == "TargetController") {
                    hit.collider.gameObject.GetComponent<TargetOperation>().ActivateShooting();
                }

            }
        }
    }
    
    void ShowTip(Vector3 origin,Camera camera) {
        RaycastHit hit;
        int mask=1<< LayerMask.NameToLayer("InteractiveObjects");
        if (tip = GameObject.FindGameObjectWithTag("UItips")) { 
            if ( Physics.Raycast(origin,camera.transform.forward, out hit, distance,mask,QueryTriggerInteraction.Collide))
            {
            
            
                if ( hit.collider.tag == "Door")
                {
                    anim = hit.collider.gameObject.GetComponent<Animator>();
                    bool doorStatus = anim.GetBool("Open");

                    if (!doorStatus)
                        tip.GetComponentInChildren<Text>().text = "Press <color=yellow>" + " E " + "</color> to open";
                    else
                        tip.GetComponentInChildren<Text>().text = "Press <color=yellow>" + " E " + "</color> to close";
                }
                else if ( hit.collider.tag=="TargetController") {
                    
                    tip.GetComponentInChildren<Text>().text = "Press <color=yellow>" + " E " + "</color> to activate targets";
                    
                }
                else if (hit.collider.tag == "Button")
                {

                    tip.GetComponentInChildren<Text>().text = "Press <color=yellow>" + " E " + "</color> to activate the elevator";
                }
                else if (hit.collider.tag == "GameController")
                {

                    tip.GetComponentInChildren<Text>().text = "Press <color=yellow>" + " E " + "</color> to use ";
                }

            }
            else
            {

                tip.GetComponentInChildren<Text>().text = "";
            }
        }
        

    }
}
