using UnityEngine;
using System.Collections;

public class TitleSceneGUI : MonoBehaviour {

	private float buttonHeight = Screen.width/6;
	private float betweenButton = Screen.width/4;

	void OnGUI() {
		// Make a group on the center of the screen
		GUI.BeginGroup (new Rect (Screen.width/2 - Screen.width/4, Screen.height/2 - Screen.width/4, Screen.width/2, buttonHeight*4));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
			
			// We'll make a box so you can see where the group is on-screen.
			if (GUI.Button (new Rect (0, 0, Screen.width/2, buttonHeight), "Classic"))
				Application.LoadLevel ("MainScene");
			if (GUI.Button (new Rect (0, betweenButton, Screen.width/2, buttonHeight), "Arcade"))
				Application.LoadLevel ("DifficultySelectionScene");
			if (GUI.Button (new Rect (0, 2*betweenButton, Screen.width/2, buttonHeight), "Exit"))
				Application.Quit ();
			
		// End the group we started above. This is very important to remember!
		GUI.EndGroup ();
	}
}
