using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {
		GUI.Label(new Rect(Screen.width/2 - 40, 50, 80, 30), "GAME OVER");
		if(GUI.Button(new Rect(Screen.width/2 - 50, 100, 100, 30), "Play again?"))
		{
			Application.LoadLevel(1);
		}
	}

}
