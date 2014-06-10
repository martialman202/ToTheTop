using UnityEngine;
using System.Collections;

public class CollisionWObstacle : MonoBehaviour {

	private Color darkRed;

	private Color origDarkBrownColor;
	private Color origLightBrownColor;

	private int lastHit = 15;

	private Renderer monkeyRenderer;

	// Use this for initialization
	void Start () {
		//origColor = gameObject.renderer.material.color;
		darkRed = new Color(0.45f, 0.0f, 0.0f);
		monkeyRenderer = this.transform.GetChild(2).renderer;
		origDarkBrownColor = monkeyRenderer.materials[0].color;
		origLightBrownColor = monkeyRenderer.materials[1].color;
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Obstacle") {
			//print("Hit obstacle, should change color");
			lastHit = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		if(lastHit < 15) {
			//gameObject.renderer.material.color = Color.red;
			monkeyRenderer.materials[0].color = darkRed;
			monkeyRenderer.materials[1].color = darkRed;
			lastHit++;
		}
		else {
			//gameObject.renderer.material.color = origColor;
			monkeyRenderer.materials[0].color = origDarkBrownColor;
			monkeyRenderer.materials[1].color = origLightBrownColor;
		}
	} 
}
