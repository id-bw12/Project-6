using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour, IGameManager{

	public ManagerStatus status { get; private set;}

	public string equippedItem{ get; private set;}

	private Dictionary<string, int>  items;


	/****************************************************************
	 * 	NAME: 			Startup
	 *  DESCRIPTION:	Starts the Inventory manager
	 * 
	 * ***************************************************************/
	public void Startup(){
		Debug.Log ("Inventory manager starting...");

		items = new Dictionary<string, int> ();

		status = ManagerStatus.Started;

		Debug.Log (items.Count);
	}

	/****************************************************************
	 * 	NAME: 			DisplayItems
	 *  DESCRIPTION:	Displays items in the console
	 * 
	 * ***************************************************************/
	private void DisplayItems(){
		string itemDisplay = "Items: ";
		foreach (KeyValuePair<string, int> item in items)
			itemDisplay += item.Key + " ("+ item.Value +") ";

		Debug.Log (itemDisplay);
	}

	/****************************************************************
	 * 	NAME: 			AddItem
	 *  DESCRIPTION:	Add item to the inventory Dictionary.
	 * 
	 * ***************************************************************/
	public void AddItem(string name){
		if (items.ContainsKey (name))
			items [name] += 1;
		else
			items [name] = 1;
		
		DisplayItems ();
	}

	/****************************************************************
	 * 	NAME: 			GetItemsList
	 *  DESCRIPTION:	Gets the Keys in the item dictionary put it
	 * 					in a string list and return it.
	 * 
	 * ***************************************************************/
	public List<string> GetItemsList(){
	
		var list = new List<string> (items.Keys);
		return list;
	}

	/****************************************************************
	 * 	NAME: 			GetItemsCount
	 *  DESCRIPTION:	Gets the item count of a specific item passed
	 * 					by the player.
	 * 
	 * ***************************************************************/
	public int GetItemCount(string name){

		if (items.ContainsKey (name))
			return items [name];

		return 0;
	}

	/****************************************************************
	 * 	NAME: 			EquipItem
	 *  DESCRIPTION:	Checks if the player has equiped the selected 
	 * 					item. If equipped then it unequipped or if 
	 * 					unequipped then equipped.
	 * 
	 * ***************************************************************/
	public bool EquipItem(string name){
		if (items.ContainsKey (name) && equippedItem != name) {
		
			equippedItem = name;
			Debug.Log ("Equipped " + name);
			return true;
		}

		equippedItem = null;
		Debug.Log ("Unequipped");
		return false;
	}


	public bool ConsumeItem(string name){
	
		if (items.ContainsKey (name)) {
			items [name]--;
			if (items [name] == 0)
				items.Remove (name);
		}else {
			Debug.Log ("cannot consume " + name);
			return false;
		}
		DisplayItems ();
		return true;
		
	}
}

