using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewTriggerUp : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] triggers;
    private bool _buttonPressed=false;
  

    public bool ButtonPressed
    {
        get
        {
            return _buttonPressed;
        }

        set
        {
            _buttonPressed = value;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (_buttonPressed) {
            foreach (var trigg in triggers)
            {
                if (trigg.activeSelf) {
                    trigg.SetActive(false);
                }
               else if (!trigg.activeSelf) {
                    trigg.SetActive(true);
                    break;
                }
            }
            _buttonPressed = false;
        }
    }
    
}
