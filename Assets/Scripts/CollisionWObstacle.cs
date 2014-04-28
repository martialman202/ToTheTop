using UnityEngine;
using System.Collections;

public class CollisionWObstacle : MonoBehaviour {

	private Color origColor;
	private int lastHit = 15;

	// Use this for initialization
	void Start () {
		origColor = gameObject.renderer.material.color;
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Obstacle") {
			print("Hit obstacle, should change color");
			lastHit = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		if(lastHit < 15) {
			gameObject.renderer.material.color = Color.red;
			lastHit++;
		}
		else {
			gameObject.renderer.material.color = origColor;
		}
	} 
}
