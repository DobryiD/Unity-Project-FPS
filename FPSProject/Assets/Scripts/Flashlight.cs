using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light[] flashligths;
    private bool isActive=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            if (isActive)
            {
                foreach (Light light in flashligths)
                {
                    light.enabled = false;
                }
                isActive = false;
            }
            else {
                foreach (Light light in flashligths)
                {
                    light.enabled = true;
                }
                isActive = true;
            }
        }
    }
}
