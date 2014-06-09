using UnityEngine;
using System.Collections;

public class Coconut : MonoBehaviour {
	public float speed = 20.0f;
	private float warningDistance;
	private float alertDistance;

	public GUISkin menuSkin;

	public Texture2D coconutAlert;
	public Texture2D coconutWarning;
	private Texture2D current;

	private enum CoconutState {Initial, Warning, Alert};
	CoconutState state;

	private int counter = 0;
	private int blinkSpeed = 25;

	private bool coroutineStarted = false;

	private GameObject treeTop; // destroy if end of game is found
	private GameObject trunk;

	void OnGUI()
	{
		GUI.skin = menuSkin;
		
		// Coconut warning
		if (state == CoconutState.Warning)
			GUI.Label (new Rect (0.4f * Screen.width, 0.50f * Screen.height, 0.2f*Screen.width, 0.2f*Screen.width), coconutWarning);

		else if (state == CoconutState.Alert)
			GUI.Label (new Rect (0.4f * Screen.width, 0.50f * Screen.height, 0.2f*Screen.width, 0.2f*Screen.width), current);
	}

	// Use this for initialization
	void Start () {
		state = CoconutState.Initial;
		current = coconutWarning;

		treeTop = GameObject.FindWithTag ("TreeTop");
		if (treeTop != null) // if treeTop found
			Destroy (this.gameObject);

		trunk = GameObject.FindWithTag ("Tree");
		if (trunk != null) {
			alertDistance = trunk.renderer.bounds.size.y * 15;
		} else {
			alertDistance = (Screen.height / Camera.main.orthographicSize);
		}
		warningDistance = alertDistance * 10;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray coconutRay = new Ray (transform.position, Vector3.down);

<<<<<<< HEAD
		if (Physics.Raycast(coconutRay, out hit, warningDistance)) {
			if (hit.collider.tag == "Player" || hit.collider.name == "Coconut_Collision_Plane")
				state = CoconutState.Warning;
		}
		else if (Physics.Raycast(coconutRay, out hit, cautionDistance)) {
			if (hit.collider.tag == "Player"|| hit.collider.name == "Coconut_Collision_Plane")
				state = CoconutState.Caution;
=======
		if (Physics.Raycast(coconutRay, out hit, alertDistance)) {
			if (hit.collider.tag == "Player" || hit.collider.name == "RayDetector")
				state = CoconutState.Alert;
		}
		else if (Physics.Raycast(coconutRay, out hit, warningDistance)) {
			if (hit.collider.tag == "Player"|| hit.collider.name == "RayDetector")
				state = CoconutState.Warning;
>>>>>>> 63e143e28d98f4c683712efecc61733b2cc811d0
		}
		else
			state = CoconutState.Initial;

		if (state == CoconutState.Alert && !coroutineStarted)
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
				current = coconutAlert;
				break;
			case 1:
				current = coconutWarning;
				break;
			default:
				break;
			}
			yield return new WaitForSeconds (.1f);
		}
		coroutineStarted = false;
	}
}
