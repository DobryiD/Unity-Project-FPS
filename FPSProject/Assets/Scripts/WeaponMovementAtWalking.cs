using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovementAtWalking : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement movementScript;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        movementScript = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementScript.InputDir != Vector3.zero)
        {
            anim.SetBool("Movement", true);
        }
        else {
            anim.SetBool("Movement", false);
        }
    }
}
