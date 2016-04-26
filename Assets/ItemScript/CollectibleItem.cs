using UnityEngine;
using System.Collections;

public class CollectibleItem : MonoBehaviour{

	void OnTriggerEnter(Collider other){

		if (this.name != "Key" || (Managers.Inventory.GetItemsList().Count == 2)) {
			Debug.Log ("Item collected: " + this.name);
			Managers.Inventory.AddItem (name);
			Destroy (this.gameObject);
		} else {
			Debug.Log ("You need to get the other Items first.");
		}
	}

}

