using UnityEngine;
using System.Collections;

public class TreeTrunkSpawner : MonoBehaviour {

	public Transform treeTrunk;

	public float placementOffset = 2.0f;
	public int numTrunks = 5;

	public float distance = 5.0f;
	public float[] pos;// = new float[3];//{ 90.0f, 225.0f, 315.0f };
	// Use this for initialization

	// unclimbableObstacle must be last element in obstacle array
	private Transform [] obstacles = new Transform[3];
	public Transform beeHiveObstacle;
	public Transform snakeObstacle;
	public Transform unclimbableObstacle;

	public float obstacleSpawnMin = 0.5f;
	public float obstacleSpawnMax = 1.25f;
	public float obstaclePlacementOffset = 40f;

	public Transform treeTop;
	public float winHeight = 1000.0f;
	private bool spawnedTreeTop = false;
	public float treeTopHeightOffset = 72.25f;

	public Transform bananas;
	public float bananasHeightOffset = 5.0f;

	public GameObject spawner;
	private GameObject mainCam;

	private Transform tree1, tree2, tree3;
	public ExtendTreeTrunk.TreeType [] tree = new ExtendTreeTrunk.TreeType [3];
	void Start () {
		obstacles [0] = beeHiveObstacle;
		obstacles [1] = snakeObstacle;
		obstacles [2] = unclimbableObstacle;

		tree [0] = ExtendTreeTrunk.TreeType.CARTOON;
		tree [1] = ExtendTreeTrunk.TreeType.CARTOON;
		tree [2] = ExtendTreeTrunk.TreeType.CARTOON;

		pos = new float[3];
		pos[0] = 30.0f;
		pos[1] = 150.0f;
		pos[2] = 270.0f;

		spawner = this.gameObject;
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");

		tree1 = (Transform)Instantiate(treeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*pos[0]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[0]/180.0f)), Quaternion.identity);
		tree2 = (Transform)Instantiate(treeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*pos[1]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[1]/180.0f)), Quaternion.identity);
		tree3 = (Transform)Instantiate(treeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*pos[2]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[2]/180.0f)), Quaternion.identity);

		tree1.gameObject.GetComponent<ExtendTreeTrunk> ().treeID = 0;
		tree2.gameObject.GetComponent<ExtendTreeTrunk> ().treeID = 1;
		tree3.gameObject.GetComponent<ExtendTreeTrunk> ().treeID = 2;

		tree1.transform.parent = spawner.transform;
		tree2.transform.parent = spawner.transform;
		tree3.transform.parent = spawner.transform;

		Invoke ("SpawnObstacle", 2);
	}

	void SpawnObstacle() {
		bool [] obstacleOnTree = new bool [3];
		obstacleOnTree [0] = false;
		obstacleOnTree [1] = false;
		obstacleOnTree [2] = false;
		if (!spawnedTreeTop) {
			bool spawnUnclimbable = false;
			int numObstacles = Random.Range (1, 3); // number of obstacles to spawn, either 1 or 2
			int whichObstacle = Random.Range (0, obstacles.Length);
			if (obstacles [whichObstacle] == unclimbableObstacle)
				spawnUnclimbable = true;
			
			if (spawnUnclimbable) {
				//print ("spawning unclimbable!");
				tree[0] = ExtendTreeTrunk.TreeType.UNCLIMBABLE;
				tree[1] = ExtendTreeTrunk.TreeType.UNCLIMBABLE;
				tree[2] = ExtendTreeTrunk.TreeType.UNCLIMBABLE;
				spawnUnclimbable = false;
			} else {
				for (int i = 0; i < numObstacles; i++) {
					whichObstacle = Random.Range (0, obstacles.Length - 1); // choose which obstacle to spawn
					int whichTree = Random.Range (0, 3); // choose which tree to spawn on
					
					if (!obstacleOnTree [whichTree]) { // if there isn't an obstacle on this tree
						obstacleOnTree [whichTree] = true; // mark that we're spawning an obstacle on this tree

					switch (whichObstacle) {
							case 0:
								tree[whichTree] = ExtendTreeTrunk.TreeType.BEEHIVE;
								break;
							case 1:
								tree[whichTree] = ExtendTreeTrunk.TreeType.SNAKE;
								break;
						}
					}
				}
			}
			Invoke ("SpawnObstacle", Random.Range (obstacleSpawnMin, obstacleSpawnMax));
		}
	}

	// Update is called once per frame
	void Update () {
		GameObject sc = GameObject.Find ("SpinController");
		SpinController spin_controller = sc.GetComponent<SpinController> ();
		if (!spin_controller.moveLeft && !spin_controller.moveRight && mainCam.transform.position.y >= winHeight && !spawnedTreeTop) {
			spawnedTreeTop = true;
			Vector3 h = this.transform.position + (Vector3.up*(mainCam.transform.position.y+treeTopHeightOffset));
			Transform tt1 = (Transform)Instantiate(treeTop, h + new Vector3(distance * Mathf.Cos(Mathf.PI*pos[0]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[0]/180.0f)),Quaternion.identity);
			Transform tt2 = (Transform)Instantiate(treeTop, h + new Vector3(distance * Mathf.Cos(Mathf.PI*pos[1]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[1]/180.0f)),Quaternion.identity);
			Transform tt3 = (Transform)Instantiate(treeTop, h + new Vector3(distance * Mathf.Cos(Mathf.PI*pos[2]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[2]/180.0f)),Quaternion.identity);

			tt1.transform.parent = spawner.transform;
			tt2.transform.parent = spawner.transform;
			tt3.transform.parent = spawner.transform;

			Instantiate(bananas, h + new Vector3(0,bananasHeightOffset,0), Quaternion.identity);
		}
	}
}
