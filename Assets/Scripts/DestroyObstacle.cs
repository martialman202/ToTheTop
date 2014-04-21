using UnityEngine;
using System.Collections;

public class DestroyObstacle : MonoBehaviour {

	public float offset = 25.0f;
	
	public GameObject mainCam;
	public GameObject thisObstacle;
	
	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		thisObstacle = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(mainCam.transform.position.y >= thisObstacle.transform.position.y + offset) {
			Destroy(thisObstacle);
		}
	}
}
