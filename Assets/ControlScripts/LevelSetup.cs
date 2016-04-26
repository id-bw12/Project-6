using UnityEngine;
using System.Collections;

public class LevelSetup: MonoBehaviour {

	private const string CHARACTER_MESH_PATH = "Graphics/Models/player",
	ANIMATOR_CONTROLLER = "Graphics/Animator/player",
	MATERIAL_COLOR = "Graphics/Materials/player";

	// Use this for initialization
	void Start() {

		MakeFloor();

		MakePlayer();

		SetupCamera();

		MakeItem();

	}

	void MakeFloor() {

		var floorObject = GameObject.CreatePrimitive(PrimitiveType.Plane);

		floorObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

		floorObject.transform.localScale = new Vector3(10, 1, 10);

		floorObject.GetComponent<Renderer>().material.color = Color.blue;

		MakeOuterWalls();

		MakeInterWalls();

		MakeDoor();

		MakePlatforms();

		MakeBlocks ();

		MakeBuilding ();

	}

	void MakePlayer() {

		GameObject playerObject = Instantiate(Resources.Load(CHARACTER_MESH_PATH) as GameObject);

		playerObject.name = "Player";

		playerObject.transform.position = new Vector3(0.0f, 1.1f, 0.0f);

		playerObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(ANIMATOR_CONTROLLER) as RuntimeAnimatorController;

		playerObject.GetComponent<Animator>().applyRootMotion = false;

		playerObject.GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load(MATERIAL_COLOR) as Material;

		playerObject.AddComponent<RelativeMovement>();
		playerObject.AddComponent<AnimatePlayer>();
	}

	void SetupCamera() {

		GameObject.Find("Main Camera").AddComponent<OrbitCamera>();
	}

	/****************************************************************
	 * 	NAME: 			MakeOuterWall
	 *  DESCRIPTION:	Makes the outer walls for the level so the player
	 * 					doesn't fall off the world.
	 * ***************************************************************/

	void MakeOuterWalls() {
		float scale = 100.0f;

		float wallYPos = 5.5f;

		float wallYScale = wallYPos * 2;

		float[,] wallXZPos = new float[,] { { 49.5f, 0.0f }, { -49.5f, 0.0f }, { 0.0f, 49.5f }, { 0.0f, -49.5f } };

		float[,] wallXZScale = new float[,] { { 1.0f, scale }, { 1.0f, scale }, { scale, 1.0f }, { scale, 1.0f } };

		for (int i = 0; i < 4; i++) {
			var wallObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

			wallObject.transform.position = new Vector3(wallXZPos[i, 0], wallYPos, wallXZPos[i, 1]);
			wallObject.transform.localScale = new Vector3(wallXZScale[i, 0], wallYScale, wallXZScale[i, 1]);

			wallObject.GetComponent<Renderer>().material.color = Color.blue;

			wallObject.name = "OuterWall " + i;

		}
	}

	/****************************************************************
	 * 	NAME: 			MakeInterWalls
	 *  DESCRIPTION:	Makes the inter wall for the door
	 * 
	 * ***************************************************************/

	void MakeInterWalls() {

		float[] wallXPos = { 35.5f, 35.5f, -35.5f, -35.5f }; 

		float wallYPos = 2.5f;

		float wallYScale = wallYPos * 2;

		float[] wallZPos = { 31.5f, -31.5f , -31.5f, 31.5f };

		for (int i = 0; i < 4; i++) {
			var wallObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

			wallObject.transform.position = new Vector3(wallXPos[i], wallYPos, wallZPos[i]);

			wallObject.transform.localScale = new Vector3(2.0f, wallYScale, 35.0f);

			wallObject.name = "InterWall " + i;

		}

	}

	/****************************************************************
	 * 	NAME: 			MakeDoor
	 *  DESCRIPTION:	Makes the doors for use and calls the switch 
	 * 					method for one of the doors
	 * 
	 * ***************************************************************/
	void MakeDoor(){

		float[] doorXPos = {35.5f, -35.5f};

		float doorYPos = 2.5f;

		for (int i = 0; i < 2; i++) {
			var doorObject = GameObject.CreatePrimitive (PrimitiveType.Cube);

			doorObject.transform.position = new Vector3 (doorXPos[i], doorYPos, 0.0f);

			doorObject.transform.localScale = new Vector3 (1.0f, doorYPos*2, 30.0f);

			doorObject.name = "Door "+ i;

			doorObject.GetComponent<Renderer> ().material.color = Color.yellow;

			doorObject.AddComponent<DoorOpenDevice> ();

			if (i == 0) 
				MakeDoorSwitch ();
			else 
				MakePressurePlate (new Vector3 (-35.0f, doorYPos, 0.0f ),new Vector3 (4.0f, 1, 1), doorObject.transform);

		}


	}

	/****************************************************************
	 * 	NAME: 			MakeDoor
	 *  DESCRIPTION:	Makes the door for use and the call the switch 
	 * 					method
	 * 
	 * ***************************************************************/
	void MakeDoorSwitch(){

		var switchObject = GameObject.CreatePrimitive (PrimitiveType.Cube);

		switchObject.transform.position = new Vector3 (34.65f,1.75f,14.75f);

		switchObject.transform.Rotate(new Vector3(0.0f, 270.0f, 0.0f));

		switchObject.name = "Switch";

		switchObject.GetComponent<Renderer> ().material.color = Color.yellow;

		switchObject.AddComponent<DeviceOperator> ();

		switchObject.AddComponent<ColorChangeDevice> ();

	}

	/****************************************************************
	 * 	NAME: 			MakePressurePlate
	 *  DESCRIPTION:	Make a pressure plate for the player to walk 
	 * 					into so the door can open.
	 * 
	 * ***************************************************************/
	void MakePressurePlate(Vector3 position, Vector3 scale, Transform parent){

		var pressurePlate = GameObject.CreatePrimitive (PrimitiveType.Cube);

		pressurePlate.transform.SetParent (parent);

		pressurePlate.transform.position = position;
		pressurePlate.transform.localScale = scale;
		pressurePlate.name = "Pressure Plate";

		pressurePlate.GetComponent<BoxCollider> ().isTrigger = true;

		pressurePlate.GetComponent<Renderer> ().material.shader = Shader.Find("Transparent/Diffuse");;

		pressurePlate.GetComponent<Renderer> ().material.color = new Color(0.0f, 1.0f, 0.0f, 0.75f);

		pressurePlate.AddComponent<DeviceTrigger> ();

	}

	/****************************************************************
	 * 	NAME: 			MakePlatforms
	 *  DESCRIPTION:	Make two platforms for the player to interact 
	 * 					in.
	 * 
	 * ***************************************************************/
	void MakePlatforms(){
		Vector3[] locations = new Vector3[]{new Vector3(5.0f,1.0f,5.0f),new Vector3(1.0f,1.5f,5.5f) };
		Vector3[] size = new Vector3[]{new Vector3(4.0f,1.5f,4.0f), new Vector3(4.0f,3.0f,4.0f) };

		for (int i = 0; i < 2; ++i) {
			var platfrom = GameObject.CreatePrimitive (PrimitiveType.Cube);

			platfrom.name = "Platform " + i;

			platfrom.transform.position = locations [i];
			platfrom.transform.localScale = size [i];
		}

	}

	/******************************************************************
	 * 	NAME: 			MakeBlocks
	 *  DESCRIPTION:	Makes a Blocks for the player to interact with
	 * 
	 * ***************************************************************/

	void MakeBlocks(){

		Vector3[] blockPos = {new Vector3(-4.2f,.5f,-2.3f),new Vector3(-4.2f,.5f,-1.2f),new Vector3(-4.2f,.5f,-.1f)
			,new Vector3(-4.2f,1.5f,-1.9f),new Vector3(-4.2f,1.5f,-.7f)};

		for (int i = 0; i < 5; i++) {

			var blockObjects = GameObject.CreatePrimitive (PrimitiveType.Cube);

			blockObjects.transform.position = blockPos [i];
			blockObjects.name = "Blocks " + i;

			blockObjects.AddComponent<Rigidbody> ();

		}
	}

	/****************************************************************
	 * 	NAME: 			MakeItem
	 *  DESCRIPTION:	Makes a item for the player to grab and put
	 * 					in the invertory
	 * 
	 * ***************************************************************/
	void MakeItem() {

		Vector3[] positions = { new Vector3 (1.0f, 4.5f, 5.5f), new Vector3 ( 25.0f, 1.5f, 10.0f), new Vector3 ( -15.0f, 1.5f, 5.0f) };

		string[] itemName = { "Health", "Energy", "Key" };

		for (int i = 0; i < 3; i++) {

			//makes the item object
			var item = GameObject.CreatePrimitive (PrimitiveType.Sphere);

			//changes the items position and scale
			item.transform.position = positions[i];
			item.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

			item.GetComponent<Renderer> ().material.color = Color.gray;

			item.name = itemName[i];

			//set the isTrigger as true.
			item.GetComponent<SphereCollider> ().isTrigger = true;

			item.AddComponent<CollectibleItem> ();

		}

	}

	void MakeBuilding(){

		Vector3[] wallPos = new Vector3[]{ new Vector3 (-10.0f,3.0f,35.5f), new Vector3 (10.0f,3.0f, 35.5f), new Vector3 ( 0.0f, 6.5f, 35.5f) };

		Vector3[] wallScale = new Vector3[]{new Vector3(1.0f,6.0f,27.0f), new Vector3(1.0f,6.0f,27.0f), new Vector3(21.0f,1.0f,27.0f)};

		for (int i = 0; i < 3; i++) {
		
			var buildingWall = GameObject.CreatePrimitive(PrimitiveType.Cube);

			buildingWall.transform.position = wallPos [i];

			buildingWall.transform.localScale = wallScale [i];

		}

		var lightSource = new GameObject ("Building Light");

		lightSource.AddComponent<Light> ();

		lightSource.transform.position = new Vector3 (0.0f, 5.5f, 45.5f);

		lightSource.transform.Rotate(new Vector3(20,180,0));

		lightSource.GetComponent<Light> ().type = LightType.Directional;

		lightSource.GetComponent<Light> ().enabled = false;

		lightSource.AddComponent<LightScript> ();

		MakePressurePlate (new Vector3 (0.0f,3.0f,29.0f), new Vector3 ( 20.0f, 6.0f, 2.0f), lightSource.transform);

	}
}