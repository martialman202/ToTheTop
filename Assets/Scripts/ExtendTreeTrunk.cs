using UnityEngine;
using System.Collections;

public class ExtendTreeTrunk : MonoBehaviour {

	public Transform trunkPrefab;
	public Transform beeHiveTrunk;
	public Transform snakeTrunk;
	public Transform bushTrunk;


	public float placementOffset = 2.0f;
	public float checkOffset = 1.0f;

	private GameObject mainCam;
	private GameObject thisTrunk;

	public bool childExists = false;
	public int treeID;

	public enum TreeType {CARTOON,BEEHIVE,SNAKE,UNCLIMBABLE}; // which trunk to spawn
	public TreeType treeType = TreeType.CARTOON;

	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");
		thisTrunk = this.gameObject;
		treeID = this.transform.parent.gameObject.GetComponent<ExtendTreeTrunk> ().treeID;
		this.gameObject.name = "Tree Trunk"; //or else (clone)(clone)...(clone)
	}

	// Update is called once per frame
	void Update () {
		if (!childExists && mainCam.transform.position.y >= thisTrunk.transform.position.y - checkOffset) {
			Vector3 newTrunkPos = new Vector3(thisTrunk.transform.position.x,
			                                  thisTrunk.transform.position.y+placementOffset,
			                                  thisTrunk.transform.position.z);
			Transform newTrunk;
			treeType = this.transform.root.gameObject.GetComponent<TreeTrunkSpawner>().tree[treeID];
			switch (treeType) {
				case TreeType.CARTOON:
					newTrunk = (Transform)Instantiate(trunkPrefab,newTrunkPos,Quaternion.identity);
					break;
				case TreeType.UNCLIMBABLE:
					newTrunk = (Transform)Instantiate(bushTrunk,newTrunkPos,Quaternion.identity);
					break;
				default:
					newTrunk = (Transform)Instantiate(trunkPrefab,newTrunkPos,Quaternion.identity);
					break;
			}
				
			newTrunk.transform.parent = thisTrunk.transform;
			
			this.transform.root.gameObject.GetComponent<TreeTrunkSpawner>().tree[treeID] = TreeType.CARTOON;

			childExists = true;
		}
	}
}
