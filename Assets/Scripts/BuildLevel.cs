using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.IO;

public class BuildLevel : MonoBehaviour {

	public string levelFileName = "Levels/test";

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

	private GameObject spawner;

	/*void Awake() {
		Application.targetFrameRate = 120;
	}*/

	private void LoadIntro()
	{
		for (int i = 0; i < introHeight; ++i) {
			Transform tree1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
			Transform tree2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
			Transform tree3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);

			trunkCounter++;
		}
	}

	private void LoadOutro()
	{
		for (int i = 0; i < outroHeight; ++i) {
			Transform tree1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
			Transform tree2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
			Transform tree3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);

			trunkCounter++;
		}

		Transform tt1 = (Transform)Instantiate(treeTop, new Vector3(distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)),Quaternion.identity);
		Transform tt2 = (Transform)Instantiate(treeTop, new Vector3(distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)),Quaternion.identity);
		Transform tt3 = (Transform)Instantiate(treeTop, new Vector3(distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)),Quaternion.identity);

		Instantiate(bananas, new Vector3(0,heightOffset*trunkCounter+bananasHeightOffset,0), Quaternion.identity);
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

					trunkCounter++;

					for (int i = 0; i < fillHeight; ++i) {
						Transform extra1 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*150.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*150.0f/180.0f)), Quaternion.identity);
						Transform extra2 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*270.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*270.0f/180.0f)), Quaternion.identity);
						Transform extra3 = (Transform)Instantiate(emptyTrunk, new Vector3 (distance * Mathf.Cos(Mathf.PI*30.0f/180.0f), heightOffset*trunkCounter, distance * Mathf.Sin(Mathf.PI*30.0f/180.0f)), Quaternion.identity);

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
		spawner = this.gameObject;

		LoadIntro ();
		LoadLevel (levelFileName);
		LoadOutro ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}