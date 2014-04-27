using UnityEngine;
using System.Collections;

public class TreeTrunkSpawner : MonoBehaviour {

	public Transform treeTrunk;

	public float placementOffset = 2.0f;
	public int numTrunks = 5;

	public float distance = 5.0f;
	public float[] pos;// = new float[3];//{ 90.0f, 225.0f, 315.0f };
	// Use this for initialization

	public int currentRotation = 0;
	public float[] rotations;
	public float moveSpeed = 150.0f;

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

	private bool moveRight = false;
	private bool moveLeft = false;

	public GameObject spawner;

	private GameObject mainCam;
	private bool onTree = false;

	void Start () {
		obstacles [0] = beeHiveObstacle;
		obstacles [1] = snakeObstacle;
		obstacles [2] = unclimbableObstacle;

		pos = new float[3];
		pos[0] = 30.0f;
		pos[1] = 150.0f;
		pos[2] = 270.0f;

		rotations = new float[3];
		rotations[0] = 0.0f;
		rotations[1] = 120.0f;
		rotations[2] = 240.0f;

		spawner = this.gameObject;
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");

		Transform tree1 = (Transform)Instantiate(treeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*pos[0]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[0]/180.0f)), Quaternion.identity);
		Transform tree2 = (Transform)Instantiate(treeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*pos[1]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[1]/180.0f)), Quaternion.identity);
		Transform tree3 = (Transform)Instantiate(treeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*pos[2]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[2]/180.0f)), Quaternion.identity);

		tree1.gameObject.GetComponent<ExtendTreeTrunk> ().treeID = 0;
		tree2.gameObject.GetComponent<ExtendTreeTrunk> ().treeID = 1;
		tree3.gameObject.GetComponent<ExtendTreeTrunk> ().treeID = 2;

		tree1.transform.parent = spawner.transform;
		tree2.transform.parent = spawner.transform;
		tree3.transform.parent = spawner.transform;

		SpawnObstacle();
	}

	void SpawnObstacle() {
		if (!spawnedTreeTop) {
			float beeHiveDistance = distance + 1.0f;

			bool spawnUnclimbable = false;

			bool [] obstacleOnTree = new bool[3];
			obstacleOnTree [0] = false;
			obstacleOnTree [1] = false;
			obstacleOnTree [2] = false;

			float [] rotateObstacle = new float[3]; // rotate obstacle depending on whichTree
			rotateObstacle [0] = 240.0f;
			rotateObstacle [1] = 120.0f;
			rotateObstacle [2] = 0.0f;

			int numObstacles = Random.Range (1, 3); // number of obstacles to spawn, either 1 or 2
			int whichObstacle = Random.Range (0, obstacles.Length);
			if (obstacles [whichObstacle] == unclimbableObstacle)
				spawnUnclimbable = true;

			if (!moveLeft && !moveRight && onTree) {
				if (spawnUnclimbable) {
					for (int i = 0; i < 3; i++) {
						Transform obstacle;

						obstacle = (Transform)Instantiate (obstacles [whichObstacle], new Vector3 ((distance) * Mathf.Cos (Mathf.PI * pos [i] / 180.0f), mainCam.transform.position.y + obstaclePlacementOffset, (distance) * Mathf.Sin (Mathf.PI * pos [i] / 180.0f)), Quaternion.AngleAxis (rotateObstacle [i] + 180.0f, new Vector3 (0, 1, 0)));
						obstacle.transform.parent = spawner.transform;
					}
				} else {
					for (int i = 0; i < numObstacles; i++) {
						whichObstacle = Random.Range (0, obstacles.Length - 1); // choose which obstacle to spawn
						int whichTree = Random.Range (0, 3); // choose which tree to spawn on

						if (!obstacleOnTree [whichTree]) { // if there isn't an obstacle on this tree
							obstacleOnTree [whichTree] = true; // mark that we're spawning an obstacle on this tree

							// spawn obstacle
							Transform obstacle;
							if( whichObstacle == 1 ) {
								obstacle = (Transform)Instantiate (obstacles [whichObstacle], new Vector3 ((distance) * Mathf.Cos (Mathf.PI * pos [i] / 180.0f), mainCam.transform.position.y + obstaclePlacementOffset, (distance) * Mathf.Sin (Mathf.PI * pos [i] / 180.0f)), Quaternion.AngleAxis (rotateObstacle [i] + 180.0f, new Vector3 (0, 1, 0)));
							}
							else {
								obstacle = (Transform)Instantiate (obstacles [whichObstacle], new Vector3 ((beeHiveDistance) * Mathf.Cos (Mathf.PI * pos [i] / 180.0f), mainCam.transform.position.y + obstaclePlacementOffset, (beeHiveDistance) * Mathf.Sin (Mathf.PI * pos [i] / 180.0f)), Quaternion.AngleAxis (rotateObstacle [i] + 180.0f, new Vector3 (0, 1, 0)));
							}
							obstacle.transform.parent = spawner.transform;
						}
					}
				}
			}

			Invoke ("SpawnObstacle", Random.Range (obstacleSpawnMin, obstacleSpawnMax));
		}
	}

	// Update is called once per frame
	void Update () {
		if (mainCam.transform.position.y >= winHeight && !spawnedTreeTop) {
			spawnedTreeTop = true;
			Vector3 h = this.transform.position + (Vector3.up*(mainCam.transform.position.y+treeTopHeightOffset));
			Transform tt1 = (Transform)Instantiate(treeTop, h + new Vector3(distance * Mathf.Cos(Mathf.PI*pos[0]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[0]/180.0f)),Quaternion.identity);
			Transform tt2 = (Transform)Instantiate(treeTop, h + new Vector3(distance * Mathf.Cos(Mathf.PI*pos[1]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[1]/180.0f)),Quaternion.identity);
			Transform tt3 = (Transform)Instantiate(treeTop, h + new Vector3(distance * Mathf.Cos(Mathf.PI*pos[2]/180.0f), 0, distance * Mathf.Sin(Mathf.PI*pos[2]/180.0f)),Quaternion.identity);

			tt1.transform.parent = spawner.transform;
			tt2.transform.parent = spawner.transform;
			tt3.transform.parent = spawner.transform;
		}

		GameObject monkey = GameObject.Find ("Monkey");
		testAutoMonkey x = monkey.GetComponent<testAutoMonkey> ();
		onTree = x.onTree;
		if ((Input.GetKeyDown (KeyCode.D) || (Input.GetKeyDown("right"))) && !moveLeft && !moveRight && onTree && !x.isJumping) {
			moveRight = true;
			x.isJumping = false;
			x.jumpVel = x.jumpImpulse;
			if( currentRotation >= 2 ) {
				currentRotation = 0;
			}
			else {
				currentRotation++;
			}
		}
		else if((Input.GetKeyDown (KeyCode.A) || (Input.GetKeyDown("left"))) && !moveLeft && !moveRight && onTree && !x.isJumping) {
			moveLeft = true;
			x.isJumping = false;
			x.jumpVel = x.jumpImpulse;
			if( currentRotation <= 0 ) {
				currentRotation = 2;
			}
			else {
				currentRotation--;
			}
		}

		if (moveRight) {
			x.onTree = false;
			if( rotations[currentRotation] == 0.0f ) {
				if( spawner.transform.eulerAngles.y >= rotations[2] ) {
					spawner.transform.Rotate (Vector3.up * Time.deltaTime * moveSpeed, Space.World);
				}
				else {
					Vector3 set = spawner.transform.eulerAngles;
					set.y = rotations[currentRotation];
					spawner.transform.eulerAngles = set;
					moveRight = false;
				}
			}
			else if( spawner.transform.eulerAngles.y >= rotations[currentRotation] ) {
				Vector3 set = spawner.transform.eulerAngles;
				set.y = rotations[currentRotation];
				spawner.transform.eulerAngles = set;
				moveRight = false;
			}
			else {
				spawner.transform.Rotate (Vector3.up * Time.deltaTime * moveSpeed, Space.World);
			}
		}
		else if (moveLeft) {
			x.onTree = false;
			if( rotations[currentRotation] == 0.0f ) {
				if( spawner.transform.eulerAngles.y-0.1f <= rotations[1] ) {
					spawner.transform.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
				}
				else {
					Vector3 set = spawner.transform.eulerAngles;
					set.y = rotations[currentRotation];
					spawner.transform.eulerAngles = set;
					moveLeft = false;
				}
			}
			else if( rotations[currentRotation] == 240.0f ) {
				if( spawner.transform.eulerAngles.y == 0.0f || spawner.transform.eulerAngles.y > rotations[currentRotation] ) {
					spawner.transform.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
				}
				else {
					Vector3 set = spawner.transform.eulerAngles;
					set.y = rotations[currentRotation];
					spawner.transform.eulerAngles = set;
					moveLeft = false;
				}
			}
			else if( spawner.transform.eulerAngles.y <= rotations[currentRotation] ) {
				Vector3 set = spawner.transform.eulerAngles;
				set.y = rotations[currentRotation];
				spawner.transform.eulerAngles = set;
				moveLeft = false;
			}
			else {
				spawner.transform.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
			}
		}
	}
}
