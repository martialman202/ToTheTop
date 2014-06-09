/*
 *  Can be used in both classic and arcade modes.
 */

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public bool classicMode = false;

	//TODO: We need to let the monkeyScript know if the camera is doing shit ot not. some bool or something;

	public float distanceFromMonkey = 5;
	public float secondsLookingAtBananas = 2;
	public float secondsGoingDownTree = 3;
	public float secondsWinLerping = 0.7f;

	//play with these until it looks right. Mostly postion y and z, and rotation x.
	//If you need to change them, change them in the scene editor.
	public Vector3 finalPosition = new Vector3(0.0f,-0.6f,-10f); 
	public Vector3 finalRotation= new Vector3(333.2337f, 0.0f, 0.0f);

	//other objects
	private GameObject monkey;
	private GameObject bananas;
	private testAutoMonkey monkeyScript;

	//for looking at bananas
	private bool foundBananas = false; //for initially finding bananas. Bananas are instantiated in the scene, they don't start in it.
	private bool lookedAtBananas = false;
	private bool set_LB_start = false;
	private float LB_start; //start time for lerp

	//for looking down from bananas (LD)
	private bool set_LD_start = false; 
	private bool lookedDown = false;
	private float LD_start; //start time
	private Vector3 LD_initialRotation = new Vector3(0,0,0);
	private Vector3 LD_finalRotation = new Vector3(90,0,0);

	//for going to the monkey
	private bool wentToMonkey = false;
	private bool set_GM_start_1 = false;
	private float GM_start_1; //start time //for moving camera from treetops
	private Vector3 GM_initialPosition;
	private Vector3 GM_finalPosition;

	//for getting behind the monkey
	private bool behindMonkey = false;
	private bool set_BH_start = false;
	private float BH_start; //start time
	private Vector3 BH_initialPosition;
	private Vector3 BH_finalPosition;

	//for following the monkey
	private bool beforeTree = true;
	private bool set_FM_start = false;
	private float FM_start; //start time

	//for win cam
	private bool set_WC_start = false;
	private float WC_start; //start time
	private Vector3 WC_initialPosition;
	private Vector3 WC_finalPosition;
	private Quaternion WC_initialRotation;
	private Quaternion WC_finalRotation;
	private Transform CamHelper;

	public bool hasFoundBananas() {
		return foundBananas;
	}

	// Use this for initialization
	void Start () {
		monkey = GameObject.FindGameObjectWithTag ("Player");
		monkeyScript = monkey.GetComponent<testAutoMonkey> ();
		CamHelper = this.transform.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
		if (monkeyScript.lost) {
			loseCam ();
		}
		else if (monkeyScript.won) {
			winCam (secondsWinLerping);
		}
		else { //if monkey has not lost
			mainUpdate ();
		}
	}

	void mainUpdate () {
		if (!lookedAtBananas) //if we havent looked at the bananas, go do that
			findBananas ();
		else if (!wentToMonkey) //then if we havent gotten down to the monkey, do that
			gotoMonkey(secondsGoingDownTree);
		else if (!behindMonkey) //then if we havent gotten behind the monkey and let it move, do that
			getBehindMonkey(); //should make monkey move after this
		else if (behindMonkey && !monkeyScript.lost)
			followMonkey();
	}

	void findBananas() {
		if (!classicMode) {
			//The camera starts at the monkey's position but really high up
			if (!foundBananas) { //this is kind of a hack
				bananas = GameObject.FindGameObjectWithTag ("Goal");
				transform.position = (new Vector3 (monkey.transform.position.x,
			                                  bananas.transform.position.y,
			                                  monkey.transform.position.z));
				transform.LookAt (bananas.transform);
				foundBananas = true;
			} else {
				if (!set_LB_start) {
					LB_start = Time.time;
					set_LB_start = true;
				} else {//if LB_start was already set
					if ((Time.time - LB_start) / secondsLookingAtBananas >= 1)
						lookedAtBananas = true;
				}
			}
		} //end if classic mode
	}

	//camera view rotates from banana to monkey
	//This actually gets used in gotoMonkey()
	void lookDown(float time) {
		if (!set_LD_start) {
			LD_start = Time.time;
			set_LD_start = true;
		}
		float t = Time.time - LD_start;
		transform.rotation = Quaternion.Lerp (Quaternion.Euler(LD_initialRotation), 
		                                      Quaternion.Euler(LD_finalRotation), 
		                                      (t / time));            

		if (transform.rotation == Quaternion.Euler(LD_finalRotation)) //if current rotation == final one, we're done
			lookedDown = true;

	}

	//
	void gotoMonkey(float time) { 
		if (!set_GM_start_1) {
			GM_start_1 = Time.time;
			GM_initialPosition = transform.position;
			GM_finalPosition = monkey.transform.position + new Vector3(0,10,0); //above monkey
			set_GM_start_1 = true;
		}
		float t = Time.time - GM_start_1;
		transform.position = Vector3.Lerp (GM_initialPosition, GM_finalPosition, t / time);//2s

		if ((t / time) >= 0.5 && !lookedDown) {
			lookDown(time/2);
		}

		if (transform.position == GM_finalPosition)
			wentToMonkey = true;
			
	}

	/*
	 * TODO: Change the following values until the camera looks good when it follows the monkey:
	 * 		new vector being added in BH_finalPosition
	 * 		new vector being added to monkey.transform.position during the LookAt
	 */
	void getBehindMonkey(){
		if (!set_BH_start) {
			BH_start = Time.time;
			BH_initialPosition = transform.position;
			BH_finalPosition = monkey.transform.position + new Vector3(0,-0.2888703f, -distanceFromMonkey); //above monkey
			set_BH_start = true;
		}
		float t = Time.time - BH_start;
		transform.position = Vector3.Lerp (BH_initialPosition, BH_finalPosition, t);
		transform.LookAt (monkey.transform);

		if (transform.position == BH_finalPosition) {
			behindMonkey = true;
			monkeyScript.cameraPermission = true; //allow the monkey to move
		}
	}

		/*
		 * Follow monkey to tree, 
		 * 	then freeze in place once monkey makes contact.
		 */
	void followMonkey(){
		if (!set_FM_start) {
			FM_start = Time.time;
			set_FM_start = true;
		}
		Vector3 pos = this.transform.position;
		Quaternion rot = this.transform.rotation;

		/*if (!this.transform.parent) {
			this.transform.parent = monkey.transform;
		}*/

		if (beforeTree) {
		this.transform.parent = monkey.transform;
		this.transform.position = Vector3.Lerp(pos, monkey.transform.position + finalPosition, (Time.time - FM_start) /1);
		this.transform.rotation = Quaternion.Lerp (rot, Quaternion.Euler(finalRotation), (Time.time - FM_start) /1);
			if (monkeyScript.isClimbing) {
				this.transform.parent = monkey.transform;
				beforeTree = false;
			}
		}
		
	} //end void followMonkey()

	//for when monkey loses
	void loseCam() {
		//print ("cam says monkey lost");
		this.transform.parent = null;
	}

	//for when monkey wins
	void winCam(float time) {
		if (!set_WC_start) {
			WC_start = Time.time;
			WC_initialPosition = transform.position;
			WC_initialRotation = transform.rotation;
			WC_finalPosition = transform.position + new Vector3(0, distanceFromMonkey, 0); //above monkey
			CamHelper.LookAt(monkey.transform);
			WC_finalRotation = CamHelper.rotation;

			set_WC_start = true;
		}

		if (transform.position != WC_finalPosition) {
			transform.position = Vector3.Lerp (WC_initialPosition, WC_finalPosition, (Time.time - WC_start) / time);
			//transform.LookAt(monkey.transform);
			transform.rotation = Quaternion.Lerp (WC_initialRotation, WC_finalRotation, (Time.time - WC_start) / time);
		}
	}
}
