using UnityEngine;
using System.Collections;

public class HUDScriptMainMenu : MonoBehaviour {

	public enum MenuState {Main=0, Options=1, Play=2};
	MenuState menuState = MenuState.Main;

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("PlaySoundEffects"))
			PlayerPrefs.SetInt ("PlaySoundEffects", 1);

		if (!PlayerPrefs.HasKey ("PlayMusic"))
			PlayerPrefs.SetInt ("PlayMusic", 1);

		GUI.enabled = true; //had bugs earlier, used this to try and fix
	}

	//TODO: make some of these values variables so changing it changes a lot of them

	void OnGUI() {
		//print ("why doesnt this work"); //seriously, why
		// Make a background box
		GUI.Box(new Rect(0,10,400,100), "Logo could prolly go here");

		if (menuState == MenuState.Main) { //main menu
			GUI.Box(new Rect(525,375,200,200), "");

			if (GUI.Button (new Rect (550, 400, 150, 50), "Play")) {
				//load level
			}
			else if (GUI.Button (new Rect (550, 450, 150, 50), "Options")) {
				menuState = MenuState.Options;
			}
			else if (GUI.Button (new Rect (550, 500, 150, 50), "Quit")) {
				menuState = MenuState.Options;
			}

		}//end main menu

		else if (menuState == MenuState.Options) { //options menu
			//get current settings for sounds
			int music = PlayerPrefs.GetInt("PlayMusic");
			int soundfx = PlayerPrefs.GetInt("PlaySoundEffects");
			string playMusic = music == 1 ? "On" : "Off";
			string playSoundFX = soundfx == 1 ? "On" : "Off";
			 
			GUI.Box(new Rect(525,375,200,200), "");

			if (GUI.Button (new Rect (550, 400, 150, 50), "Music: " + playMusic)) {
				music = music == 1 ? 0 : 1; //if music == 1, then set to 0. If 0, set to 1.
				PlayerPrefs.SetInt("PlayMusic", music);
			}
			else if (GUI.Button (new Rect (550, 450, 150, 50), "Sound Effects: " + playSoundFX)) {
				soundfx = soundfx == 1 ? 0 : 1; //if soundfx == 1, then set to 0. If 0, set to 1.
				PlayerPrefs.SetInt("PlaySoundEffects", soundfx);
			}
			else if (GUI.Button (new Rect (550, 500, 150, 50), "Back")) {
				menuState = MenuState.Main;
			}
		} //end options menu

		else if (menuState == MenuState.Play) { //play menu
			//then have a menu to display the different game modes
			//make a function to switch. Makes the code cleaner.

		} //end play menu
	}

	void switchLevel(int whichLevel) {
		PlayerPrefs.Save (); //we do this at the end because writing to disk can be slow
	}

}
