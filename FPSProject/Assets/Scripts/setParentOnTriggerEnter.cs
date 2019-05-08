using UnityEngine;
using System.Collections;

public class setParentOnTriggerEnter : MonoBehaviour {

	public GameObject parentObject;
	public GameObject parentToEnter;
    

    public bool selfDestroy = true;

	void OnTriggerEnter( Collider other )
	{
		if( other.tag == "Player" )
		{
			if(parentToEnter)
			{
				parentObject.transform.SetParent(parentToEnter.transform);
			}
			else
			{
				parentObject.transform.SetParent(null);
			}
			
			if( selfDestroy ) Destroy ( this.gameObject );
		}
	}
    
}
