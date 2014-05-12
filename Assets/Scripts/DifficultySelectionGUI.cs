using UnityEngine;
using System.Collections;

public class DifficultySelectionGUI : MonoBehaviour {
	void OnGUI() {
		// Make a group on the center of the screen
		GUI.BeginGroup (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 75, 150, 150));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
		
		// We'll make a box so you can see where the group is on-screen.
		if (GUI.Button (new Rect (0, 0, 150, 50), "Easy"))
			Application.LoadLevel ("EasyLevels");
		GUI.Button (new Rect (0, 70, 150, 50), "Medium");
			//A/plication.LoadLevel ("DifficultySelectionScene");
		//GUI.Button (new Rect (0, 140, 150, 50), "Hard");
		
		// End the group we started above. This is very important to remember!
		GUI.EndGroup ();
	}
}
