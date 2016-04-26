using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour{

	private Vector3 playerPosition;

	private bool isEnabled = false;

	/****************************************************************
	 * 	NAME: 			Deactivate
	 *  DESCRIPTION:	Turns the light off or on when exiting the 
	 * 					pressure plate
	 * 
	 * ***************************************************************/
	public void Deactivate( ){

		playerPosition = GameObject.Find ("Player").transform.position;

		if (isEnabled  && (playerPosition.z < 28.0f)) 
			isEnabled = false;
		else 
			if (!isEnabled ) 
				isEnabled = true;

		this.GetComponent<Light> ().enabled = isEnabled;
	}
}

