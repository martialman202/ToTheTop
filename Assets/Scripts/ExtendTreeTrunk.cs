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

	private float[] rotations = new float[3];

	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");
		thisTrunk = this.gameObject;
		if( this.transform.parent.gameObject.GetComponent<ExtendTreeTrunk>() )
			treeID = this.transform.parent.gameObject.GetComponent<ExtendTreeTrunk> ().treeID;
		this.gameObject.name = "Tree Trunk"; //or else (clone)(clone)...(clone)
		rotations [0] = 240.0f;
		rotations [1] = 120.0f;
		rotations [2] = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		if (!childExists && mainCam.transform.position.y >= thisTrunk.transform.position.y - checkOffset) {
			Vector3 origin = Vector3.zero;
			Vector3 newTrunkPos = new Vector3(thisTrunk.transform.position.x,
			                                  thisTrunk.transform.position.y+placementOffset,
			                                  thisTrunk.transform.position.z);
			Transform newTrunk;
			treeType = this.transform.root.gameObject.GetComponent<TreeTrunkSpawner>().tree[treeID];
			float rotate = rotations[treeID] + this.transform.root.transform.eulerAngles.y;
			switch (treeType) {
				case TreeType.CARTOON:
				newTrunk = (Transform)Instantiate(trunkPrefab,newTrunkPos,Quaternion.AngleAxis(rotate,new Vector3(0,1,0)));
					break;
				case TreeType.BEEHIVE:
				newTrunk = (Transform)Instantiate(beeHiveTrunk,newTrunkPos,Quaternion.AngleAxis(rotate,new Vector3(0,1,0)));
					break;
				case TreeType.SNAKE:
				newTrunk = (Transform)Instantiate(snakeTrunk,newTrunkPos,Quaternion.AngleAxis(rotate,new Vector3(0,1,0)));
					break;
				case TreeType.UNCLIMBABLE:
				newTrunk = (Transform)Instantiate(bushTrunk,newTrunkPos,Quaternion.AngleAxis(rotate,new Vector3(0,1,0)));
					break;
				default:
				newTrunk = (Transform)Instantiate(trunkPrefab,newTrunkPos,Quaternion.AngleAxis(rotate,new Vector3(0,1,0)));
					break;
			}
				
			newTrunk.transform.parent = thisTrunk.transform;
			
			this.transform.root.gameObject.GetComponent<TreeTrunkSpawner>().tree[treeID] = TreeType.CARTOON;

			childExists = true;
		}
	}
}
