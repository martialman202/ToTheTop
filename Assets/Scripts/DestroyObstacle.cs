using UnityEngine;
using System.Collections;

public class DestroyObstacle : MonoBehaviour {

	public float offset = 20.0f;
	
	public GameObject mainCam;

	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (mainCam.transform.position.y >= this.gameObject.transform.position.y + offset)
			Destroy (this.gameObject);
	}
}
