using UnityEngine;
using System.Collections;

public class SpinController : MonoBehaviour {

	public int currentRotation = 0;
	public float[] rotations;
	public float moveSpeed = 175.0f;
	
	public bool moveRight = false;
	public bool moveLeft = false;

	private bool onTree = false;
	
	private GameObject spawner;
	private GameObject monkeyObject;
	private testAutoMonkey monkey;
	private MonkeyMouse mmouse;

	private GameObject mainCam;
	private SoundMainScene sounds;

	// Use this for initialization
	void Start () {

		mainCam = GameObject.FindGameObjectWithTag("MainCamera");

		spawner = this.gameObject;
		
		rotations = new float[3];
		rotations[0] = 0.0f;
		rotations[1] = 120.0f;
		rotations[2] = 240.0f;

		monkeyObject = GameObject.Find ("Monkey");
		monkey = monkeyObject.GetComponent<testAutoMonkey> ();
		mmouse = this.GetComponent<MonkeyMouse> ();

		//Audio
		sounds = mainCam.GetComponent<SoundMainScene> ();
	}
	
	// Update is called once per frame
	void Update () {
		onTree = monkey.onTree;
		if ((Input.GetKeyDown (KeyCode.A) || (Input.GetKeyDown("left")) || (mmouse.MoveRight())) && !moveLeft && !moveRight && onTree && !monkey.isJumping) {
			moveRight = true;
			monkey.jumpState = testAutoMonkey.JumpState.left;

			if (sounds.playSoundEffects)
				sounds.audioSources[4].Play();

			if( currentRotation >= 2 ) {
				currentRotation = 0;
			}
			else {
				currentRotation++;
			}
		}
		else if((Input.GetKeyDown (KeyCode.D) || (Input.GetKeyDown("right")) || (mmouse.MoveLeft())) && !moveLeft && !moveRight && onTree && !monkey.isJumping) {
			moveLeft = true;
			monkey.jumpState = testAutoMonkey.JumpState.right;

			if (sounds.playSoundEffects)
				sounds.audioSources[4].Play();

			if( currentRotation <= 0 ) {
				currentRotation = 2;
			}
			else {
				currentRotation--;
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

	void FixedUpdate() {
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
					monkey.jumpState = testAutoMonkey.JumpState.none;
				}
			}
			else if( spawner.transform.eulerAngles.y >= rotations[currentRotation] ) {
				Vector3 set = spawner.transform.eulerAngles;
				set.y = rotations[currentRotation];
				spawner.transform.eulerAngles = set;
				moveRight = false;
				monkey.onTree = true;
				monkey.jumpState = testAutoMonkey.JumpState.none;
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
					monkey.jumpState = testAutoMonkey.JumpState.none;
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
					monkey.jumpState = testAutoMonkey.JumpState.none;
				}
			}
			else if( spawner.transform.eulerAngles.y <= rotations[currentRotation] ) {
				Vector3 set = spawner.transform.eulerAngles;
				set.y = rotations[currentRotation];
				spawner.transform.eulerAngles = set;
				moveLeft = false;
				monkey.onTree = true;
				monkey.jumpState = testAutoMonkey.JumpState.none;
			}
			else {
				spawner.transform.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
			}
		}
	}
}
