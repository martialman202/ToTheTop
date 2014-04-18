using UnityEngine;
using System.Collections;

public class testAutoMonkey : MonoBehaviour {

	public int lifePoints = 3;

	public Vector3 moveDirection = new Vector3(0,0,1); //starts forward, when hits tree, is up
	public float moveSpeed = 1.0f; 
	public bool onTree = false;

	private GameObject mainCam; //camera should stop following monkey on lose
	private enum MonkeyState {initial=1, climbing=2, lose=3, win=4};
	MonkeyState monkeyState = MonkeyState.initial;
	//int monkeyState = (int)MonkeyState.initial;

	// Use this for initialization
	void Start () {
		print (gameObject.name + " has been started.");
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Tree") {
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

		}
	}

	// Update is called once per frame
	void Update () {
		if (lifePoints == 0) {
			monkeyState = MonkeyState.lose;
		}

		if (monkeyState == MonkeyState.initial || monkeyState == MonkeyState.climbing) {
				gameObject.transform.Translate (moveDirection * moveSpeed * Time.deltaTime);
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
		Debug.Break ();
	}
	
}
