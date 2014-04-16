using UnityEngine;
using System.Collections;

public class ExtendTreeTrunk : MonoBehaviour {

	public Transform trunkPrefab;

	public float placementOffset = 5.0f;
	public float checkOffset = 1.0f;

	public GameObject mainCam;
	public GameObject thisTrunk;

	public bool childExists = false;

	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		thisTrunk = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!childExists && mainCam.transform.position.y >= thisTrunk.transform.position.y - checkOffset) {
			Transform newTrunk = (Transform)Instantiate(trunkPrefab,new Vector3(thisTrunk.transform.position.x,thisTrunk.transform.position.y+placementOffset,thisTrunk.transform.position.z),Quaternion.identity);
			newTrunk.transform.parent = thisTrunk.transform;
			childExists = true;
		}
	}
}
