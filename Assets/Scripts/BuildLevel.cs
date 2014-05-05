using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.IO;

public class BuildLevel : MonoBehaviour {

	public string levelFileName = "Assets/Levels/test.level";

	public float distance = 5.0f;

	public float heightOffset = 2.0f;

	public int introHeight = 25;
	public int outroHeight = 25;

	public int fillHeight = 2;

	private int trunkCounter = 0;

	public Transform emptyTrunk;
	public Transform beehiveTrunk;
	public Transform snakeTrunk;
	public Transform bushTrunk;

	public Transform treeTop;
	
	public Transform bananas;
	public float bananasHeightOffset = 5.0f;

	public int currentRotation = 0;
	public float[] rotations;
	public float moveSpeed = 150.0f;

	private bool moveRight = false;
	private bool moveLeft = false;

	private GameObject mainCam;
	private bool onTree = false;

	private GameObject spawner;

	private void LoadIntro()
	{
		for (int i = 0; i < introHeight; ++i) {
			Transform tree1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
			Transform tree2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
			Transform tree3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);

			tree1.transform.parent = spawner.transform;
			tree2.transform.parent = spawner.transform;
			tree3.transform.parent = spawner.transform;

			trunkCounter++;
		}
	}

	private void LoadOutro()
	{
		for (int i = 0; i < outroHeight; ++i) {
			Transform tree1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
			Transform tree2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
			Transform tree3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);
			
			tree1.transform.parent = spawner.transform;
			tree2.transform.parent = spawner.transform;
			tree3.transform.parent = spawner.transform;
			
			trunkCounter++;
		}

		Transform tt1 = (Transform)Instantiate(treeTop, new Vector3(distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)),Quaternion.identity);
		Transform tt2 = (Transform)Instantiate(treeTop, new Vector3(distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)),Quaternion.identity);
		Transform tt3 = (Transform)Instantiate(treeTop, new Vector3(distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)),Quaternion.identity);
		
		tt1.transform.parent = spawner.transform;
		tt2.transform.parent = spawner.transform;
		tt3.transform.parent = spawner.transform;
		
		Instantiate(bananas, new Vector3(0,heightOffset*trunkCounter+bananasHeightOffset,0), Quaternion.identity);
	}

	private bool LoadLevel(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);

			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();
					
					if (line != null)
					{
						// Do whatever you need to do with the text line, it's a string now
						// In this example, I split it into arguments based on comma
						// deliniators, then send that array to DoStuff()
						string[] entries = line.Split(' ');
						if (entries.Length == 3)
						{
							Transform tree1;
							Transform tree2;
							Transform tree3;

							switch(int.Parse(entries[0])) {
							case 1:
								tree1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
								break;
							case 2:
								tree1 = (Transform)Instantiate(beehiveTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
								break;
							case 3:
								tree1 = (Transform)Instantiate(snakeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
								break;
							case 4:
								tree1 = (Transform)Instantiate(bushTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
								break;
							default:
								tree1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
								break;
							}

							switch(int.Parse(entries[1])) {
							case 1:
								tree2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
								break;
							case 2:
								tree2 = (Transform)Instantiate(beehiveTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
								break;
							case 3:
								tree2 = (Transform)Instantiate(snakeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
								break;
							case 4:
								tree2 = (Transform)Instantiate(bushTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
								break;
							default:
								tree2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
								break;
							}

							switch(int.Parse(entries[2])) {
							case 1:
								tree3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);
								break;
							case 2:
								tree3 = (Transform)Instantiate(beehiveTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);
								break;
							case 3:
								tree3 = (Transform)Instantiate(snakeTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);
								break;
							case 4:
								tree3 = (Transform)Instantiate(bushTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);
								break;
							default:
								tree3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);
								break;
							}

							tree1.transform.Rotate(new Vector3(0,120.0f,0));
							tree3.transform.Rotate(new Vector3(0,240.0f,0));

							tree1.transform.parent = spawner.transform;
							tree2.transform.parent = spawner.transform;
							tree3.transform.parent = spawner.transform;

							trunkCounter++;

							for (int i = 0; i < fillHeight; ++i) {
								Transform extra1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
								Transform extra2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
								Transform extra3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);
								
								extra1.transform.parent = spawner.transform;
								extra2.transform.parent = spawner.transform;
								extra3.transform.parent = spawner.transform;
								
								trunkCounter++;
							}
						}
					}
				}
				while (line != null);
				
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				return true;
			}
		}
		
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e)
		{
			print(e.Message);
			return false;
		}
	}

	// Use this for initialization
	void Start () {
		spawner = this.gameObject;
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");

		rotations = new float[3];
		rotations[0] = 0.0f;
		rotations[1] = 120.0f;
		rotations[2] = 240.0f;

		LoadIntro ();
		LoadLevel (levelFileName);
		LoadOutro ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject m = GameObject.Find ("Monkey");
		testAutoMonkey monkey = m.GetComponent<testAutoMonkey> ();
		onTree = monkey.onTree;
		if ((Input.GetKeyDown (KeyCode.D) || (Input.GetKeyDown("right"))) && !moveLeft && !moveRight && onTree && !monkey.isJumping) {
			moveRight = true;
			monkey.isJumping = false;
			monkey.jumpVel = monkey.jumpImpulse;
			if( currentRotation >= 2 ) {
				currentRotation = 0;
			}
			else {
				currentRotation++;
			}
		}
		else if((Input.GetKeyDown (KeyCode.A) || (Input.GetKeyDown("left"))) && !moveLeft && !moveRight && onTree && !monkey.isJumping) {
			moveLeft = true;
			monkey.isJumping = false;
			monkey.jumpVel = monkey.jumpImpulse;
			if( currentRotation <= 0 ) {
				currentRotation = 2;
			}
			else {
				currentRotation--;
			}
		}
		
		if (moveRight) {
			monkey.onTree = false;
			if( rotations[currentRotation] == 0.0f ) {
				if( spawner.transform.eulerAngles.y >= rotations[2] ) {
					spawner.transform.Rotate (Vector3.up * Time.deltaTime * moveSpeed, Space.World);
				}
				else {
					Vector3 set = spawner.transform.eulerAngles;
					set.y = rotations[currentRotation];
					spawner.transform.eulerAngles = set;
					moveRight = false;
					monkey.onTree = true;
				}
			}
			else if( spawner.transform.eulerAngles.y >= rotations[currentRotation] ) {
				Vector3 set = spawner.transform.eulerAngles;
				set.y = rotations[currentRotation];
				spawner.transform.eulerAngles = set;
				moveRight = false;
				monkey.onTree = true;
			}
			else {
				spawner.transform.Rotate (Vector3.up * Time.deltaTime * moveSpeed, Space.World);
			}
		}
		else if (moveLeft) {
			monkey.onTree = false;
			if( rotations[currentRotation] == 0.0f ) {
				if( spawner.transform.eulerAngles.y-0.1f <= rotations[1] ) {
					spawner.transform.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
				}
				else {
					Vector3 set = spawner.transform.eulerAngles;
					set.y = rotations[currentRotation];
					spawner.transform.eulerAngles = set;
					moveLeft = false;
					monkey.onTree = true;
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
					monkey.onTree = true;
				}
			}
			else if( spawner.transform.eulerAngles.y <= rotations[currentRotation] ) {
				Vector3 set = spawner.transform.eulerAngles;
				set.y = rotations[currentRotation];
				spawner.transform.eulerAngles = set;
				moveLeft = false;
				monkey.onTree = true;
			}
			else {
				spawner.transform.Rotate (Vector3.down * Time.deltaTime * moveSpeed, Space.World);
			}
		}
	}
}