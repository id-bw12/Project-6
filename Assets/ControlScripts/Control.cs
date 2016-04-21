﻿using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	private const string CHARACTER_MESH_PATH = "Graphics/Models/player",
	ANIMATOR_CONTROLLER = "Graphics/Animator/player",
	MATERIAL_COLOR = "Graphics/Materials/player";

	// Use this for initialization
	void Start () {
	
		SetupLevel ();

		MakePlayer ();

		SetupCamera ();

        MakeItem();

	}
	
	// Update is called once per frame
	void Update () {

	}

	void SetupLevel(){

        var floorObject = GameObject.CreatePrimitive (PrimitiveType.Plane);

		floorObject.transform.position = new Vector3 (0.0f,0.0f,0.0f);

		floorObject.transform.localScale = new Vector3 (10, 1, 10);

		floorObject.GetComponent<Renderer> ().material.color = Color.blue;

        MakeOuterWalls();

		MakePlatforms ();

	}

	void MakePlayer(){
	
		GameObject playerObject = Instantiate(Resources.Load (CHARACTER_MESH_PATH) as GameObject);

		playerObject.name = "Player";

		playerObject.transform.position = new Vector3 (0.0f, 1.1f, 0.0f);

		playerObject.GetComponent<Animator> ().runtimeAnimatorController = Resources.Load (ANIMATOR_CONTROLLER)as RuntimeAnimatorController;

		playerObject.GetComponent<Animator> ().applyRootMotion = false;

		playerObject.GetComponentInChildren<SkinnedMeshRenderer> ().material = Resources.Load (MATERIAL_COLOR)as Material;

		playerObject.AddComponent<RelativeMovement> ();
		playerObject.AddComponent<AnimatePlayer> ();
	}

	void SetupCamera (){

		GameObject.Find ("Main Camera").AddComponent<OrbitCamera> ();
	}

    void MakeOuterWalls() {
        float scale = 100.0f;

        float wallYPos = 7.5f;

        float wallYScale = wallYPos * 2;

        float[,] wallXZPos = new float[,] { { 49.5f, 0.0f }, { -49.5f, 0.0f }, { 0.0f, 49.5f }, { 0.0f, -49.5f } };

        float[,] wallXZScale = new float[,] { { 1.0f, scale }, { 1.0f, scale }, { scale, 1.0f }, { scale, 1.0f } };

        for (int i = 0; i < 4; i++)
        {
            var wallObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

            wallObject.transform.position = new Vector3(wallXZPos[i, 0], wallYPos, wallXZPos[i, 1]);
            wallObject.transform.localScale = new Vector3(wallXZScale[i, 0], wallYScale, wallXZScale[i, 1]);

            wallObject.GetComponent<Renderer>().material.color = Color.blue;

        }
    }

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

    void MakeItem() {

        //makes the item object
        var item = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //changes the items position and scale
        item.transform.position = new Vector3(8.0f,1.5f,4.0f);
        item.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //set the isTrigger as true.
        item.GetComponent<BoxCollider>().isTrigger = true;
    }
}
