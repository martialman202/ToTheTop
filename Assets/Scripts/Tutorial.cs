﻿using UnityEngine;
using System.Collections;

public class Tutorial : InfiniteLevelManager {
	public Transform treeTop;
	public Transform coconut;
	public Transform bananas;
	public float bananasHeightOffset = 5.0f;

	public Texture2D arrowSwipe;
	public Texture2D arrowMove;
	public Texture2D arrowJump;
	private float arrowWidth = 0.7f * Screen.width;
	private float arrowHeight = 0.17f * Screen.width;
	private float deltaMove;
	private float deltaJump;

	public Texture2D coconutDescription;
	public Texture2D heart1;
	public Texture2D heart2;
	public Texture2D heart3;
	
	public enum TutorialState {Begin,Active,Inactive,End,BeeHive,Snake,Deathvine,Coconut,CoconutDescription,Hearts};
	public TutorialState state = TutorialState.Begin;
	public TutorialState mode = TutorialState.Begin;

	public enum Arrows {None,Move,Jump,Swipe};
	public Arrows arrow = Arrows.None;

	private enum Graphic {None,CoconutDescription, Hearts};
	private Graphic graphic = Graphic.None;
	
	public int outroHeight = 5;

	private float coconutSpawnHeight;
	public int counter = 0;
	private GameObject monkey;
	private testAutoMonkey monkeyController;
	private float timeCounter = 0.0f;
	private bool spawned = false;
	private bool nextTutorial = false;
	private float trunkHeight;
	private float trunkWidth;
	private Vector3 rayPosition;

	private GameObject [] obstacles;

	private bool onTree = true;
	private bool isJumping = false;

	void OnGUI () {
		switch (arrow) {
		case Arrows.None:
			break;
		case Arrows.Swipe:
			GUI.Label (new Rect (0.05f * Screen.width, 0.01f * Screen.height, 0.9f*Screen.width, 4 * arrowHeight), arrowSwipe);
			break;
		case Arrows.Move:
			GUI.Label (new Rect (0.05f * Screen.width, 0.01f * Screen.height, 0.9f*Screen.width, 4 * arrowHeight), arrowMove);
			break;
		case Arrows.Jump:
			GUI.Label (new Rect (0.05f * Screen.width, 0.01f * Screen.height, 0.9f*Screen.width, 4 * arrowHeight), arrowJump);
			break;
		default:
			break;
		}

		switch (graphic) {
		case Graphic.None:
			break;
		case Graphic.CoconutDescription:
			GUI.Label (new Rect (0.05f * Screen.width, 0.01f * Screen.height, 0.9f*Screen.width, 4 * arrowHeight), coconutDescription);
			break;
		case Graphic.Hearts:
			GUI.Label (new Rect (0.05f * Screen.width, 0.1f * Screen.height, 0.9f*Screen.width, 4 * arrowHeight), heart1);
			GUI.Label (new Rect (0.05f * Screen.width, (0.1f * Screen.height) + arrowHeight, 0.9f*Screen.width, 4 * arrowHeight), heart2);
			GUI.Label (new Rect (0.05f * Screen.width, (0.1f * Screen.height) + (2 * arrowHeight), 0.9f*Screen.width, 4 * arrowHeight), heart3);
			break;
		default:
			break;
		}
	}

	// Use this for initialization
	public override void Start () {
		base.Start ();
		monkey = GameObject.FindWithTag ("Player");
		monkeyController = monkey.GetComponent<testAutoMonkey> ();
		empty = true;
		state = TutorialState.Active;
		mode = TutorialState.Inactive;


		trunkWidth = emptyTrunk.gameObject.renderer.bounds.size.z;
		trunkHeight = emptyTrunk.gameObject.renderer.bounds.size.y;
		deltaMove = emptyTrunk.gameObject.renderer.bounds.size.y * 4;
		deltaJump = emptyTrunk.gameObject.renderer.bounds.size.y * 4;//(Screen.height / Camera.main.orthographicSize) * 0.06f;

		coconutSpawnHeight = emptyTrunk.gameObject.renderer.bounds.size.y * 50;

		Invoke("ActivateTutorial",2);
	}

	// Update is called once per frame
	void Update () {
		// update ray position
		rayPosition = monkey.transform.position;
		rayPosition.y += trunkHeight * 2;

		// run clean up to recycle any trunks that are no longer on scene
		bool wasRowRecycled = base.cleanUp();

		// create a new row if a row was just recycled back into the trunk pool
		if (wasRowRecycled && state == TutorialState.Active) {
			if (spawnObstacle && !empty) {
				switch (mode) {
				case TutorialState.Inactive:
					base.extendEmptyTrunks ();
					break;
				case TutorialState.BeeHive:
					SpawnBeeHive ();
					break;
				case TutorialState.Snake:
					SpawnSnakes ();
					break;
				case TutorialState.Deathvine:
					SpawnDeathvine ();
					break;
				case TutorialState.Coconut:
					SpawnCoconut ();
					break;
				case TutorialState.End:
					state = mode;
					break;
				default:
					base.extendEmptyTrunks ();
					break;
				}
				empty = false;
				spawnObstacle = false;
			} else {
				base.extendEmptyTrunks ();
			}
			//extendWBeehivesOrSnakes(2,2,1);
			//extendWBrush();
			//extendCustomRow(1,0,2);
		} else if (state == TutorialState.End) {
			LoadOutro();
			state = TutorialState.Inactive;
		}

		switch (mode) {
		case TutorialState.Inactive:
			arrow = Arrows.None;
			graphic = Graphic.None;
			break;
		case TutorialState.Active:
			ActiveTutorial ();
			break;
		case TutorialState.BeeHive:
			BeeHiveTutorial ();
			break;
		case TutorialState.Snake:
			if (nextTutorial)
				SnakeTutorial ();
			break;
		case TutorialState.Deathvine:
			if (nextTutorial)
				DeathvineTutorial ();
			break;
		case TutorialState.CoconutDescription:
			if (!spawned)
				StartCoroutine (CoconutDescriptionTransition ());
			break;
		case TutorialState.Coconut:
			CoconutTutorial ();
			break;
		case TutorialState.Hearts:
			if (!spawned)
				StartCoroutine (Hearts ());
			break;
		case TutorialState.End:
			state = mode;
			mode = TutorialState.Inactive;
			break;
		default:
			break;
		}

		if (monkeyController.lifePoints < 3) {
			counter = counter - (3 - monkeyController.lifePoints);
			if (counter < 0)
				counter = 0;
			monkeyController.lifePoints = 3;
		}

		// remove IgnoreRaycast layer
		obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		
		foreach (GameObject obstacle in obstacles) {
			obstacle.gameObject.layer = 0;
		}
	}

	void ActiveTutorial () {
		arrow = Arrows.Move;
		if (counter < 3 && ListenForMove())
			counter++;

		if (counter >= 3 && counter < 6) {
			arrow = Arrows.Jump;
			if (ListenForJump()) {
				counter++;
			}
		}

		if (counter >= 6) {
			arrow = Arrows.None;
			mode = TutorialState.BeeHive;
			counter = 0;
		}

	}

	void BeeHiveTutorial () {
		RaycastHit hit;
		rayPosition += monkey.transform.forward * 0.5f;
		Ray ray = new Ray (rayPosition, Vector3.up);
		
		if (Physics.Raycast (ray, out hit, deltaMove) && counter <= 1) {
			if (hit.transform.name == "Model_Beehive") {
				arrow = Arrows.Swipe;
			}
		} else
			arrow = Arrows.None;

		if (arrow != Arrows.None) {
			if (ListenForSwipe ())
				counter++;
		}

		if (counter <= 1 && !spawned) {
			empty = false;
			StartCoroutine (SpawnObstacle ());
		}
		else if (counter > 1) {
			arrow = Arrows.None;
			mode = TutorialState.Snake;
			counter = 0;
			StartCoroutine (TutorialTransition());
		}
	}

	void SnakeTutorial () {
		RaycastHit hit;
		//rayPosition += monkey.transform.forward * 0.75f;
		Ray ray = new Ray (rayPosition, Vector3.up);
		
		if (Physics.Raycast (ray, out hit, deltaMove) && counter <= 1) {
			if (hit.collider.name == "Model_Snake") {
				arrow = Arrows.Move;
			}
		} else
			arrow = Arrows.None;

		if (arrow != Arrows.None) {
			if (ListenForMove ())
				counter++;
		}

		if (counter <= 1 && !spawned) {
			empty = false;
			StartCoroutine (SpawnObstacle ());
		}
		else if (counter > 1) {
			arrow = Arrows.None;
			mode = TutorialState.Deathvine;
			counter = 0;
			StartCoroutine (TutorialTransition());
		}
	}

	void DeathvineTutorial () {
		RaycastHit hit;
		Ray ray = new Ray (rayPosition, Vector3.up);
		
		if (Physics.Raycast (ray, out hit, deltaMove) && counter <= 1) {
			if (hit.collider.name == "Prefab_DeathVine(Clone)") {
				arrow = Arrows.Jump;
			}
		} else
			arrow = Arrows.None;

		if (arrow != Arrows.None) {
			if (ListenForJump ())
				counter++;
		}
		
		if (counter <= 1 && !spawned) {
			empty = false;
			StartCoroutine (SpawnObstacle ());
		}
		else if (counter > 1) {
			arrow = Arrows.None;
			mode = TutorialState.CoconutDescription;
			counter = 0;
			StartCoroutine (CoconutGUI());
			StartCoroutine (TutorialTransition());
		}
	}

	void CoconutTutorial () {
		RaycastHit hit;
		Ray ray = new Ray (rayPosition, Vector3.up);
		
		if (Physics.Raycast (ray, out hit) && counter <= 1) {
			if (hit.collider.name == "Prefab_Coconut") {
				arrow = Arrows.Move;
			}
		} else
			arrow = Arrows.None;

		if (arrow != Arrows.None) {
			if (ListenForMove ())
				counter++;
		}

		GameObject c = GameObject.Find ("Prefab_Coconut");
		if (counter <= 1 && !spawned && c == null) {
			empty = false;
			StartCoroutine (SpawnObstacle ());
		}
		else if (counter > 1) {
			arrow = Arrows.None;
			mode = TutorialState.Hearts;
			counter = 0;
			StartCoroutine (TutorialTransition());
		}
	}

	private void LoadOutro()
	{
		for (int i = 0; i < outroHeight; ++i) {
			Transform tree1 = (Transform)Instantiate (emptyTrunk, new Vector3 (distance * Mathf.Cos (Mathf.PI * 150.0f / 180.0f), heightOffset * trunkCounter, distance * Mathf.Sin (Mathf.PI * 150.0f / 180.0f)), Quaternion.identity);
			Transform tree2 = (Transform)Instantiate (emptyTrunk, new Vector3 (distance * Mathf.Cos (Mathf.PI * 270.0f / 180.0f), heightOffset * trunkCounter, distance * Mathf.Sin (Mathf.PI * 270.0f / 180.0f)), Quaternion.identity);
			Transform tree3 = (Transform)Instantiate (emptyTrunk, new Vector3 (distance * Mathf.Cos (Mathf.PI * 30.0f / 180.0f), heightOffset * trunkCounter, distance * Mathf.Sin (Mathf.PI * 30.0f / 180.0f)), Quaternion.identity);

			trunkCounter++;
		}
		
		Transform tt1 = (Transform)Instantiate (treeTop, new Vector3 (distance * Mathf.Cos (Mathf.PI * 30.0f / 180.0f), heightOffset * trunkCounter, distance * Mathf.Sin (Mathf.PI * 30.0f / 180.0f)), Quaternion.identity);
		Transform tt2 = (Transform)Instantiate (treeTop, new Vector3 (distance * Mathf.Cos (Mathf.PI * 150.0f / 180.0f), heightOffset * trunkCounter, distance * Mathf.Sin (Mathf.PI * 150.0f / 180.0f)), Quaternion.identity);
		Transform tt3 = (Transform)Instantiate (treeTop, new Vector3 (distance * Mathf.Cos (Mathf.PI * 270.0f / 180.0f), heightOffset * trunkCounter, distance * Mathf.Sin (Mathf.PI * 270.0f / 180.0f)), Quaternion.identity);
		
		Transform b = (Transform)Instantiate (bananas, new Vector3 (0, heightOffset * trunkCounter + bananasHeightOffset, 0), Quaternion.identity);
	}

	void SpawnBeeHive () {
		base.extendCustomRow (1, 1, 1);
	}

	void SpawnSnakes () {
		base.extendCustomRow (2, 2, 2);
	}

	void SpawnDeathvine () {
		base.extendWBrush();
	}

	void SpawnCoconut() {
		if (monkeyController.onTree) {
			Vector3 coconutSpawnPosition = monkey.transform.position;
			coconutSpawnPosition.y  += coconutSpawnHeight;
			Transform coconutClone = (Transform)Instantiate (coconut, coconutSpawnPosition, Quaternion.identity);
			coconutClone.name = coconut.name;
		}
	}

	bool ListenForJump () { // returns true if player jumps
		bool ret = !isJumping && monkeyController.isJumping && (monkeyController.jumpState == testAutoMonkey.JumpState.up);
		isJumping = monkeyController.isJumping;
		return ret;
	}
	
	bool ListenForMove () { // returns true if player moves to another tree
		bool ret = onTree && !monkeyController.onTree && (monkeyController.jumpState == testAutoMonkey.JumpState.left || monkeyController.jumpState == testAutoMonkey.JumpState.right);
		onTree = monkeyController.onTree;
		return ret;
	}

	bool ListenForSwipe () { // returns true if player swipes
		return ListenForMove () || ListenForJump ();
	}

	void ActivateTutorial () {
		mode = TutorialState.Active;
	}

	IEnumerator SpawnObstacle () {
		spawned = true;
		yield return new WaitForSeconds (2.0f);
		if (!spawnObstacle)
			spawnObstacle = true;
		spawned = false;
	}

	IEnumerator TutorialTransition() {
		nextTutorial = false;
		yield return new WaitForSeconds (2.0f);
		nextTutorial = true;
	}

	IEnumerator CoconutGUI () {
		yield return new WaitForSeconds (2.0f);
		graphic = Graphic.CoconutDescription;
	}
		

	IEnumerator CoconutDescriptionTransition () {
		spawned = true;
		yield return new WaitForSeconds (3.0f);
		graphic = Graphic.None;
		mode = TutorialState.Coconut;	
		spawned = false;
	}

	IEnumerator Hearts () {
		spawned = true;
		yield return new WaitForSeconds (1.0f);
		graphic = Graphic.Hearts;
		yield return new WaitForSeconds (3.0f);
		mode = TutorialState.End;
		spawned = false;
	}
}
