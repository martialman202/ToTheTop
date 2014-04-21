using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {

	private GameObject player ;
	private testAutoMonkey monkeyScript = player.GetComponent<testAutoMonkey>();

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		monkeyScript = player.GetComponent<testAutoMonkey>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.Label (new Rect (110, 110, 200, 130), "Life: " + Other.lifePoints);
	}
}
