using UnityEngine;
using System.Collections;

public class testTreeSpawn : MonoBehaviour {

	public Transform treeSection;
	public float distance = 15.0f; //This is the distance from each tree to the spawner.
	public float treeSectionHeight = 8.0f;
	public Transform [] traps;
	

	private float spawnAngle = 120.0f;
	private float angle1 = 120.0f * Mathf.PI / 180;
	private float angle2 = 240.0f * Mathf.PI / 180;


	// Use this for initialization
	void Start () {

		float height = 8.0f; //TODO how can I access the height of the trunk?

		for (int i = 0; i < 20; i++) 
		{
			Instantiate(treeSection, new Vector3(distance,height*i, 0), Quaternion.identity);
			Instantiate(treeSection, new Vector3(distance * Mathf.Cos(angle1),height*i, distance * Mathf.Sin (angle1)), Quaternion.identity);
			Instantiate(treeSection, new Vector3(distance * Mathf.Cos(angle2),height*i, distance * Mathf.Sin (angle2)), Quaternion.identity);

	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawnTrees () {
		float height = 8.0f; //TODO how can I access the height of the trunk?
		
		for (int i = 0; i < 20; i++) 
		{
			Instantiate(treeSection, new Vector3(distance,height*i, 0), Quaternion.identity);
			Instantiate(treeSection, new Vector3(distance * Mathf.Cos(angle1),height*i, distance * Mathf.Sin (angle1)), Quaternion.identity);
			Instantiate(treeSection, new Vector3(distance * Mathf.Cos(angle2),height*i, distance * Mathf.Sin (angle2)), Quaternion.identity);
				
		}
	}

	void spawnTraps () {

	}

}
