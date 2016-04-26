using UnityEngine;
using System.Collections;

public class DeviceTrigger : MonoBehaviour{
	[SerializeField] private GameObject[] targets;


	void OnTriggerEnter(Collider other){

		if (Managers.Inventory.equippedItem == "Key") {
			foreach (GameObject target in targets) {
				target.SendMessage ("Activate");
			}
		}
			
	}

	void OnTriggerExit(Collider other){
		Debug.Log (targets [0].name);

		foreach (GameObject target in targets) {
			target.SendMessage("Deactivate");
		}
	}

	// Use this for initialization
	void Start ()
	{
	
		targets = new GameObject[]{this.transform.parent.gameObject};
	}
}

