using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private float jumpSpeed = 12.0f, gravity = -9.0f, terminalVelocity = -10.0f,
		minFall = -1.6f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float MinFall{
		get{ return minFall; }
		set{ minFall = value; }

	}

	public float Gravity{
		get{ return gravity; }
		set{ gravity = value; }
	}

	public float JumpSpeed{
		get{ return jumpSpeed; }
		set{ jumpSpeed = value;}
	}

	public float TerminalVelocity{
		get{ return terminalVelocity; }
		set{ terminalVelocity = value; }
	}
}
