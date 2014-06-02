using UnityEngine;
using System.Collections;

public class SkyboxManager : MonoBehaviour {

	public Skybox [] skyboxes = new Skybox[2];

	// Use this for initialization
	void Start () {
		Skybox = skyboxes[0];
		if(Manager.Instance.levelIndex > 3)
			Skybox = skyboxes[1];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
