using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour {
	[SerializeField] private Transform target;

	private float moveSpeed = 6.0f, vertSpeed, horInput, vertInput;
	private CharacterController charController;
	private ControllerColliderHit contact;
	private Animator animate;
	private PlayerControl control;

    private enum charState { idle = 0, moving = 1, jumping = 2, crouch = 3 };

    private charState state = charState.idle;

    // Use this for initialization
    void Start () {

        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        target = GameObject.Find ("Main Camera").transform;

		charController = GetComponent<CharacterController> ();

		charController.center = new Vector3 (0, 1, 0);
		charController.radius = 0.4f;
		charController.height = 1.88f;

		control = gameObject.GetComponent<PlayerControl> ();

		vertSpeed = control.MinFall;

		animate = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 movement = Vector3.zero;

		bool hitGround = false;
        float horInput = Input.GetAxis("Horizontal"), vertInput= Input.GetAxis("Vertical");

        RaycastHit hit;
        
        movement.x = horInput * moveSpeed;
        movement.z = vertInput * moveSpeed;

        this.gameObject.GetComponent<AnimatePlayer>().SelectingAnimatation(movement, state);

		movement = Vector3.ClampMagnitude (movement, moveSpeed);

		RotatePlayer (horInput,vertInput, ref movement);

		if (vertSpeed < 0 && Physics.Raycast (transform.position, Vector3.down, out hit)) {

			float check = (charController.height + charController.radius) / 9.9f;

			hitGround = hit.distance <= check;

            if (horInput != 0)
                Debug.Log(hitGround);
		}

		CheckJumping (hitGround, ref movement);

		movement.y = vertSpeed;

		movement *= Time.deltaTime;
		charController.Move (movement);
	}

	void RotatePlayer(float horInput, float vertInput, ref Vector3 movement){
	
		if (horInput != 0 || vertInput != 0) {

			movement.x = horInput;
			movement.y = vertInput;

			Quaternion tmp = target.rotation;
			target.eulerAngles = new Vector3 (0, target.eulerAngles.y, 0);
			movement = target.TransformDirection (movement);

			target.rotation = tmp;

			transform.rotation = Quaternion.LookRotation (movement);
		}

	}

	void OnControllerColliderHit(ControllerColliderHit hit){

		contact = hit;
	}

	/****************************************************************
	 * 	NAME: 			CheckingJumping
	 *  DESCRIPTION:	Passes a boolean and references a Vector3
	 * 					variable and checks if the player is grounded
	 * 					and 
	 * 					then passes it to the game logic and 
	 * 					activate to the timer.
	 * 
	 * ***************************************************************/

	void CheckJumping(bool isGrounded, ref Vector3 movement){
		
		if (isGrounded) {
            if (Input.GetButton("Jump")) {
                vertSpeed = control.JumpSpeed;
                state = charState.jumping;
            }
            else {

                vertSpeed = -0.1f;
                animate.SetBool("Jumping", false);
                if (horInput != 0 || vertInput != 0)
                    state = charState.moving;
                else
                    state = charState.idle;

            }
		} else {
			vertSpeed += control.Gravity * 5 * Time.deltaTime;

			if (vertSpeed < control.TerminalVelocity)
				vertSpeed = control.TerminalVelocity;
			
			if (contact != null) 
				animate.SetBool ("Jumping", true);

			if (charController.isGrounded) {
				if (Vector3.Dot (movement, contact.normal) < 0)
					movement = contact.normal * moveSpeed;
				else
					movement += contact.normal * moveSpeed;
			}

		}
	}
}
