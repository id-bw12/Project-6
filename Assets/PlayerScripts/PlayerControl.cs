using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour, IGameManager {

	protected float jumpSpeed = 12.0f, gravity = -9.0f, terminalVelocity = -10.0f,
		minFall = -1.6f;

    protected enum charState { idle = 0, moving = 1, jumping = 2, falling = 3};

    protected static charState state = charState.idle;

	public int health{ get; private set;}
	public int maxHealth{ get; private set;}

	public ManagerStatus status { get; private set;}

	/******************************************************************
	 * 	NAME: 			Startup
	 *  DESCRIPTION:	sets up the players health, the maxhealth limit,
	 * 					and get the status to Started.
	 * 
	 * ***************************************************************/
	public void Startup(){
		Debug.Log ("Player control starting...");

		health = 50;

		maxHealth = 100;

		status = ManagerStatus.Started;
	}

	/******************************************************************
	 * 	NAME: 			ChangeHealth
	 *  DESCRIPTION:	Updates the players health and checks to see if
	 * 					the players health either reach or surpass zero
	 * 					or if the health passed the health limit.
	 * 
	 * ***************************************************************/
	public void ChangeHealth(int value){

		health += value;
		if (health > maxHealth) {
			health = maxHealth;
		} else if (health < 0) {
			health = 0;
		}

		Debug.Log ("Health: "+ health + "/" + maxHealth);
	}
}
