using UnityEngine;
using System.Collections;

public class DoorOpenDevice : MonoBehaviour{
	[SerializeField] private Vector3 dPos = new Vector3(0f, 2.5f, 0f);

	private bool open = false;

	/****************************************************************
	 * 	NAME: 			Operate
	 *  DESCRIPTION:	For the door with the switch to open or close
	 * 
	 * ***************************************************************/
	public void Operate(){
		var pos = Vector3.zero;
		if (open) {
			 pos = transform.position - dPos;
		} else {
			 pos = transform.position + dPos;
		}

		transform.position = pos;

		open = !open;
	}

	/****************************************************************
	 * 	NAME: 			Activate
	 *  DESCRIPTION:	Opens the door when the player enters the 
	 * 					pressure plate
	 * 
	 * ***************************************************************/
	public void Activate(){
		var pos = Vector3.zero;
		if (!open) {
			pos = transform.position + dPos;
			transform.position = pos;
			open = true;
		}
	}

	/****************************************************************
	 * 	NAME: 			Deactivate
	 *  DESCRIPTION:	Close the door when the player leaves the 
	 * 					pressure plate
	 * 
	 * ***************************************************************/
	public void Deactivate(){
		var pos = Vector3.zero;
		if (open) {
			pos = transform.position - dPos;
			transform.position = pos;
			open = false;
		}
	}
}

