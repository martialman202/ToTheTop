using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform mainCam;
	public float camSpeed = 10.0f;

	private Vector3 initialPosition = new Vector3 ();

	private GameObject monkey;

	// Use this for initialization
	void Start () {
		monkey = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		mainCam.Translate (Vector3.up * Time.deltaTime * camSpeed, Space.World);
	}
}
