using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicUpOnTrigger : MonoBehaviour
{
    public GameObject obj;
    private AudioSource source;
    public bool selfDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        source = obj.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            source.Play();
            if (selfDestroy) Destroy(this.gameObject);
        }
    }
}
