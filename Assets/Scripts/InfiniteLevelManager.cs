using UnityEngine;
using System.Collections;

public class InfiniteLevelManager : MonoBehaviour {
	public GUISkin menuSkin;
	
	protected Transform[] trunks;

	public bool empty; // check to spawn only empty trunks

	public Transform emptyTrunk;
	public Transform beehiveTrunk;
	public Transform snakeTrunk;
	public Transform brushSawBlade;

	private GameObject mainCam;
	public float camOffset = 2.0f;

	private int introHeight = 25;

	protected float distance = 5.0f;
	protected float heightOffset = 2.0f;
	protected int trunkCounter = 0;

	private const int numEmptyTrunks = 100;
	private const int numBeehiveTrunks = 10;
	private const int numSnakeTrunks = 10;
	private const int numBrushModels = 5;

	private int numTrunks;
	private int emptyStartIdx, emptyEndIdx, beeStartIdx, beeEndIdx, snakeStartIdx, snakeEndIdx, brushStartIdx, brushEndIdx;

	public float obstacleSpawnMin = 0.75f;
	public float obstacleSpawnMax = 3.0f;
	public float obstacleSpawnTime;
	private float speedFactor; // factor to increase/decrease time for difficulty
	protected bool spawnObstacle = false;

	private int fSize = (int)(0.05f * Screen.width);

	void OnGUI () {
		GUI.skin = menuSkin;
		
		// score
		GUI.skin.box.fontSize = fSize;
		/*
		int originalSize = GUI.skin.box.fontSize;
		int boxSize = originalSize / 2;
		GUI.skin.box.fontSize = boxSize;
		*/
		GUI.Box(new Rect(Screen.width*0.80f, Screen.width*0.01f, Screen.width*0.18f, Screen.width*0.1f), (Manager.Instance.score).ToString());
		//GUI.skin.box.fontSize = originalSize;
	}

	// creates the pool of trunks that will be managed by this system
	protected void instantiateTrunkPool() {
		numTrunks = numEmptyTrunks+numBeehiveTrunks+numSnakeTrunks+numBrushModels;
		trunks = new Transform[numTrunks];

		emptyStartIdx = 0;
		emptyEndIdx = emptyStartIdx + numEmptyTrunks;
		beeStartIdx = emptyEndIdx;
		beeEndIdx = beeStartIdx + numBeehiveTrunks;
		snakeStartIdx = beeEndIdx;
		snakeEndIdx = snakeStartIdx + numSnakeTrunks;
		brushStartIdx = snakeEndIdx;
		brushEndIdx = brushStartIdx + numBrushModels;

		for( int i = emptyStartIdx; i < emptyEndIdx; ++i ) {
			trunks[i] = (Transform)Instantiate(emptyTrunk,Vector3.zero,Quaternion.identity);
			trunks[i].gameObject.SetActive(false);
		}
		
		for( int i = beeStartIdx; i < beeEndIdx; ++i ) {
			trunks[i] = (Transform)Instantiate(beehiveTrunk,Vector3.zero,Quaternion.identity);
			trunks[i].gameObject.SetActive(false);
		}
		
		for( int i = snakeStartIdx; i < snakeEndIdx; ++i ) {
			trunks[i] = (Transform)Instantiate(snakeTrunk,Vector3.zero,Quaternion.identity);
			trunks[i].gameObject.SetActive(false);
		}
		
		for( int i = brushStartIdx; i < brushEndIdx; ++i ) {
			trunks[i] = (Transform)Instantiate(brushSawBlade,Vector3.zero,Quaternion.identity);
			trunks[i].gameObject.SetActive(false);
		}
	}

	// creates an intro of empty tree trunks
	protected void createIntro() {
		for( int i = emptyStartIdx; i < introHeight*3; i += 3 ) {
			trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f));
			trunks[i].gameObject.SetActive(true);

			trunks[i+1].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f));
			trunks[i+1].gameObject.SetActive(true);

			trunks[i+2].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f));
			trunks[i+2].gameObject.SetActive(true);

			trunkCounter++;
		}
	}

	// sets any neccessary trunks/obstacles to inactive
	protected bool cleanUp() {
		bool wasRowRecycled = false;
		for( int i = 0; i < numTrunks; ++i ) {
			if( trunks[i].gameObject.activeSelf && trunks[i].transform.position.y < mainCam.transform.position.y - camOffset ) {
				trunks[i].gameObject.SetActive(false);
				wasRowRecycled = true;
			}
		}
		return wasRowRecycled;
	}

	// add an empty row of tree trunks
	protected void extendEmptyTrunks() {
		bool tree1Set = false;
		bool tree2Set = false;
		bool tree3Set = false;

		for( int i = emptyStartIdx; i < emptyEndIdx; ++i ) {
			if( tree1Set && tree2Set && tree3Set ) {
				break;
			}
			else if( !trunks[i].gameObject.activeSelf ) {
				if( !tree1Set ) {
					trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f));
					trunks[i].gameObject.SetActive(true);
					tree1Set = true;
				}
				else if( !tree2Set ) {
					trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f));
					trunks[i].gameObject.SetActive(true);
					tree2Set = true;
				}
				else if( !tree3Set ) {
					trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f));
					trunks[i].gameObject.SetActive(true);
					tree3Set = true;
				}
			}
		}
		trunkCounter++;
	}

	protected void extendWBrush() {
		// add a brush model to this row
		for( int i = brushStartIdx; i < brushEndIdx; ++i ) {
			if( !trunks[i].gameObject.activeSelf ) {
				trunks[i].transform.position = new Vector3(0,heightOffset*trunkCounter,0);
				trunks[i].gameObject.SetActive(true);
				break;
			}
		}
		// then add in a row of empty tree trunks
		extendEmptyTrunks();
	}

	// add a new row with either beehives or snakes
	// amount == amount of obstacles you want in that row 1-3
	// treeIndex == tree index for the (first) obstacle
	// obstacleID == 0 for beehives, 1 for snakes
	protected void extendWBeehivesOrSnakes(int amount, int treeIndex, int obstacleID) {
		bool[] treeSet;
		treeSet = new bool[3];
		treeSet[0] = false;
		treeSet[1] = false;
		treeSet[2] = false;

		// restrict amount to the range [1-3]
		amount = (amount < 1) ? 1 : amount;
		amount = (amount > 3) ? 3 : amount;

		// guarantee that treeIndex is within range [0-2]
		treeIndex = treeIndex % 3;

		int startIdx, endIdx;
		if (obstacleID == 0) {	// set loop indices to pull from the beehive trunk obstacles
			startIdx = beeStartIdx;
			endIdx = beeEndIdx;
		}
		else {					// set loop indices to pull from the snake trunk obstacles
			startIdx = snakeStartIdx;
			endIdx = snakeEndIdx;
		}

		for( int i = startIdx; i < endIdx; ++i ) {
			if( (amount == 1 && treeSet[treeIndex]) || 
			   (amount == 2 && treeSet[treeIndex] && treeSet[(treeIndex+1)%3]) || 
			   (amount == 3 && treeSet[treeIndex] && treeSet[(treeIndex+1)%3] && treeSet[(treeIndex+2)%3]) ) {
				break;
			}
			if( !trunks[i].gameObject.activeSelf ) {
				if( !treeSet[treeIndex] ) {
					switch(treeIndex) {
						case 0:
							trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f));
							trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,120.0f,0));
							trunks[i].gameObject.SetActive(true);
							break;
						case 1:
							trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f));
							trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
							trunks[i].gameObject.SetActive(true);
							break;
						case 2:
							trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f));
							trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,240.0f,0));
							trunks[i].gameObject.SetActive(true);
							break;
					}
					treeSet[treeIndex] = true;
				}
				else if( amount > 1 && !treeSet[(treeIndex+1)%3] ) {
					switch(treeIndex) {
						case 0:
							trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f));
							trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
							trunks[i].gameObject.SetActive(true);
							break;
						case 1:
							trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f));
							trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,240.0f,0));
							trunks[i].gameObject.SetActive(true);
							break;
						case 2:
							trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f));
							trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,120.0f,0));
							trunks[i].gameObject.SetActive(true);
							break;
					}
					treeSet[(treeIndex+1)%3] = true;
				}
				else if( amount > 2 && !treeSet[(treeIndex+2)%3] ) {
					switch(treeIndex) {
						case 0:
							trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f));
							trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,240.0f,0));
							trunks[i].gameObject.SetActive(true);
							break;
						case 1:
							trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f));
							trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,120.0f,0));
							trunks[i].gameObject.SetActive(true);
							break;
						case 2:
							trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f));
							trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
							trunks[i].gameObject.SetActive(true);
							break;
					}
					treeSet[(treeIndex+2)%3] = true;
				}
			}
		}

		// add empty trunks to the remaining trees in this row
		for( int i = emptyStartIdx; i < emptyEndIdx; ++i ) {
			if( treeSet[0] && treeSet[1] && treeSet[2] ) {
				break;
			}
			else if( !trunks[i].gameObject.activeSelf ) {
				if( !treeSet[0] ) {
					trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f));
					trunks[i].gameObject.SetActive(true);
					treeSet[0] = true;
				}
				else if( !treeSet[1] ) {
					trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f));
					trunks[i].gameObject.SetActive(true);
					treeSet[1] = true;
				}
				else if( !treeSet[2] ) {
					trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f));
					trunks[i].gameObject.SetActive(true);
					treeSet[2] = true;
				}
			}
		}

		trunkCounter++;
	}

	// Add in a custom row that allows individual choosing of each tree obstacle
	// 0 == empty trunk
	// 1 == beehive obstacle
	// 2 == snake obstacle
	protected void extendCustomRow(int obstacle1ID, int obstacle2ID, int obstacle3ID) {
		int tree1StartIdx, tree1EndIdx, tree2StartIdx, tree2EndIdx, tree3StartIdx, tree3EndIdx;

		bool[] treeSet;
		treeSet = new bool[3];
		treeSet[0] = false;
		treeSet[1] = false;
		treeSet[2] = false;

		// Set Tree 1 indices
		switch (obstacle1ID) {
			case 0:
				tree1StartIdx = emptyStartIdx;
				tree1EndIdx = emptyEndIdx;
				break;
			case 1:
				tree1StartIdx = beeStartIdx;
				tree1EndIdx = beeEndIdx;
				break;
			case 2:
				tree1StartIdx = snakeStartIdx;
				tree1EndIdx = snakeEndIdx;
				break;
			default:
				tree1StartIdx = emptyStartIdx;
				tree1EndIdx = emptyEndIdx;
				break;
		}
		// Set Tree 2 indices
		switch (obstacle2ID) {
		case 0:
			tree2StartIdx = emptyStartIdx;
			tree2EndIdx = emptyEndIdx;
			break;
		case 1:
			tree2StartIdx = beeStartIdx;
			tree2EndIdx = beeEndIdx;
			break;
		case 2:
			tree2StartIdx = snakeStartIdx;
			tree2EndIdx = snakeEndIdx;
			break;
		default:
			tree2StartIdx = emptyStartIdx;
			tree2EndIdx = emptyEndIdx;
			break;
		}
		// Set Tree 3 indices
		switch (obstacle3ID) {
		case 0:
			tree3StartIdx = emptyStartIdx;
			tree3EndIdx = emptyEndIdx;
			break;
		case 1:
			tree3StartIdx = beeStartIdx;
			tree3EndIdx = beeEndIdx;
			break;
		case 2:
			tree3StartIdx = snakeStartIdx;
			tree3EndIdx = snakeEndIdx;
			break;
		default:
			tree3StartIdx = emptyStartIdx;
			tree3EndIdx = emptyEndIdx;
			break;
		}

		// add in tree 1
		for( int i = tree1StartIdx; i < tree1EndIdx; ++i ) {
			if( !trunks[i].gameObject.activeSelf ) {
				trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f));
				trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,120.0f,0));
				trunks[i].gameObject.SetActive(true);
				treeSet[0] = true;
				break;
			}
		}
		// add in tree 2
		for( int i = tree2StartIdx; i < tree2EndIdx; ++i ) {
			if( !trunks[i].gameObject.activeSelf ) {
				trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f));
				trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
				trunks[i].gameObject.SetActive(true);
				treeSet[1] = true;
				break;
			}
		}
		// add in tree 3
		for( int i = tree3StartIdx; i < tree3EndIdx; ++i ) {
			if( !trunks[i].gameObject.activeSelf ) {
				trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f));
				trunks[i].transform.rotation = Quaternion.Euler(new Vector3(0,240.0f,0));
				trunks[i].gameObject.SetActive(true);
				treeSet[2] = true;
				break;
			}
		}


		// add empty trunks to any tree where an obstacle could not be placed (i.e. no more available in pool)
		for( int i = emptyStartIdx; i < emptyEndIdx; ++i ) {
			if( treeSet[0] && treeSet[1] && treeSet[2] ) {
				break;
			}
			else if( !trunks[i].gameObject.activeSelf ) {
				if( !treeSet[0] ) {
					trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f));
					trunks[i].gameObject.SetActive(true);
					treeSet[0] = true;
				}
				else if( !treeSet[1] ) {
					trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f));
					trunks[i].gameObject.SetActive(true);
					treeSet[1] = true;
				}
				else if( !treeSet[2] ) {
					trunks[i].transform.position = new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f));
					trunks[i].gameObject.SetActive(true);
					treeSet[2] = true;
				}
			}
		}

		trunkCounter++;
	}

	// Use this for initialization
	public virtual void Start () {
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");

		instantiateTrunkPool();
		createIntro ();
		Invoke ("SpawnObstacle", 2);

		obstacleSpawnTime = obstacleSpawnMax;
	}

	protected virtual void SpawnObstacle () {
		if (!spawnObstacle)
			spawnObstacle = true;
		Invoke ("SpawnObstacle", obstacleSpawnTime); //Random.Range (obstacleSpawnTime, obstacleSpawnMax));
	}
	
	// Update is called once per frame
	void Update () {
		// run clean up to recycle any trunks that are no longer on scene
		bool wasRowRecycled = cleanUp();

		// 0 == empty trunk
		// 1 == beehive obstacle
		// 2 == snake obstacle
		// 3 == deathvine
		// create a new row if a row was just recycled back into the trunk pool
		if( wasRowRecycled ) {
			if( spawnObstacle && !empty ) {
				bool spawnUnclimbable = false;
				int whichObstacle = Random.Range (1, 4); // choose which obstacle to spawn, either 1 (beehive), 2 (snake), or 3 (deathvine)
				if (whichObstacle == 3)
					spawnUnclimbable = true;
				
				if (spawnUnclimbable) 
					extendWBrush();
				else {
					int [] trees = new int[3];
					trees[0] = Random.Range (0, 3);
					trees[1] = Random.Range (0, 3);
					trees[2] = Random.Range (0, 3);
					extendCustomRow(trees[0],trees[1],trees[2]);
				}

				spawnObstacle =  false;
			}
			else
				extendEmptyTrunks();
			//extendWBeehivesOrSnakes(2,2,1);
			//extendWBrush();
			//extendCustomRow(1,0,2);
		}

		speedFactor = 0.001f;
		if (obstacleSpawnTime > obstacleSpawnMin)
			obstacleSpawnTime -= speedFactor;
	}
}
