/*
 * Note on the audio sources:
 * SoundMainScene is a script attached to the camera.
 * Read comments there for details about array of sounds.
 * 
 * Note on Monkey's win behavior: to change the behavior of the monkey's 
 * jump at the end, check the NOTE inside win()
 * */

using UnityEngine;
using System.Collections;

public class testAutoMonkey : MonoBehaviour {

	public bool classicMode = false;

	public int lifePoints = 3;

	public Vector3 moveDirection = new Vector3(0,0,1); //starts forward, when hits tree, is up
	public float moveSpeed = 15.0f;
	public float maxMonkeySpeed = 25.0f;
	public float accelerationFactor = 15; // how fast the monkey should accelerate, in relation to Time.deltaTime
	public float slowFactor = 0.5f; // slow factor 0-1, 0 for full speed, 1 for stop
	public float monkeySpeed;
	public bool onTree = false;

	public float checkpointHeight = 250.0f;

	private GameObject mainCam;
	private Color origColor;
	private MonkeyMouse mmouse;
	private enum MonkeyState {sceneStart = 0, initial=1, climbing=2, lose=3, win=4, pause=5};
	MonkeyState monkeyState;

	public bool isJumping = false;
	public float jumpVel = 0.0f;
	public float simGravity = 4.0f;
	public float jumpImpulse = -50.0f;
	public Vector3 jumpDir = new Vector3(0,0,1);

	private Vector3 origPos = Vector3.zero;
	
	public AudioClip [] Clips;
	private AudioSource[] audioSources;

	//Camera stuff
	public bool cameraPermission = false; //Let the monkey move when the camera gives it permission.
	public bool isClimbing = false; //for use with CameraController only
	public bool lost = false; //set to true if you lose.
	public bool won = false;

	//Audio
	private SoundMainScene sounds;
	private bool playedWin;
	private bool playedLose; 

	//Win
	private bool winJump = false; //for end jump
	private Vector3 winPos;

	private CharacterController controller;

	// Use this for initialization
	void Start () {
		if (classicMode) {
			monkeyState = MonkeyState.initial;
			Manager.Instance.score = 0;
		}
		else { //if not classic mode
			monkeyState = MonkeyState.sceneStart;
		}

		monkeySpeed = moveSpeed;
		Manager.Instance.monkeySpeed = monkeySpeed;

		//print (gameObject.name + " has been started.");
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");

		//Audio
		sounds = mainCam.GetComponent<SoundMainScene> ();

		//Set position
		//TODO: have the monkey start relative to the tree spawners spawn distance
		
		origColor = gameObject.renderer.material.color;
		mmouse = this.GetComponent<MonkeyMouse> ();
		Manager.Instance.prevLevel = Application.loadedLevel;
		controller = GetComponent<CharacterController>();
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.tag == "Tree" && !onTree && monkeyState != MonkeyState.win) {
			moveDirection = new Vector3 (0, 1, 0);
			onTree = true;
			monkeyState = MonkeyState.climbing;
			isClimbing = true; //this variable is for the CameraController
			//print ("collision detected!");
		}
		else if( hit.gameObject.tag == "Tree" && isJumping) {
			isJumping = false;
			jumpVel = 0;
			Vector3 resetPos = origPos;
			resetPos.y = this.gameObject.transform.position.y;
			this.gameObject.transform.position = resetPos;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Obstacle") {
			lifePoints--;
			monkeySpeed = moveSpeed * slowFactor;
			if (sounds != null && sounds.playSoundEffects)
				sounds.audioSources[3].Play ();
		}
		if (other.gameObject.tag == "TreeTop") {
			//print ("hit " + other.gameObject.tag);
			monkeyState = MonkeyState.win;
			moveDirection = Vector3.zero;
		}
	}

	void Update() {
		Manager.Instance.monkeyHeight = this.transform.position.y;
		if(monkeyState == MonkeyState.initial || monkeyState == MonkeyState.climbing) {
			if (!isJumping && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown ("up") || mmouse.MoveUp()) && onTree) {
				isJumping = true;
				jumpVel = jumpImpulse;
				mmouse.ResetPos();
				origPos = this.gameObject.transform.position;
			}
		}

		if (classicMode) {
			if (Manager.Instance.monkeySpeed < maxMonkeySpeed) {
				if (Manager.Instance.monkeyHeight >= checkpointHeight) {
					Manager.Instance.monkeySpeed++;
					checkpointHeight += checkpointHeight;
					print(Manager.Instance.monkeySpeed);
				}
			}
		}

		if (monkeySpeed < Manager.Instance.monkeySpeed)
			monkeySpeed += Time.deltaTime * accelerationFactor;
		else
			monkeySpeed = Manager.Instance.monkeySpeed;
	}

	void FixedUpdate () {
		//print (Time.deltaTime);
		//print ("Life: " + lifePoints);
		if (lifePoints <= 0) {
			monkeyState = MonkeyState.lose;
		}

		if (monkeyState == MonkeyState.sceneStart) {
			if (cameraPermission)
				monkeyState = MonkeyState.initial;
		}
		else if (monkeyState == MonkeyState.initial || monkeyState == MonkeyState.climbing) {
			//gameObject.transform.Translate (moveDirection * moveSpeed * Time.deltaTime);
			Vector3 dir = moveDirection*monkeySpeed;

			if (!isJumping && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown ("up") || mmouse.MoveUp()) && onTree) {
				isJumping = true;
				jumpVel = jumpImpulse;

				//Audio
				if (sounds != null && sounds.playSoundEffects)
					sounds.audioSources[4].Play();

				mmouse.ResetPos();
				origPos = this.gameObject.transform.position;
			}

			if (isJumping) {
				jumpVel += simGravity;
				dir += jumpDir * jumpVel;//new Vector3(0,0,jumpVel);
			}

			if ( classicMode )
				Manager.Instance.score += (int)(Time.deltaTime * 100);

			controller.Move(dir * Time.deltaTime);

		} else if (monkeyState == MonkeyState.lose) {
			lose ();
		} else if (monkeyState == MonkeyState.win ) { //TODO might have to change some things here for win jump
			if (isJumping && winPos == new Vector3 ()) { // finish jumping before winning. Do this if winPos still not set
				Vector3 dir = moveDirection*monkeySpeed;
				jumpVel += simGravity;
				dir += jumpDir * jumpVel;//new Vector3(0,0,jumpVel);
				//dir = new Vector3(0,0,jumpVel);

				controller.Move(dir * Time.deltaTime);
			}
			else if(onTree || winPos != new Vector3 ()) {
				won = true; //for camera
				win ();
			}
		}

	}

	void lose() {
		//print("You Lose.");
		lost = true;

		rigidbody.useGravity = true;
		rigidbody.AddForce (2, 0, 2);

		//audio
		if (!sounds.audioSources[2].isPlaying && !playedLose && sounds.playSoundEffects) { //if that sound is not playing, and we have not played it
			sounds.playMusic = false;
			sounds.audioSources[2].Play();
			playedLose = true;
		}
		else if ((playedLose && !sounds.audioSources[2].isPlaying) || !sounds.playSoundEffects) { //if that sound is not playing, and we have played it
			Application.LoadLevel("EndGameScene"); //scene change
		}
	}
	
	void win() {
		//print("You Win!");

		// update high score
		int highScore = PlayerPrefs.GetInt ("HighScore");
		if (Manager.Instance.score > highScore) 
			PlayerPrefs.SetInt ("HighScore", Manager.Instance.score);

		string starPoints = "Level" + (Manager.Instance.levelIndex + 1).ToString() + "Stars";
		if(lifePoints > PlayerPrefs.GetInt(starPoints)) {
			PlayerPrefs.SetInt(starPoints, lifePoints);
		}

		if (!sounds.audioSources[1].isPlaying && !playedWin) { //if that sound is not playing, and we have not played it
			sounds.playMusic = false;
			sounds.audioSources[1].Play();
			playedWin = true;
		}

		else if (playedWin && !sounds.audioSources[1].isPlaying && winJump) { //if that sound is not playing, and we have played it
			// Get HUDScript from Main Camera 
			HUDScript hud = Camera.main.gameObject.GetComponent<HUDScript>();
			//hud.displayWin = true; //TODO this should be uncommented later I think
		}

		//monkey jumps up on top of tree //win jumping
		if (winPos == new Vector3 ()) { //if winPos hasnt been assigned, initialize things for the jump
			winPos = this.gameObject.transform.position; //monkey's current position. need it so we know when to stop falling.
			isJumping = true;	//is it jumping? As of now, yes.
			simGravity = -4.0f; //negative for downwards
			jumpImpulse = 120; //jump impulse.
			jumpVel = jumpImpulse;
		}
		else { //if winPos has already been assigned
			Vector3 dir = moveDirection*1.3f; //forward*speed

			//NOTE: To change behavior of the jump, play with jumpImpulse, dir

			if (isJumping) { //if jumping
				jumpDir = new Vector3(0,1,0); //direction of jump, should be +y
				moveDirection = -this.transform.forward; //Should be towards center of trees

				jumpVel += simGravity;	//decrement the jump velocity
				dir += jumpDir * jumpVel; 

				controller.Move(dir * Time.deltaTime);

				if (this.gameObject.transform.position.y < winPos.y+2.5 && jumpVel <= 0) //if we are lower than when we started, stop jumping
					isJumping = false;
			}
			else { //when finished jumping
				winJump = true;
			}
		} //end win jumping
		

	}

}
