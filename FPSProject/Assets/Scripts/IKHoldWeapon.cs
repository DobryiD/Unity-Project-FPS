using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHoldWeapon : MonoBehaviour {

    public Transform lookTarget;
    Transform leftHoldTarget;
    Transform leftHold;
    Quaternion lhRot;
    PlayerInventory inventory;
    Vector3 lookOffset;
    
    Animator anim;
    Vector3 lookTargetPos;
    Quaternion lookTargetRot;

    public Vector3 rotation;
    Vector3 leftHandRotation;
    

    private PlayerHealth health;
    

    // Use this for initialization
    private void Awake () {
        anim = GetComponent<Animator>();
        inventory = GetComponent<PlayerInventory>();
        leftHold = new GameObject().transform;
        leftHold.name = "leftHold";
        health = GetComponent<PlayerHealth>();
    }
    
    private void Update()
    {
        if (health.GetCurrentHealth() <= 0) {
            return;
        }

        if (anim.GetBool("PistolArmed"))
        {
            lookOffset = inventory.secondaryWeapon.lookOffset;
            SetLeftHand();
        }
        else if (anim.GetBool("RifleArmed"))
        {
            lookOffset = inventory.primaryWeapon.lookOffset;
            SetLeftHand();
        }

    }
    private void SetLeftHand() {
        if (GameObject.FindGameObjectWithTag("LeftHandPosition"))
            leftHoldTarget = GameObject.FindGameObjectWithTag("LeftHandPosition").transform;

        leftHold.position = leftHoldTarget.position;
        lhRot = leftHoldTarget.rotation;

        lookTargetPos = lookTarget.position;
        lookTargetRot = lookTarget.rotation;
    }

    private void OnAnimatorIK()
    {

        if (anim.GetBool("PistolArmed") || anim.GetBool("RifleArmed"))
        {
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHold.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, lhRot);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        }
        else {
            anim.SetBool("PistolArmed", false);
            anim.SetBool("RifleArmed", false);
            anim.SetBool("GunUp", false);
            
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
        }
        if (anim.GetBool("GunUp"))
        {
            Vector3 rightHandTargetPosition;
            rightHandTargetPosition = lookTargetPos + lookTargetRot * lookOffset;

            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTargetPosition);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotation(AvatarIKGoal.RightHand, lookTargetRot * Quaternion.Euler(rotation));
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        }
        else {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        }

    }
}
