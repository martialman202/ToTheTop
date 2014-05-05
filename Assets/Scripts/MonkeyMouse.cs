using UnityEngine;
using System.Collections;

public class MonkeyMouse : MonoBehaviour {

	private Vector3 prevMousePos;
	private Vector3 currMousePos;
	private float threshhold = 0.0005f;
	private int frameCount;
	private int frameWait = 3;

	public MonkeyMouse () {
		prevMousePos = new Vector3 (0.0f, 0.0f);
		currMousePos = new Vector3 (0.0f, 0.0f);
		frameCount = 0;
		print ("Initial mmouse " + currMousePos.y);
	}

	private Vector3 DeltaMousePos() {
		print ("current " + currMousePos.y);
		print ("prev " + prevMousePos.y);
		return new Vector3(currMousePos.x - prevMousePos.x, currMousePos.y - prevMousePos.y);
	}
	
	public bool MoveUp() {
		Vector3 change = DeltaMousePos();
		print ("Change x " + change.x);
		print ("Change y " + change.y);
		return Input.GetMouseButtonDown(0) && (change.y > threshhold);
	}

	public bool MoveLeft() {
		Vector3 change = DeltaMousePos();
		return Input.GetMouseButtonDown(0) && (-1.0f * change.x > threshhold);
	}

	public bool MoveRight() {
		Vector3 change = DeltaMousePos();
		return Input.GetMouseButtonDown(0) && (change.x > threshhold);
	}

	public void ResetPos() {
		currMousePos = Vector3.zero;
		prevMousePos = currMousePos;
	}

	// Use this for initialization
	/*
	void Start () {
		prevMousePos = Input.mousePosition;
		currMousePos = Input.mousePosition;
	}
	*/

	// Update is called once per frame
	void Update () {
		if(!Input.GetMouseButtonDown(0)) {
			this.ResetPos();
		}
		else{
			prevMousePos = currMousePos;
			currMousePos = Input.mousePosition;
		}
		/*
		if(frameCount == frameWait) {
			prevMousePos = currMousePos;
			currMousePos = Input.mousePosition;
			print ("Update mouse " + currMousePos.y);
			frameCount = 0;
		}
		frameCount++; */
	}
}
