using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour {
	[SerializeField] private Transform target;


	private float rotSpeed = 1.5f, rotY;

	private Vector3 offSet;

	// Use this for initialization
	void Start () {
	
		target = GameObject.Find ("Player").transform;

		rotY = transform.eulerAngles.y;
		offSet = target.position - transform.position;

	}
	
	// Update is called once per frame
	void LateUpdate () {
	
		float horInput = Input.GetAxis ("Horizontal");

		if (horInput != 0)
			rotY += horInput * rotSpeed;
		else
			rotY += Input.GetAxis ("Mouse X") * rotSpeed * 3;

		Quaternion rotation = Quaternion.Euler (0,rotY,0);

		transform.position = target.position - (rotation * offSet);

		transform.LookAt (target);
	}


	float CameraSpeed{
		get{ return rotSpeed;}
		set { rotSpeed = value;}

	}

}
