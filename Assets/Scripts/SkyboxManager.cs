using UnityEngine;
using System.Collections;

public class SkyboxManager : MonoBehaviour {

	public Material [] skyboxes = new Material[3];

	// Use this for initialization
	void Start () {
		Skybox sky = (Skybox)gameObject.GetComponent("Skybox");
		if((Manager.Instance.levelIndex > 2) && (Manager.Instance.levelIndex <= 5))
			sky.material = skyboxes[1];
		else if(Manager.Instance.levelIndex > 5)
			sky.material = skyboxes[2];
	}
}
