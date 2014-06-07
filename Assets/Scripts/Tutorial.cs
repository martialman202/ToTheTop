﻿using UnityEngine;
using System.Collections;

public class Tutorial : InfiniteLevelManager {
	public Transform treeTop;
	public Transform bananas;
	public float bananasHeightOffset = 5.0f;

	public Texture2D arrowMove;
	public Texture2D arrowJump;
	private float arrowWidth = 0.7f * Screen.width;
	private float arrowHeight = 0.17f * Screen.width;
	private float deltaMove;
	private float deltaJump;
	
	public enum TutorialState {Begin,Active,Inactive,End,BeeHive,Snake,Deathvine};
	public TutorialState state = TutorialState.Begin;
	public TutorialState mode = TutorialState.Begin;

	private enum Arrows {None,Move,Jump,Swipe};
	private Arrows arrow = Arrows.None;
	
	public int outroHeight = 10;

	private MonkeyMouse mmouse;
	private int counter = 0;
	private GameObject monkey;
	private testAutoMonkey monkeyController;
	private float timeCounter = 0.0f;
	private bool spawned = false;
	private bool nextTutorial = false;
	void OnGUI () {
		switch (arrow) {
		case Arrows.None:
			break;
		case Arrows.Move:
			GUI.Label (new Rect (0.05f * Screen.width, 0.01f * Screen.height, 0.9f*Screen.width, 3 * arrowHeight), arrowMove);
			break;
		case Arrows.Jump:
			GUI.Label (new Rect (0.05f * Screen.width, 0.01f * Screen.height, 0.9f*Screen.width, 3 * arrowHeight), arrowJump);
			break;
		case Arrows.Swipe:
			break;
		default:
			break;
		}
	}

	// Use this for initialization
	public override void Start () {
		base.Start ();
		mmouse = this.GetComponent<MonkeyMouse> ();
		monkey = GameObject.FindWithTag ("Player");
		monkeyController = monkey.GetComponent<testAutoMonkey> ();
		empty = true;
		state = TutorialState.Active;
		mode = TutorialState.Inactive;

		deltaMove = emptyTrunk.gameObject.renderer.bounds.size.y * 3;
		deltaJump = emptyTrunk.gameObject.renderer.bounds.size.y * 3;//(Screen.height / Camera.main.orthographicSize) * 0.06f;
		Invoke("ActivateTutorial",2);
	}

	// Update is called once per frame
	void Update () {
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
					SpawnBeeHive();
					break;
				case TutorialState.Snake:
					SpawnSnakes();
					break;
				case TutorialState.Deathvine:
					SpawnDeathvine();
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
		default:
			break;
		}

		print (counter);

		if (monkeyController.lifePoints < 3) {
			//counter = counter - (3 - monkeyController.lifePoints);
			//if (counter < 0)
			//	counter = 0;
			monkeyController.lifePoints = 3;
		}
	}

	void ActiveTutorial () {
		arrow = Arrows.Move;
		if (monkeyController.onTree && ListenForMove () && counter < 3)
			counter++;

		if (counter >= 3 && counter < 6) {
			arrow = Arrows.Jump;
			if (monkeyController.onTree && ListenForJump ()) {
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
		Ray ray = new Ray (monkey.transform.position, Vector3.up);
		
		if (Physics.Raycast (ray, out hit, deltaJump) && counter == 0 && monkeyController.onTree) {
			if (hit.collider.name == "ReducedCartoonBeehive") {
				arrow = Arrows.Jump;
			}
			if (monkeyController.onTree && ListenForJump ())
				counter++;
		} else if (Physics.Raycast (ray, out hit, deltaMove) && counter == 1 && !monkeyController.isJumping) {
			if (hit.collider.name == "ReducedCartoonBeehive") {
				arrow = Arrows.Move;
			}
			if (monkeyController.onTree && ListenForMove ())
				counter++;
		} else
			arrow = Arrows.None;

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
		Ray ray = new Ray (monkey.transform.position, Vector3.up);
		
		if (Physics.Raycast (ray, out hit, deltaMove) && counter <= 1) {
			if (hit.collider.name == "CartoonSnakePrefab") {
				arrow = Arrows.Move;
			}
			if (monkeyController.onTree && ListenForMove ())
				counter++;
		} else
			arrow = Arrows.None;

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
		Ray ray = new Ray (monkey.transform.position, Vector3.up);
		
		if (Physics.Raycast (ray, out hit, deltaMove) && counter <= 1) {
			if (hit.collider.name == "Prefab_DeathVine(Clone)") {
				arrow = Arrows.Jump;
			}
			if (monkeyController.onTree && ListenForJump ())
				counter++;
		} else
			arrow = Arrows.None;
		
		if (counter <= 1 && !spawned) {
			empty = false;
			StartCoroutine (SpawnObstacle ());
		}
		else if (counter > 1) {
			arrow = Arrows.None;
			mode = TutorialState.End;
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

	bool ListenForJump () { // returns true if player jumps
		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown ("up") || mmouse.MoveUp ())
			return true;
		return false;
	}
	
	bool ListenForMove () { // returns true if player moves to another tree
		if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown("left") || mmouse.MoveRight() ||
		    Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown("right") || mmouse.MoveLeft())
			return true;
		return false;
	}
	
	bool ListenForSwipe () { // returns true if player swipes
		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown ("up") || mmouse.MoveUp () ||
		    Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown ("left") || mmouse.MoveRight () || 
		    Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown ("right") || mmouse.MoveLeft ())
			return true;
		return false;
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
		yield return new WaitForSeconds (1.0f);
		nextTutorial = true;
	}
}
