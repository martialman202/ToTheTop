﻿using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GUISkin menuSkin;
	public Texture2D gameLogo;
	public Texture2D [] stars = new Texture2D[4];
	private float buttonWidth = 0.7f * Screen.width;
	private float buttonHeight = 0.17f * Screen.width;
	private float betweenButton = 0.22f * Screen.width;

	private enum MenuState {Main=0, Options=1, Levels=2, Play=3};
	private MenuState currMenu = MenuState.Main;
	private MenuState prevMenu = MenuState.Main;
	private int levelIndex = 0;

	// Use this for initialization
	void Start () {
		Manager.Instance.levelIndex = 0;
		if(!PlayerPrefs.HasKey("PlaySoundFX"))
			PlayerPrefs.SetInt("PlaySoundFX", 1);
		if(!PlayerPrefs.HasKey("PlayMusic"))
			PlayerPrefs.SetInt("PlayMusic", 1);
	}

	void OnGUI () {
		if(Input.GetKey(KeyCode.Escape))
			Application.Quit();

		GUI.skin = menuSkin;
		// Make a group on the center of the screen

		if(currMenu == MenuState.Main) {
			if(Input.GetKey(KeyCode.Escape))
				Application.Quit();
			else
				DrawMain();
		}
		else if(currMenu == MenuState.Levels)
			DrawLevels();
		else if(currMenu == MenuState.Options)
			DrawOptions();
		/*
		else if(currMenu == MenuState.Play);
		*/
	}

	//Draw the main menu
	void DrawMain() {
		GUI.Label (new Rect (0.05f * Screen.width, 0.01f * Screen.height, 0.9f*Screen.width, 3 * buttonHeight), gameLogo);
		GUI.BeginGroup (new Rect (0.15f * Screen.width, 0.3f * Screen.height, buttonWidth, buttonHeight*6));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
		
		// We'll make a box so you can see where the group is on-screen.
		if (GUI.Button (new Rect (0, 0, buttonWidth, buttonHeight), "Arcade")) {
			prevMenu = currMenu;
			currMenu = MenuState.Levels;
		}
		if (GUI.Button (new Rect (0, betweenButton, buttonWidth, buttonHeight), "Classic"))
			Application.LoadLevel ("MainScene");
		if(GUI.Button(new Rect(0, 2*betweenButton, buttonWidth, buttonHeight), "Options")) {
			prevMenu = currMenu;
			currMenu = MenuState.Options;
		}
		if (GUI.Button (new Rect (0, 3*betweenButton, buttonWidth, buttonHeight), "Exit"))
			Application.Quit ();
		
		// End the group we started above. This is very important to remember!
		GUI.EndGroup ();
	}

	//Draw level selector
	void DrawLevels() {
		GUI.BeginGroup(new Rect(0.1f * Screen.width, 0.1f * Screen.height, 0.9f * Screen.width, 0.8f * Screen.height));

		//Test drawing star labels
		int numPerRow = 3;
		for(int i = 0; i < Manager.Instance.numLevels; i++) {
			int currRow = i / 3;
			string starPoints = "Level" + (i+1).ToString() + "Stars";
			if(PlayerPrefs.HasKey(starPoints)) {
				// Select correct star label
				GUI.Label(new Rect(0.32f * (i % numPerRow) * Screen.width, (currRow * 0.36f) * Screen.width,
				                   0.22f * Screen.width, 0.1f * Screen.width), stars[(int) (PlayerPrefs.GetInt(starPoints))]);
			}
			else {
				//Set the label to 0 and draw
				PlayerPrefs.SetInt(starPoints, 0);
				GUI.Label(new Rect(0.32f * (i % numPerRow) * Screen.width, (currRow * 0.36f) * Screen.width,
				                   0.22f * Screen.width, 0.1f * Screen.width), stars[0]);
			}
		}

		/*
		//Star labels
		GUI.Label(new Rect(0.03f * Screen.width, 0, 0.22f * Screen.width, 0.08f * Screen.width), stars[1]);
		GUI.Label(new Rect(0.31f * Screen.width, 0, 0.22f * Screen.width, 0.1f * Screen.width), stars[2]);
		GUI.Label(new Rect(0.60f * Screen.width, 0, 0.22f * Screen.width, 0.1f * Screen.width), stars[3]);
		*/

		//Draw levels
		for(int i = 0; i < Manager.Instance.numLevels; i++) {
			int currRow = i / 3;
			if(GUI.Button(new Rect(0.29f * (i % numPerRow) * Screen.width, (0.1f + (currRow * 0.36f)) * Screen.width,
			                       0.22f*Screen.width, 0.22f*Screen.width), (i+1).ToString ())) {
				Manager.Instance.levelIndex = i;
				Manager.Instance.levelFileName = Manager.Instance.levels[i];
				Application.LoadLevel("LevelFromFile");
			}
		}

		GUI.EndGroup ();
	}

	//Draw the Options Menu
	void DrawOptions() {
		GUI.Label (new Rect (0.05f * Screen.width, 0.01f * Screen.height, 0.9f*Screen.width, 3 * buttonHeight), gameLogo);
		GUI.BeginGroup (new Rect (0.15f * Screen.width, Screen.height/2 - Screen.width/4, buttonWidth, buttonHeight*4));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
		
		// We'll make a box so you can see where the group is on-screen.
		if(PlayerPrefs.GetInt("PlaySoundFX") == 1) {
			if(GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight), "Sound FX On"))
				PlayerPrefs.SetInt("PlaySoundFX", 0);
		}
		else if(PlayerPrefs.GetInt("PlaySoundFX") == 0) {
			if(GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight), "Sound FX Off"))
				PlayerPrefs.SetInt("PlaySoundFX", 1);
		}

		if(PlayerPrefs.GetInt("PlayMusic") == 1) {
			if(GUI.Button(new Rect(0, betweenButton, buttonWidth, buttonHeight), "Music On"))
				PlayerPrefs.SetInt("PlayMusic", 0);
		}
		else if(PlayerPrefs.GetInt("PlayMusic") == 0) {
			if(GUI.Button(new Rect(0, betweenButton, buttonWidth, buttonHeight), "Music Off"))
				PlayerPrefs.SetInt("PlayMusic", 1);
		}

		if (GUI.Button (new Rect (0, 2*betweenButton, buttonWidth, buttonHeight), "Main Menu")) {
			prevMenu = currMenu;
			currMenu = MenuState.Main;
		}
		
		// End the group we started above. This is very important to remember!
		GUI.EndGroup ();
	}
}