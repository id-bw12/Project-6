using UnityEngine;
using System.Collections;

public class DeviceOperator : MonoBehaviour{
	public float radius = 1.5f;

	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetButtonDown ("Fire3")) {
			Collider[] hitColliders = 
				Physics.OverlapSphere (transform.position, radius);
			
			foreach (Collider hitCollider in hitColliders) {
				var direction = hitCollider.transform.position - transform.position;

				if (Vector3.Dot (transform.forward, direction) > .5f) {
					hitColliders[1].SendMessage ("Operate", SendMessageOptions.DontRequireReceiver);
					hitColliders[2].SendMessage ("Operate", SendMessageOptions.DontRequireReceiver);

				}
			}
		}
	}
}

