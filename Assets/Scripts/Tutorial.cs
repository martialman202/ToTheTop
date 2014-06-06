using UnityEngine;
using System.Collections;

public class Tutorial : InfiniteLevelManager {
	private MonkeyMouse mmouse;
	public Transform treeTop;
	public Transform bananas;
	public float bananasHeightOffset = 5.0f;
	
	public enum TutorialState {Begin,Active,Inactive,End};
	public TutorialState state = TutorialState.Active;
	
	public int outroHeight = 25;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		mmouse = this.GetComponent<MonkeyMouse> ();
		empty = true;
	}

	// Update is called once per frame
	void Update () {
		// run clean up to recycle any trunks that are no longer on scene
		bool wasRowRecycled = base.cleanUp();

		// create a new row if a row was just recycled back into the trunk pool
		if (wasRowRecycled && state == TutorialState.Active) {
			if (!empty) {

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
}
