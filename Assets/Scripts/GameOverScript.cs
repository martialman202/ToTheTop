using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	private float buttonHeight = 50.0f;

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {
		GUI.BeginGroup (new Rect (Screen.width/2 - Screen.width/4, Screen.height/2 - Screen.width/4, Screen.width/2, 300));
			GUI.Label(new Rect(Screen.width/8, 0, Screen.width/2, 30), "GAME OVER");
			if(GUI.Button(new Rect(0, 50, Screen.width/2, buttonHeight), "Play again?"))
			{
				Application.LoadLevel(PlayerPrefs.GetInt("previousLevel"));
			}
			if(GUI.Button(new Rect(0, 125, Screen.width/2, buttonHeight), "Main Menu"))
			{
				Application.LoadLevel("TitleScene");
			}
		GUI.EndGroup ();
	}

}
