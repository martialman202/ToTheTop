using UnityEngine;
using System.Collections;

public class testMoveMonkey : MonoBehaviour {

	//Constant variables
	private int frameWait = 10;
	private Vector3 moveRight = new Vector3(20.0f, 0.0f, 0.0f);
	private Vector3 moveLeft = new Vector3(10.0f, 0.0f, 17.32f);

	//Delay variables
	private int rightLastPressed;
	private int leftLastPressed;

	// Use this for initialization
	void Start () {
		rightLastPressed = frameWait;
		leftLastPressed = frameWait;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("right") && (rightLastPressed >= frameWait)) {
			gameObject.transform.Translate(moveRight);
			rightLastPressed = 0;
			leftLastPressed++;
			gameObject.transform.Rotate(Vector3.up * -120.0f);
		}
		else if(Input.GetKey("left") && (leftLastPressed >= frameWait)) {
			gameObject.transform.Translate(moveLeft);
			gameObject.transform.Rotate(Vector3.up * 120.0f);
			rightLastPressed++;
			leftLastPressed = 0;
		}
		else
		{
			rightLastPressed++;
			leftLastPressed++;
		}
	}
}
