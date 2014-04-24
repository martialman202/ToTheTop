using UnityEngine;
using System.Collections;

public class testAutoMonkey : MonoBehaviour {

	public int lifePoints = 3;

	public Vector3 moveDirection = new Vector3(0,0,1); //starts forward, when hits tree, is up
	public float moveSpeed = 1.0f; 
	public bool onTree = false; //does this stay true after monkey makes contact with tree?

	private GameObject mainCam; //camera should stop following monkey on lose //Will be used for camera work later
	private enum MonkeyState {initial=1, climbing=2, lose=3, win=4};
	MonkeyState monkeyState = MonkeyState.initial;

	public bool isJumping = false;
	public float jumpVel = 0.0f;
	public float simGravity = 2.0f;
	public float jumpImpulse = -60.0f;
	public float origZ = 0.0f;

	public AudioClip [] Clips;
	private AudioSource[] audioSources;
	public bool playJumpSound;

	// Use this for initialization
	void Start () {
		//Testing
		print (gameObject.name + " has been started.");

		//find main camera
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");

		//Audio
		playJumpSound = false;
		audioSources = new AudioSource[Clips.Length];
		for (int i = 0; i < 2; i++) {
			print(i);
			audioSources[i] = this.gameObject.AddComponent("AudioSource") as AudioSource;
			audioSources[i].clip = Clips[i];
		}

		//Set position
		//TODO: have the monkey start relative to the tree spawners spawn distance

	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Tree") {
			origZ = gameObject.transform.position.z;
			moveDirection = new Vector3 (0, 1, 0);
			onTree = true;
			monkeyState = MonkeyState.climbing;
		}

		return;

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Obstacle") {
			lifePoints--;
			print (gameObject.name + " hit " + other.gameObject.name +"!");
			audioSources[0].Play(); //play audio
		}
	}

	// Update is called once per frame
	void Update () {

		if (lifePoints == 0) {
			monkeyState = MonkeyState.lose;
		}

		if (monkeyState == MonkeyState.initial || monkeyState == MonkeyState.climbing) {
				gameObject.transform.Translate (moveDirection * moveSpeed * Time.deltaTime);

				if( !isJumping && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("up")) ) {
					isJumping = true;
					jumpVel = jumpImpulse;
				audioSources[1].Play();
				}

				if( isJumping ) {
					jumpVel += simGravity;
					if( origZ <= gameObject.transform.position.z + jumpVel*Time.deltaTime ) {
						Vector3 newPos = gameObject.transform.position;
						newPos.z = origZ;
						gameObject.transform.position = newPos;
						isJumping = false;
					}
					else {
						gameObject.transform.Translate ( new Vector3(0,0,-1) * jumpVel * Time.deltaTime );
					}
				}
		}
		else if (monkeyState == MonkeyState.lose) {
			lose ();
		}




	}

	void lose() {
		Debug.Log ("You Lose.");
		Debug.Break ();
	}
	
	void win() {
		Debug.Log ("You Win");
		//Debug.Break ();
	}
	
}
