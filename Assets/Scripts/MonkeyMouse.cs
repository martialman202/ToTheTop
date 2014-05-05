using UnityEngine;
using System.Collections;

public class MonkeyMouse : MonoBehaviour {

	private Vector3 prevMousePos;
	private Vector3 currMousePos;
	private float threshhold = 5.0f;

	private Vector3 DeltaMousePos() {
		print ("current " + currMousePos.y);
		print ("prev " + prevMousePos.y);
		return new Vector3(currMousePos.x - prevMousePos.x, currMousePos.y - prevMousePos.y);
	}

	public bool MoveUp() {
		Vector3 change = DeltaMousePos();
		return Input.GetMouseButton(0) && (change.y > threshhold);
	}

	public bool MoveLeft() {
		Vector3 change = DeltaMousePos();
		return Input.GetMouseButton(0) && (-1.0f * change.x > threshhold);
	}

	public bool MoveRight() {
		Vector3 change = DeltaMousePos();
		return Input.GetMouseButton (0) && (change.x > threshhold);
	}

	// Use this for initialization
	void Start () {
		prevMousePos = Input.mousePosition;
		currMousePos = Input.mousePosition;
	}

	// Update is called once per frame
	void Update () {
		prevMousePos = currMousePos;
		currMousePos = Input.mousePosition;
	}
}
