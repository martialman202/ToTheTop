using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {
	public GUISkin menuSkin;
	
	public bool displayLife = true;
	public Texture2D heart;
	public float heartScale = 1.0f;
	
	private bool paused = false;	
	private float buttonHeight = Screen.width/6;
	private float betweenButton = Screen.width/4;

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
		GUI.skin = menuSkin;
		
		//Life Display
		float heartD = 0.05f * Screen.width;
		if (displayLife) {
			for (int i = 0; i < monkeyScript.lifePoints; i++) {
				GUI.DrawTexture (new Rect (heartD + (heartD * i), heartD, heartD, heartD), heart);
			}
		}

		//Pause Button
		int originalSize = GUI.skin.button.fontSize;
		int pauseSize = originalSize / 2;
		GUI.skin.button.fontSize = pauseSize;
		if(GUI.Button(new Rect(0.85f * Screen.width, Screen.height - (0.15f * Screen.width), 0.1f * Screen.width, 0.1f * Screen.width), "| |")) {
			if(!paused) paused = true;
			else paused = false;
		}
		GUI.skin.button.fontSize = originalSize;

		if (paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;

		//Pause Menu
		if (paused) {
			GUI.BeginGroup (new Rect (Screen.width/2 - Screen.width/4, Screen.height/2 - Screen.width/4, Screen.width/2, buttonHeight*4));
				// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
				
				// We'll make a box so you can see where the group is on-screen.
				if (GUI.Button (new Rect (0, 0, Screen.width/2, buttonHeight), "Resume")) {
					paused = false;
					Time.timeScale = 1;
				}
				if (GUI.Button (new Rect (0, betweenButton, Screen.width/2, buttonHeight), "Restart")) {
					Application.LoadLevel(Manager.Instance.prevLevel);
				}
				if (GUI.Button (new Rect (0, 2*betweenButton, Screen.width/2, buttonHeight), "Menu")) {
					Application.LoadLevel ("TitleScene");
				}
				
			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		}

		//Win Screen
		if (displayWin) {
			//TODO: set display win from testAutoMonkey
		}
	}
}
