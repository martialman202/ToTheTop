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

	public Transform [] obstacles;
	public float obstacleSpawnMin = 0.5f;
	public float obstacleSpawnMax = 1.25f;
	public float obstaclePlacementOffset = 10f;

	private bool moveRight = false;
	private bool moveLeft = false;

	public GameObject spawner;

	private GameObject mainCam;
	private bool onTree = false;

	void Start () {
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

		tree1.transform.parent = spawner.transform;
		tree2.transform.parent = spawner.transform;
		tree3.transform.parent = spawner.transform;

		SpawnObstacle();
	}

	void SpawnObstacle() {
		float beeHiveDistance = distance+1.0f;

		int numObstacles = Random.Range (1, 3); // number of obstacles to spawn, either 1 or 2
		if (!moveLeft && !moveRight) {
			for (int i = 0; i < numObstacles; i++) {
				int whichObstacle = Random.Range (0, obstacles.Length); // choose which obstacle to spawn
				int whichTree = Random.Range (0, 3); // choose which tree to spawn on

				float rotateObstacle = 0.0f;
				switch (whichTree) {
				case 1:
					rotateObstacle = 120.0f;
					break;
				case 0:
					rotateObstacle = 240.0f;
					break;
				default:
					break;
				}

				Transform obstacle = (Transform)Instantiate (obstacles [whichObstacle], new Vector3 ((beeHiveDistance) * Mathf.Cos (Mathf.PI * pos [whichTree] / 180.0f), mainCam.transform.position.y + obstaclePlacementOffset, (beeHiveDistance) * Mathf.Sin (Mathf.PI * pos [whichTree] / 180.0f)), Quaternion.AngleAxis(rotateObstacle,new Vector3(0,1,0)));
				obstacle.transform.parent = spawner.transform;
			}
		}
		Invoke("SpawnObstacle",Random.Range(obstacleSpawnMin,obstacleSpawnMax));
	}

	// Update is called once per frame
	void Update () {
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
