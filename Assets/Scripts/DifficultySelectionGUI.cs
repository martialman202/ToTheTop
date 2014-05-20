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
		// Grid level selection
		GUI.BeginGroup(new Rect(0.1f * Screen.width, Screen.height/2 - Screen.width/4, 0.9f * Screen.width, 3 * buttonHeight));
		//Star labels
		GUI.Label(new Rect(0.02f * Screen.width, 0, 0.22f * Screen.width, 0.08f * Screen.width), stars[1]);
		GUI.Label(new Rect(0.31f * Screen.width, 0, 0.22f * Screen.width, 0.1f * Screen.width), stars[2]);
		GUI.Label(new Rect(0.60f * Screen.width, 0, 0.22f * Screen.width, 0.1f * Screen.width), stars[3]);

		//Level buttons
		if (GUI.Button (new Rect (0, 0.1f * Screen.width, 0.22f * Screen.width, 0.22f * Screen.width), "1")) {
			// Tell Manager what level to load
			Manager.Instance.levelIndex = 0;
			Manager.Instance.levelFileName = Manager.Instance.levels[Manager.Instance.levelIndex];
			Application.LoadLevel ("LevelFromFile");
		}
		if(GUI.Button(new Rect(0.29f * Screen.width, 0.1f * Screen.width, 0.22f * Screen.width, 0.22f * Screen.width), "2")) {
			// Tell Manager what level to load
			Manager.Instance.levelIndex = 5;
			Manager.Instance.levelFileName = Manager.Instance.levels[Manager.Instance.levelIndex];
			Application.LoadLevel ("LevelFromFile");
		}
		if(GUI.Button(new Rect(0.58f * Screen.width, 0.1f * Screen.width, 0.22f * Screen.width, 0.22f * Screen.width), "3")) {
			// Tell Manager what level to load
			Manager.Instance.levelIndex = 9;
			Manager.Instance.levelFileName = Manager.Instance.levels[Manager.Instance.levelIndex];
			Application.LoadLevel ("LevelFromFile");
		}
		GUI.EndGroup ();
	}
}
