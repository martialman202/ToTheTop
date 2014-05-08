using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {
		GUI.BeginGroup (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 300));
			GUI.Label(new Rect(0, 0, 100, 30), "GAME OVER");
			if(GUI.Button(new Rect(0, 50, 100, 30), "Play again?"))
			{
				Application.LoadLevel(PlayerPrefs.GetInt("previousLevel"));
			}
			if(GUI.Button(new Rect(0, 100, 100, 30), "Main Menu"))
			{
				Application.LoadLevel("TitleScene");
			}
		GUI.EndGroup ();
	}

}
