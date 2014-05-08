using UnityEngine;
using System.Collections;

public class MonkeyMouse : MonoBehaviour {

	private Vector3 prevMousePos;
	private float threshhold = 0.001f;
	private bool dragging;
	private int frameCount;
	private int frameWait = 3;

	public MonkeyMouse () {
		prevMousePos = Vector3.zero;
		dragging = false;
		frameCount = 0;
	}

	private Vector3 DeltaMousePos() {
		return new Vector3(Input.mousePosition.x - prevMousePos.x, Input.mousePosition.y - prevMousePos.y);
	}
	
	public bool MoveUp() {
		Vector3 change = DeltaMousePos();
		return dragging && (change.sqrMagnitude != 0) && (change.y > threshhold) && (change.y > change.x);
	}

	public bool MoveRight() {
		Vector3 change = DeltaMousePos();
		return dragging && (change.sqrMagnitude != 0) && (-1.0f * change.x > threshhold) && (-1.0f * change.x > change.y);
	}

	public bool MoveLeft() {
		Vector3 change = DeltaMousePos();
		return dragging && (change.sqrMagnitude != 0) && (change.x > threshhold) && (change.x > change.y);
	}

	public void ResetPos() {
		prevMousePos = Vector3.zero;
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
		if(Input.GetMouseButton(0)) {
			if(!dragging) {
				dragging = true;
				prevMousePos = Input.mousePosition;
			}
		}
		else
			dragging = false;
		/*
		if(dragging)
			print ("Dragging on");
		else 
			print ("Not dragging"); */
	}
}
