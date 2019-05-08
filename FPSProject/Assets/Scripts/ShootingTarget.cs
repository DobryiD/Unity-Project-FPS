using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : MonoBehaviour {
    public bool TargetClosed;
    public GameObject WoodImpact;
    private Animator anim;
    [SerializeField]
    private GameObject bulletHole;

    
	void Awake () {
        anim = GetComponent<Animator>();
	}
	
    public GameObject GetBulletHole() {
        return bulletHole;
    }
    public void OpenTarget() {
        anim.SetBool("Pos",true);
        TargetClosed = false;
    }
    public void GetShot() {
        anim.SetBool("Pos", false);
        TargetClosed = true;
    }
    public void Delete() {
        gameObject.SetActive(false);
    }
    public void ImpactAction( Vector3 position,Quaternion rotat) {
        GameObject impact = Instantiate(WoodImpact, position,rotat);
        impact.GetComponentInChildren<ParticleSystem>().Emit(25);
        Destroy(impact, 2f);
    }
}
