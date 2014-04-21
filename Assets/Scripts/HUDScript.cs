using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {

	public Texture2D heart;

	private GameObject player ;
	private testAutoMonkey monkeyScript;


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
		//GUI.Label (new Rect (110, 110, 200, 130), "Life: " + monkeyScript.lifePoints);

		if (monkeyScript.lifePoints <= 3)
		for (int i = 0; i < monkeyScript.lifePoints; i++) {
			GUI.DrawTexture(new Rect(110+(16*i),16, 16, 16), heart);
		}
	}
}
