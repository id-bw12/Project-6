using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	protected float jumpSpeed = 12.0f, gravity = -9.0f, terminalVelocity = -10.0f,
		minFall = -1.6f;

    protected enum charState { idle = 0, moving = 1, jumping = 2, falling = 3, crouch = 4 };

    protected static charState state = charState.idle;

    // Use this for initialization
    void Start () {
	
	}
}
