using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetNewTaskInMenu : MonoBehaviour
{
    public Text textPlace;
    public string task;

    public bool selfDestroy = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            textPlace.text = task;
            if (selfDestroy) Destroy(this.gameObject);
        }
    }
}
