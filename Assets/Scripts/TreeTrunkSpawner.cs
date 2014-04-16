using UnityEngine;
using System.Collections;

public class TreeTrunkSpawner : MonoBehaviour {

	public Transform treeTrunk;
	public Transform spawner;

	public float distance = 5.0f;
	public float[] pos;// = new float[3];//{ 90.0f, 225.0f, 315.0f };
	// Use this for initialization

	public int currentRotation = 0;
	public float[] rotations;
	public float moveSpeed = 40.0f;

	public bool moveRight = false;
	public bool moveLeft = false;

	void Start () {
		pos = new float[3];
		pos[0] = 30.0f;
		pos[1] = 150.0f;
		pos[2] = 270.0f;

		rotations = new float[3];
		rotations[0] = 0.0f;
		rotations[1] = 120.0f;
		rotations[2] = 240.0f;

		Transform tree1 = (Transform)Instantiate(treeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*pos[0]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[0]/180.0f)), Quaternion.identity);
		Transform tree2 = (Transform)Instantiate(treeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*pos[1]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[1]/180.0f)), Quaternion.identity);
		Transform tree3 = (Transform)Instantiate(treeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*pos[2]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[2]/180.0f)), Quaternion.identity);
	
		tree1.transform.parent = spawner;
		tree2.transform.parent = spawner;
		tree3.transform.parent = spawner;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.D) && !moveLeft && !moveRight) {
			moveRight = true;
			if( currentRotation >= 2 ) {
				currentRotation = 0;
			}
			else {
				currentRotation++;
			}
		}
		else if(Input.GetKeyDown (KeyCode.A) && !moveLeft && !moveRight) {
			moveLeft = true;
			if( currentRotation <= 0 ) {
				currentRotation = 2;
			}
			else {
				currentRotation--;
			}
		}

		if (moveRight) {
			if( rotations[currentRotation] == 0.0f ) {
				if( spawner.eulerAngles.y >= rotations[2] ) {
					spawner.Rotate (Vector3.up * Time.deltaTime * moveSpeed, Space.World);
				}
				else {
					Vector3 set = spawner.eulerAngles;
					set.y = rotations[currentRotation];
					spawner.eulerAngles = set;
					moveRight = false;
				}
			}
			else if( spawner.eulerAngles.y >= rotations[currentRotation] ) {
				Vector3 set = spawner.eulerAngles;
				set.y = rotations[currentRotation];
				spawner.eulerAngles = set;
				moveRight = false;
			}
			else {
				spawner.Rotate (Vector3.up * Time.deltaTime * moveSpeed, Space.World);
			}
		}
		else if (moveLeft) {
			if( rotations[currentRotation] == 0.0f ) {
				if( spawner.eulerAngles.y-0.1f <= rotations[1] ) {
					spawner.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
				}
				else {
					Vector3 set = spawner.eulerAngles;
					set.y = rotations[currentRotation];
					spawner.eulerAngles = set;
					moveLeft = false;
				}
			}
			else if( rotations[currentRotation] == 240.0f ) {
				if( spawner.eulerAngles.y == 0.0f || spawner.eulerAngles.y > rotations[currentRotation] ) {
					spawner.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
				}
				else {
					Vector3 set = spawner.eulerAngles;
					set.y = rotations[currentRotation];
					spawner.eulerAngles = set;
					moveLeft = false;
				}
			}
			else if( spawner.eulerAngles.y <= rotations[currentRotation] ) {
				Vector3 set = spawner.eulerAngles;
				set.y = rotations[currentRotation];
				spawner.eulerAngles = set;
				moveLeft = false;
			}
			else {
				spawner.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
			}
		}
	}
}
