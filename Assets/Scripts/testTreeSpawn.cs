using UnityEngine;
using System.Collections;

public class testTreeSpawn : MonoBehaviour {

	public Transform treeSection;
	public float distance = 20.0f; //This is the distance from each tree to the spawner

	private float spawnAngle = 120.0f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 1; i++) 
		{
			Instantiate(treeSection, new Vector3(distance, 2, 0), Quaternion.identity);
			Instantiate(treeSection, new Vector3(distance * Mathf.Cos(.75f), 2, distance * Mathf.Sin (.75f)), Quaternion.identity);
			Instantiate(treeSection, new Vector3(distance * Mathf.Cos(-.25f), 2, distance * Mathf.Sin (-.250f)), Quaternion.identity);

	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
