﻿/*
 * Note on the audio sources:
 * SoundMainScene is a script attached to the camera.
 * Read comments there for details about array of sounds.
 * */

using UnityEngine;
using System.Collections;

public class testAutoMonkey : MonoBehaviour {

	public bool classicMode = false;

	public int lifePoints = 3;

	public Vector3 moveDirection = new Vector3(0,0,1); //starts forward, when hits tree, is up
	public float moveSpeed = 20.0f; 
	public bool onTree = false;

	private GameObject mainCam;
	private Color origColor;
	private MonkeyMouse mmouse;
	private enum MonkeyState {sceneStart = 0, initial=1, climbing=2, lose=3, win=4, pause=5};
	MonkeyState monkeyState;

	public bool isJumping = false;
	public float jumpVel = 0.0f;
	public float simGravity = 4.0f;
	public float jumpImpulse = -45.0f;
	public Vector3 jumpDir = new Vector3(0,0,1);

	private Vector3 origPos = Vector3.zero;
	
	public AudioClip [] Clips;
	private AudioSource[] audioSources;

	//Camera stuff
	public bool cameraPermission = false; //Let the monkey move when the camera gives it permission.
	public bool isClimbing = false; //for use with CameraController only

	//Audio
	private SoundMainScene sounds;
	private bool playedWin;
	private bool playedLose; 

	private CharacterController controller;

	// Use this for initialization
	void Start () {
		if (classicMode) {
			monkeyState = MonkeyState.initial;
		}
		else { //if not classic mode
			monkeyState = MonkeyState.sceneStart;
		}

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

				if (sounds != null && sounds.playSoundEffects)
					sounds.audioSources[4].Play();

				mmouse.ResetPos();
				origPos = this.gameObject.transform.position;
			}
		}
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
			Vector3 dir = moveDirection*moveSpeed;

			if (isJumping) {
				jumpVel += simGravity;
				dir += jumpDir * jumpVel;//new Vector3(0,0,jumpVel);
			}

			controller.Move(dir * Time.deltaTime);

		} else if (monkeyState == MonkeyState.lose) {
			lose ();
		} else if (monkeyState == MonkeyState.win) {
			Vector3 dir = moveDirection*moveSpeed;
			if (isJumping) { // finish jumping before winning
				jumpVel += simGravity;
				dir += jumpDir * jumpVel;//new Vector3(0,0,jumpVel);
				//dir = new Vector3(0,0,jumpVel);

				controller.Move(dir * Time.deltaTime);
			}
			else if(onTree)
				win ();
		}
	}

	void lose() {
		print("You Lose.");

		if (!sounds.audioSources[2].isPlaying && !playedLose) { //if that sound is not playing, and we have not played it
			sounds.playMusic = false;
			sounds.audioSources[2].Play();
			playedLose = true;
		}
		else if (playedLose && !sounds.audioSources[2].isPlaying) { //if that sound is not playing, and we have played it
			Application.LoadLevel("EndGameScene");
		}
	}
	
	void win() {

		print("You Win");
		if (!sounds.audioSources[1].isPlaying && !playedLose) { //if that sound is not playing, and we have not played it
			sounds.playMusic = false;
			sounds.audioSources[1].Play();
			playedLose = true;
		}
		else if (playedLose && !sounds.audioSources[1].isPlaying) { //if that sound is not playing, and we have played it
			// Get HUDScript from Main Camera 
			HUDScript hud = Camera.main.gameObject.GetComponent<HUDScript>();
			hud.displayWin = true;
		}
	}

}
