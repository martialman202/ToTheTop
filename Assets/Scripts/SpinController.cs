using UnityEngine;
using System.Collections;

public class SpinController : MonoBehaviour {

	public int currentRotation = 0;
	public float[] rotations;
	public float moveSpeed = 250.0f;
	
	public bool moveRight = false;
	public bool moveLeft = false;

	private bool onTree = false;
	
	private GameObject spawner;

	// Use this for initialization
	void Start () {
		spawner = this.gameObject;
		
		rotations = new float[3];
		rotations[0] = 0.0f;
		rotations[1] = 120.0f;
		rotations[2] = 240.0f;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject m = GameObject.Find ("Monkey");
		testAutoMonkey monkey = m.GetComponent<testAutoMonkey> ();
		onTree = monkey.onTree;
		if ((Input.GetKeyDown (KeyCode.A) || (Input.GetKeyDown("left"))) && !moveLeft && !moveRight && onTree && !monkey.isJumping) {
			moveRight = true;

			if( currentRotation >= 2 ) {
				currentRotation = 0;
			}
			else {
				currentRotation++;
			}
		}
		else if((Input.GetKeyDown (KeyCode.D) || (Input.GetKeyDown("right"))) && !moveLeft && !moveRight && onTree && !monkey.isJumping) {
			moveLeft = true;

			if( currentRotation <= 0 ) {
				currentRotation = 2;
			}
			else {
				currentRotation--;
			}
		}
		
		if (moveRight) {
			monkey.onTree = false;
			if( rotations[currentRotation] == 0.0f ) {
				if( spawner.transform.eulerAngles.y >= rotations[2] ) {
					spawner.transform.Rotate (Vector3.up * Time.deltaTime * moveSpeed, Space.World);
				}
				else {
					Vector3 set = spawner.transform.eulerAngles;
					set.y = rotations[currentRotation];
					spawner.transform.eulerAngles = set;
					moveRight = false;
					monkey.onTree = true;
				}
			}
			else if( spawner.transform.eulerAngles.y >= rotations[currentRotation] ) {
				Vector3 set = spawner.transform.eulerAngles;
				set.y = rotations[currentRotation];
				spawner.transform.eulerAngles = set;
				moveRight = false;
				monkey.onTree = true;
			}
			else {
				spawner.transform.Rotate (Vector3.up * Time.deltaTime * moveSpeed, Space.World);
			}
		}
		else if (moveLeft) {
			monkey.onTree = false;
			if( rotations[currentRotation] == 0.0f ) {
				if( spawner.transform.eulerAngles.y-0.1f <= rotations[1] ) {
					spawner.transform.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
				}
				else {
					Vector3 set = spawner.transform.eulerAngles;
					set.y = rotations[currentRotation];
					spawner.transform.eulerAngles = set;
					moveLeft = false;
					monkey.onTree = true;
				}
			}
			else if( rotations[currentRotation] == 240.0f ) {
				if( spawner.transform.eulerAngles.y == 0.0f || spawner.transform.eulerAngles.y > rotations[currentRotation] ) {
					spawner.transform.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
				}
				else {
					Vector3 set = spawner.transform.eulerAngles;
					set.y = rotations[currentRotation];
					spawner.transform.eulerAngles = set;
					moveLeft = false;
					monkey.onTree = true;
				}
			}
			else if( spawner.transform.eulerAngles.y <= rotations[currentRotation] ) {
				Vector3 set = spawner.transform.eulerAngles;
				set.y = rotations[currentRotation];
				spawner.transform.eulerAngles = set;
				moveLeft = false;
				monkey.onTree = true;
			}
			else {
				spawner.transform.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
			}
		}

		if( currentRotation == 0 ) {
			monkey.jumpDir = new Vector3(0,0,1);
		}
		else if( currentRotation == 1 ) {
			monkey.jumpDir = new Vector3(Mathf.Sqrt(3.0f)/2.0f,0,-0.5f);
		}
		else {
			monkey.jumpDir = new Vector3(-Mathf.Sqrt(3.0f)/2.0f,0,-0.5f);
		}
	}
}
