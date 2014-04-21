using UnityEngine;
using System.Collections;

public class DestroyTreeTrunk : MonoBehaviour {

	public float offset = 25.0f;

	public GameObject mainCam;
	public GameObject thisTrunk;

	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		thisTrunk = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Transform parent = thisTrunk.transform.parent;
		if(mainCam.transform.position.y >= thisTrunk.transform.position.y+offset && parent.tag == "TreeSpawner") {
			Transform child = thisTrunk.transform.GetChild(0);
			child.transform.parent = parent;
			Destroy(thisTrunk);
		}
	}
}
