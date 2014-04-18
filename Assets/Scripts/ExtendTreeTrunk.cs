using UnityEngine;
using System.Collections;

public class ExtendTreeTrunk : MonoBehaviour {

	public Transform trunkPrefab;

	public float placementOffset = 5.0f;
	public float checkOffset = 1.0f;

	private GameObject mainCam;
	private GameObject thisTrunk;

	public bool childExists = false;

	private GameObject treeSpawner;
	

	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");
		thisTrunk = this.gameObject;
		this.gameObject.name = "Tree Trunk"; //or else (clone)(clone)...(clone)
	}

	// Update is called once per frame
	void Update () {
		if (!childExists && mainCam.transform.position.y >= thisTrunk.transform.position.y - checkOffset) {
			Vector3 newTrunkPos = new Vector3(thisTrunk.transform.position.x,
			                                  thisTrunk.transform.position.y+placementOffset,
			                                  thisTrunk.transform.position.z);
			Transform newTrunk = (Transform)Instantiate(trunkPrefab,newTrunkPos,Quaternion.identity);
			newTrunk.transform.parent = thisTrunk.transform;
			childExists = true;
		}
	}
}
