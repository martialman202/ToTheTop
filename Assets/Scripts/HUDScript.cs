using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {

	public bool displayLife = true;
	public Texture2D heart;
	public float heartScale = 1.0f;

	private GameObject player ;
	private testAutoMonkey monkeyScript;
	private float heartSize = 16.0f;

	public bool displayWin;



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
		//Life Display
		float heartD = heartSize * heartScale;
		if (displayLife) 
		for (int i = 0; i < monkeyScript.lifePoints; i++) {
			GUI.DrawTexture(new Rect(100+(heartD*i),heartD, heartD, heartD), heart);
		}

		//Win Screen
		if (displayWin) {
			//TODO: set display win from testAutoMonkey
		}
	}
}
