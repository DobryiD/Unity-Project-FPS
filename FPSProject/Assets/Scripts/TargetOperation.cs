using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TargetOperation : MonoBehaviour {

    public float timeBeforeAppering;
    public GameObject[] targets;
    private bool ShootingStarted = false;
    private int TargetCount = 0;
    private GameObject[] randomisedTargets;
    private GameObject tip;
    private bool IsStanding = false;

    

    private void Update()
    {

        if (ShootingStarted&&TargetCount<targets.Length)
        {
            StartCoroutine(startShootingTargets(randomisedTargets));

        }
        else if (TargetCount >= targets.Length) {
            ShootingStarted = false;
            TargetCount = 0;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        
    }
    public void ActivateShooting()
    {
            ShootingStarted = true;
            randomisedTargets = targets.OrderBy(x => Random.value).ToArray();
            gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private IEnumerator startShootingTargets(GameObject[] targ) {

        if (!targ[TargetCount].GetComponent<ShootingTarget>().TargetClosed)
        {
            
            targ[TargetCount].SetActive(true);
            targ[TargetCount].GetComponent<ShootingTarget>().OpenTarget();
        }
        else if (targ[TargetCount].GetComponent<ShootingTarget>().TargetClosed)
        {
            targ[TargetCount].GetComponent<ShootingTarget>().TargetClosed = false;
            TargetCount++;
        }
        yield return new WaitForSeconds(timeBeforeAppering);
    }

    
    private void ShootingRandeTip(bool IsStanding)
    {
        if (tip = GameObject.FindGameObjectWithTag("UItips"))
        {
            if (IsStanding)
            {
                tip.GetComponentInChildren<Text>().text = "Press <color=yellow>" + " E " + "</color> to start shooting targets";
            }
            else
            {
                tip.GetComponentInChildren<Text>().text = "";
            }
        }

    }
}
