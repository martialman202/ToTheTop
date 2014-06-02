using UnityEngine;
using System.Collections;

public class SkyboxManager : MonoBehaviour {

	public Material [] skyboxes = new Material[2];

	// Use this for initialization
	void Start () {
		Skybox sky = (Skybox)gameObject.GetComponent("Skybox");
		if((Manager.Instance.levelIndex + 1) > 3)
			sky.material = skyboxes[1];
	}
}
