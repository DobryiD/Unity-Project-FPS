using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheGameplayDoorTrigger : MonoBehaviour
{
    public GameObject[] triggers;
    public GameObject door;
    public GameObject[] objsSetActive;
    public GameObject[] objsSetUnactive;
    public GameObject anotherTrigger;
    public bool selfDestroy;
    private bool destroid=false;



    private void Update()
    {
        if (!anotherTrigger.activeSelf)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            foreach (var item in triggers)
            {
                if (item == null)
                {
                    destroid = true;
                }
                else
                    destroid = false;
            }
            if (destroid)
            {
                foreach (GameObject item in objsSetActive)
                {
                    item.SetActive(true);
                }
                foreach (GameObject item in objsSetUnactive)
                {
                    item.SetActive(false);
                }
                door.GetComponent<Animator>().CrossFade("DoorOpen140de",0.1f,0);
                gameObject.SetActive(false);
                if (selfDestroy) { Destroy(this.gameObject); }
            }
            
        }
        
    }
}
