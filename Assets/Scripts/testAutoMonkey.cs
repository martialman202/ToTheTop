using UnityEngine;
using System.Collections;

public class testAutoMonkey : MonoBehaviour {

	public int lifePoints = 3;

	public Vector3 moveDirection = new Vector3(0,0,1); //starts forward, when hits tree, is up
	public float moveSpeed = 1.0f; 
	public bool onTree = false;
	public float repeatDamagePeriod = 0.5f;
	private float lastHitTime = 0.0f;

	private GameObject mainCam; //camera should stop following monkey on lose //Will be used for camera work later
	private Color origColor;
	private enum MonkeyState {initial=1, climbing=2, lose=3, win=4};
	MonkeyState monkeyState = MonkeyState.initial;
	//int monkeyState = (int)MonkeyState.initial;

	public bool isJumping = false;
	public float jumpVel = 0.0f;
	public float simGravity = 2.0f;
	public float jumpImpulse = -60.0f;
	public float origZ = 0.0f;

	// Use this for initialization
	void Start () {
		print (gameObject.name + " has been started.");
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		origColor = gameObject.renderer.material.color;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Tree" && !onTree && monkeyState != MonkeyState.win) {
			origZ = gameObject.transform.position.z;
			moveDirection = new Vector3 (0, 1, 0);
			onTree = true;
			monkeyState = MonkeyState.climbing;
			print ("collision detected!");
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Obstacle") {
			if (Time.time > lastHitTime + repeatDamagePeriod) {
				lifePoints--;
				print (gameObject.name + " hit " + other.gameObject.name + "!");
				lastHitTime = Time.time;
			}
		}
		if (other.gameObject.tag == "TreeTop") {
			print ("hit " + other.gameObject.tag);
			monkeyState = MonkeyState.win;
		}
	}

	bool MouseMoveLeft()
	{
		return Input.GetMouseButton (0);
	}

	// Update is called once per frame
	void Update () {
		//print ("Life: " + lifePoints);
		if (lifePoints <= 0) {
			monkeyState = MonkeyState.lose;
		}

		if (monkeyState == MonkeyState.initial || monkeyState == MonkeyState.climbing) {
			gameObject.transform.Translate (moveDirection * moveSpeed * Time.deltaTime);

			if (!isJumping && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown ("up")) && onTree) {
				isJumping = true;
				jumpVel = jumpImpulse;
			}

			if (isJumping) {
				jumpVel += simGravity;
				if (origZ <= gameObject.transform.position.z + jumpVel * Time.deltaTime) {
					Vector3 newPos = gameObject.transform.position;
					newPos.z = origZ;
					gameObject.transform.position = newPos;
					isJumping = false;
				} else {
					gameObject.transform.Translate (new Vector3 (0, 0, -1) * jumpVel * Time.deltaTime);
				}
			}
		} else if (monkeyState == MonkeyState.lose) {
			lose ();
		} else if (monkeyState == MonkeyState.win) {
			if (isJumping) { // finish jumping before winning
				jumpVel += simGravity;
				if (origZ <= gameObject.transform.position.z + jumpVel * Time.deltaTime) {
					Vector3 newPos = gameObject.transform.position;
					newPos.z = origZ;
					gameObject.transform.position = newPos;
					isJumping = false;
					win ();
				} else {
					gameObject.transform.Translate (new Vector3 (0, 0, -1) * jumpVel * Time.deltaTime);
				}
			}
			else if(onTree)
				win ();
		}
	}

	void lose() {
		print("You Lose.");
		Application.LoadLevel(0);
	}
	
	void win() {
		print("You Win");
		Application.LoadLevel(0);
	}
	
}
