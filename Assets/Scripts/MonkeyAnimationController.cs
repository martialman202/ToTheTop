using UnityEngine;
using System.Collections;

public class MonkeyAnimationController : MonoBehaviour {

	private Animation animation;
	private enum AnimIdx { idle = 0, run = 1, climb = 2, jump_up = 3, land_up = 4, jump_left = 5, land_left = 6, jump_right = 7, land_right = 8 };
	private AnimIdx animIdx;

	private GameObject monkey;
	private testAutoMonkey monkeyScript;

	private bool animDone;

	void setupSpeeds() {
		animation["Run"].speed = 1.0f;
		animation["Climb"].speed = 2.0f;
		animation["Jump_Up"].speed = 2.0f;
		animation["Land_Up"].speed = 15.0f;
		animation["Jump_Left"].speed = 2.0f;
		animation["Land_Left"].speed = 10.0f;
		animation["Jump_Right"].speed = 2.0f;
		animation["Land_Right"].speed = 10.0f;
	}

	// Use this for initialization
	void Start () {
		monkey = this.gameObject;
		monkeyScript = monkey.GetComponent<testAutoMonkey> ();
		animation = this.GetComponent<Animation> ();
		animation.Play ("Idle");
		animIdx = AnimIdx.idle;
		animDone = true;

		setupSpeeds();
	}
	
	// Update is called once per frame
	void Update () {
		if( monkeyScript.monkeyState == testAutoMonkey.MonkeyState.initial && animIdx != AnimIdx.run ) {
			animation.CrossFade("Run", 0.2f);
			animIdx = AnimIdx.run;
		}
		else if( monkeyScript.jumpState == testAutoMonkey.JumpState.up && animIdx != AnimIdx.jump_up ) {
			animation.CrossFade("Jump_Up", 0.1f);
			animIdx = AnimIdx.jump_up;
		}
		else if( monkeyScript.jumpState == testAutoMonkey.JumpState.none && animIdx == AnimIdx.jump_up ) {
			animation.CrossFade("Land_Up", 0.1f);
			animIdx = AnimIdx.land_up;
			animDone = false;
		}
		else if( monkeyScript.jumpState == testAutoMonkey.JumpState.left && animIdx != AnimIdx.jump_left ) {
			animation.CrossFade("Jump_Left", 0.1f);
			animIdx = AnimIdx.jump_left;
		}
		else if( monkeyScript.jumpState == testAutoMonkey.JumpState.none && animIdx == AnimIdx.jump_left ) {
			animation.CrossFade("Land_Left", 0.1f);
			animIdx = AnimIdx.land_left;
			animDone = false;
		}
		else if( monkeyScript.jumpState == testAutoMonkey.JumpState.right && animIdx != AnimIdx.jump_right ) {
			animation.CrossFade("Jump_Right", 0.1f);
			animIdx = AnimIdx.jump_right;
		}
		else if( monkeyScript.jumpState == testAutoMonkey.JumpState.none && animIdx == AnimIdx.jump_right ) {
			animation.CrossFade("Land_Right", 0.1f);
			animIdx = AnimIdx.land_right;
			animDone = false;
		}
		else if( monkeyScript.jumpState == testAutoMonkey.JumpState.none && monkeyScript.monkeyState == testAutoMonkey.MonkeyState.climbing && animIdx != AnimIdx.climb && animDone ) {
			animation.CrossFade("Climb", 0.2f);
			animIdx = AnimIdx.climb;
		}

		// if a landing animation is playing, wait for it to end
		if( animIdx == AnimIdx.land_up || animIdx == AnimIdx.land_left || animIdx == AnimIdx.land_right ) {
			animDone = !animation.isPlaying;
		}

	}
}
