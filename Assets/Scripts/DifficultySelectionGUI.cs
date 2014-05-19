using UnityEngine;
using System.Collections;

public class DifficultySelectionGUI : MonoBehaviour {

	public GUISkin menuSkin;
	public Texture2D [] stars = new Texture2D[4];
	private float buttonHeight = Screen.width/6;
	private float betweenButton = Screen.width/4;

	void OnGUI() {
		if(Input.GetKey(KeyCode.Escape)) {
			Application.LoadLevel("TitleScene");
		}

		GUI.skin = menuSkin;

		// Make a group on the center of the screen
		/*
		GUI.BeginGroup (new Rect (Screen.width / 2 - Screen.width/4, Screen.height / 2 - Screen.width/4, Screen.width/2, buttonHeight * 3));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
		
		// We'll make a box so you can see where the group is on-screen.
		if (GUI.Button (new Rect (0, 0, Screen.width/2, buttonHeight), "Easy"))
			Application.LoadLevel ("EasyLevels");
		GUI.Button (new Rect (0, betweenButton, Screen.width/2, buttonHeight), "Medium");
			//A/plication.LoadLevel ("DifficultySelectionScene");
		//GUI.Button (new Rect (0, 140, 150, 50), "Hard");
		
		// End the group we started above. This is very important to remember!
		GUI.EndGroup ();
		*/

		// Grid level selection
		GUI.BeginGroup(new Rect(0.1f * Screen.width, Screen.height/2 - Screen.width/4, 0.9f * Screen.width, 3 * buttonHeight));
		//Star labels
		GUI.Label(new Rect(0.02f * Screen.width, 0, 0.22f * Screen.width, 0.1f * Screen.width), stars[1]);
		GUI.Label(new Rect(0.31f * Screen.width, 0, 0.22f * Screen.width, 0.1f * Screen.width), stars[2]);
		GUI.Label(new Rect(0.60f * Screen.width, 0, 0.22f * Screen.width, 0.1f * Screen.width), stars[3]);

		//Level buttons
		if(GUI.Button(new Rect(0, 0.1f * Screen.width, 0.22f * Screen.width, 0.22f * Screen.width), "1"))
			Application.LoadLevel("EasyLevels");
		if(GUI.Button(new Rect(0.29f * Screen.width, 0.1f * Screen.width, 0.22f * Screen.width, 0.22f * Screen.width), "2"));
		if(GUI.Button(new Rect(0.58f * Screen.width, 0.1f * Screen.width, 0.22f * Screen.width, 0.22f * Screen.width), "3"));
		GUI.EndGroup ();
	}
}
