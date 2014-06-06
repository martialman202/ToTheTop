using UnityEngine;
using System.Collections;

public class Coconut : MonoBehaviour {
	public float speed = 20.0f;
	private float warningDistance = (Screen.height / Camera.main.orthographicSize)/3;
	private float cautionDistance = (Screen.height / Camera.main.orthographicSize);

	public GUISkin menuSkin;

	public Texture2D coconutCaution;
	public Texture2D coconutWarning;
	private Texture2D current;

	private enum CoconutState {Initial,Caution, Warning};
	CoconutState state;

	private int counter = 0;
	private int blinkSpeed = 25;

	private bool coroutineStarted = false;

	void OnGUI()
	{
		GUI.skin = menuSkin;
		
		//Coconut warning
		if (state == CoconutState.Caution)
			GUI.Label (new Rect (0.4f * Screen.width, 0.50f * Screen.height, 0.2f*Screen.width, 0.2f*Screen.width), coconutCaution);

		else if (state == CoconutState.Warning)
			GUI.Label (new Rect (0.4f * Screen.width, 0.50f * Screen.height, 0.2f*Screen.width, 0.2f*Screen.width), current);
	}

	// Use this for initialization
	void Start () {
		state = CoconutState.Initial;
		current = coconutWarning;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray coconutRay = new Ray (transform.position, Vector3.down);

		if (Physics.Raycast(coconutRay, out hit, warningDistance)) {
			if (hit.collider.tag == "Player" || hit.collider.name == "RayDetector")
				state = CoconutState.Warning;
		}
		else if (Physics.Raycast(coconutRay, out hit, cautionDistance)) {
			if (hit.collider.tag == "Player"|| hit.collider.name == "RayDetector")
				state = CoconutState.Caution;
		}
		else
			state = CoconutState.Initial;

		if (state == CoconutState.Warning && !coroutineStarted)
			StartCoroutine (Flicker ());
	}

	void FixedUpdate () {
		this.transform.Translate (Vector3.down * speed * Time.deltaTime);
	}

	// Coroutine to flicker coconut is warning
	IEnumerator Flicker() {
		coroutineStarted = true;
		for (int i = 0; i < 10; i++) {
			switch (i % 2) {
			case 0:
				current = coconutWarning;
				break;
			case 1:
				current = coconutCaution;
				break;
			default:
				break;
			}
			yield return new WaitForSeconds (.1f);
		}
		coroutineStarted = false;
	}
}
