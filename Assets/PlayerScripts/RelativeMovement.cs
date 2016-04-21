using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : PlayerControl
{
    [SerializeField]
    private Transform target;

    private float moveSpeed = 6.0f, vertSpeed, horInput, vertInput;
    private CharacterController charController;
    private ControllerColliderHit contact;
    private Animator animate;
    private PlayerControl control;

    // Use this for initialization
    void Start()
    {

        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        target = GameObject.Find("Main Camera").transform;

        charController = GetComponent<CharacterController>();

        charController.center = new Vector3(0, 0, 0);
        charController.radius = 0.4f;
        charController.height = 2f;

        control = gameObject.GetComponent<PlayerControl>();

        vertSpeed = minFall;

        animate = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 movement = Vector3.zero;

        bool hitGround = false;

        RaycastHit hit;

        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        movement.x = horInput * moveSpeed;
        movement.z = vertInput * moveSpeed;

        movement = Vector3.ClampMagnitude(movement, moveSpeed);

        RotatePlayer(horInput, vertInput, ref movement);

        if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {

            float check = (charController.height + charController.radius) / 1.9f;

            hitGround = hit.distance <= check;

        }
        
        CheckJumping(hitGround, ref movement);

        movement.y = vertSpeed;

        movement *= Time.deltaTime;
        
        if (hitGround)
            if (movement.x == 0 && movement.z == 0)
                state = charState.idle;
            else
                if (movement.x != 0 || movement.z != 0)
                state = charState.moving;

        this.gameObject.GetComponent<AnimatePlayer>().SelectingAnimatation();

        charController.Move(movement);
    }

    void RotatePlayer(float horInput, float vertInput, ref Vector3 movement)
    {

        if (horInput != 0 || vertInput != 0)
        {

            movement.x = horInput;
            movement.y = vertInput;

            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);

            target.rotation = tmp;

            transform.rotation = Quaternion.LookRotation(movement);
        }

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

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

    void CheckJumping(bool isGrounded, ref Vector3 movement)
    {

        if (isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                vertSpeed = jumpSpeed;
                state = charState.jumping;
            }
            else
                vertSpeed = -0.1f;
                animate.SetBool("Jumping", false);
        }
        else {
            vertSpeed += gravity * 5 * Time.deltaTime;

            if (vertSpeed < terminalVelocity)
                vertSpeed = terminalVelocity;

            if (contact != null)
                state = charState.jumping;

            if (charController.isGrounded)
            {
                if (Vector3.Dot(movement, contact.normal) < 0)
                    movement = contact.normal * moveSpeed;
                else
                    movement += contact.normal * moveSpeed;
            }

        }
    }
}
