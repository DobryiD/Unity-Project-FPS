using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {
    
    public float sensivity;
    public float minVert;
    public float maxVert;
    public Transform cameraTransform;

    Vector3 startRotation;
    private float _rotationXrecoil = 0;
    private float _rotationY = 0;
    private PlayerHealth health;
    private PauseMenu pause;
    void Awake() {
        pause = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();

        health = GetComponent<PlayerHealth>();
    }


    void RecoilAndRotation() {
        Fire fire;
        if (fire=GetComponentInChildren<Fire>())
        {

            float recoilRotationX = fire.currRecoil * fire.recoilSmoothness;
            float _recoil = 0;
            _recoil = Mathf.Clamp(_recoil, minVert, maxVert);
            _recoil = _rotationXrecoil - recoilRotationX;

            if (!Input.GetButton("Fire1"))
            {
                _rotationXrecoil = _recoil;

                fire.RemoveRecoil();
            }
            cameraTransform.localRotation = Quaternion.Euler(_recoil, 0, 0);

        }
        else {

            cameraTransform.localRotation = Quaternion.Euler(_rotationXrecoil, 0, 0);
            
        }

    }

    void LateUpdate() {

        if (!pause.isStopped())
        {
            if (health.GetCurrentHealth() <= 0)
            {
                return;
            }
            _rotationXrecoil -= Input.GetAxis("Mouse Y") * sensivity;
            _rotationXrecoil = Mathf.Clamp(_rotationXrecoil, minVert, maxVert);

            float delta = Input.GetAxis("Mouse X") * sensivity;
            _rotationY = transform.localEulerAngles.y + delta;

            RecoilAndRotation();

            transform.localEulerAngles = new Vector3(0, _rotationY, 0);

        }

    }

}
