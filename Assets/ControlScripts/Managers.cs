using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerControl))]
[RequireComponent(typeof(InventoryManager))]

public class Managers : MonoBehaviour{
	public static PlayerControl Player { get; private set;}
	public static InventoryManager Inventory { get; private set;}

	private List<IGameManager> startSequence;

	/******************************************************************
	 * 	NAME: 			Awake
	 *  DESCRIPTION:	sets up the players health, the maxhealth limit,
	 * 					and get the status to Started.
	 * 
	 * ***************************************************************/
	void Awake(){

		Player = GetComponent<PlayerControl> ();
		Inventory = GetComponent<InventoryManager> ();

		startSequence = new List<IGameManager> ();
		startSequence.Add (Player);
		startSequence.Add (Inventory);

		StartCoroutine (StartupManagers());
		
	}

	private IEnumerator StartupManagers(){


		int numModules = startSequence.Count;
		int numReady = 0;
		int lastReady = 0;

		foreach (IGameManager manager in startSequence)
			manager.Startup ();

		yield return null;

		while (numReady < numModules) {
			lastReady = numReady;
			numReady = 0;

			foreach (IGameManager manager in startSequence)
				if (manager.status == ManagerStatus.Started)
					numReady++;

			if (numReady > lastReady)
				Debug.Log ("Progress: " + "numReady" + "/" + numModules);

			yield return null;

			Debug.Log ("All managers started up");
		}

	}

}

