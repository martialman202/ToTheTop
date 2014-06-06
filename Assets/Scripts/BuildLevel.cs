using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Text;
using System.IO;

public class BuildLevel : MonoBehaviour {
	public GUISkin menuSkin;

	private string levelFileName = "Levels/";

	public float distance = 5.0f;

	public float heightOffset = 2.0f;

	public int introHeight = 25;
	public int outroHeight = 25;

	public int fillHeight = 2;

	private int trunkCounter = 0;

	public Transform emptyTrunk;
	public Transform beehiveTrunk;
	public Transform snakeTrunk;
	public Transform brushSawBlade;

	public Transform treeTop;
	
	public Transform bananas;
	public float bananasHeightOffset = 5.0f;

	public float treeHeight = 0;
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	public Texture2D topLeaves;
	public Vector2 size;
	private float barDisplay = 0.0f;
	private int fSize = (int)(0.05f * Screen.width);

	//Tree progress bar resources
	public Texture2D progressTree;
	public Texture2D treeFiller;

	private GameObject spawner;

	private bool startActiveManager;
	private List<Transform> trunks;
	private GameObject mainCam;

	/*void Awake() {
		Application.targetFrameRate = 120;
	}*/

	void OnGUI () {
		GUI.skin = menuSkin;

		// level indicator
		GUI.skin.box.fontSize = fSize;
		GUI.Box(new Rect(Screen.width*0.88f, Screen.width*0.01f, Screen.width*0.1f, Screen.width*0.1f), (Manager.Instance.levelIndex+1).ToString());
		
		// progress bar
		// draw the background:
		//GUI.Label(new Rect(0.82f * Screen.width, 0.06f * Screen.height, 0.25f * Screen.width, 0.2f * Screen.height), topLeaves);
		GUI.BeginGroup (new Rect (Screen.width*0.91f, 0.14f * Screen.height, size.x, size.y+(Screen.width*0.25f)));
			GUI.DrawTexture (new Rect (0, 0, size.x, size.y), progressBarEmpty);
			
			// draw the filled-in part:
			GUI.BeginGroup (new Rect (0, 0, size.x, size.y));
				GUI.DrawTexture (new Rect (0, size.y, size.x, -(size.y * barDisplay)), progressBarFull);
			GUI.EndGroup ();
			
		GUI.EndGroup ();

		//Draw TreeTop
		GUI.Label(new Rect(0.8f * Screen.width, 0.06f * Screen.height, 0.25f * Screen.width, 0.2f * Screen.height), topLeaves);
		//print (Manager.Instance.treeHeight);
	}

	private void LoadIntro()
	{
		for (int i = 0; i < introHeight; ++i) {
			Transform tree1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
			Transform tree2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
			Transform tree3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);

			trunks.Add(tree1);
			trunks.Add(tree2);
			trunks.Add(tree3);

			trunkCounter++;
		}
	}

	private void LoadOutro()
	{
		for (int i = 0; i < outroHeight; ++i) {
			Transform tree1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
			Transform tree2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
			Transform tree3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);

			trunks.Add(tree1);
			trunks.Add(tree2);
			trunks.Add(tree3);

			trunkCounter++;
		}

		Transform tt1 = (Transform)Instantiate(treeTop, new Vector3(distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)),Quaternion.identity);
		Transform tt2 = (Transform)Instantiate(treeTop, new Vector3(distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)),Quaternion.identity);
		Transform tt3 = (Transform)Instantiate(treeTop, new Vector3(distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)),Quaternion.identity);
				
		Transform b = (Transform)Instantiate(bananas, new Vector3(0,heightOffset*trunkCounter+bananasHeightOffset,0), Quaternion.identity);

		trunks.Add(tt1);
		trunks.Add(tt2);
		trunks.Add(tt3);
		trunks.Add(b);

		treeHeight = tt1.position.y;
		Manager.Instance.treeHeight = treeHeight;
	}

	private bool LoadLevel(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			TextAsset theReader = Resources.Load(fileName) as TextAsset;
			//print(fileName);
			//print (theReader.text);
			string[] linesFromFile = theReader.text.Split("\n"[0]);

			foreach ( string line in linesFromFile ) {
				//print (line);		
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
							tree1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
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
							tree2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
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
							tree3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);
							break;
						default:
							tree3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);
							break;
					}

					tree1.transform.Rotate(new Vector3(0,120.0f,0));
					tree3.transform.Rotate(new Vector3(0,240.0f,0));

					if( int.Parse(entries[0]) == 4 ) {
						Transform bsb = (Transform)Instantiate(brushSawBlade, new Vector3(0,heightOffset*trunkCounter,0), Quaternion.identity);
						trunks.Add(bsb);
					}

					trunks.Add(tree1);
					trunks.Add(tree2);
					trunks.Add(tree3);

					trunkCounter++;

					for (int i = 0; i < fillHeight; ++i) {
						Transform extra1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
						Transform extra2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
						Transform extra3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);

						trunks.Add(extra1);
						trunks.Add(extra2);
						trunks.Add(extra3);

						trunkCounter++;
					}
				}
			}
		return true;
	}
	catch (Exception e)
	{
		print(e.Message);
		return false;
	}
}

	// Use this for initialization
	void Start () {
		trunks = new List<Transform> ();

		spawner = this.gameObject;
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");

		LoadIntro ();
		LoadLevel (levelFileName+Manager.Instance.levelFileName);
		LoadOutro ();

		size = new Vector2(Screen.width*0.04f,Screen.height*0.25f);
	}
	
	// Update is called once per frame
	void Update () {
		// Update progress bar
		barDisplay = Manager.Instance.monkeyHeight/Manager.Instance.treeHeight;


		// Check if camera is setup
		if( mainCam.GetComponent<CameraController>().hasFoundBananas() ) {
			startActiveManager = true;
		}

		// Set objects to active/inactive if they're on the screen
		if( startActiveManager ) {
			float camY = mainCam.transform.position.y;
			foreach( Transform t in trunks ) {
				if( t.position.y < camY + 40.0f && t.position.y > camY - 40.0f ) {
					t.gameObject.SetActive(true);
				}
				else {
					t.gameObject.SetActive(false);
				}
			}
		}

	}
}