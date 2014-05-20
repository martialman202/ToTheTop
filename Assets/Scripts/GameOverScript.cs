using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public GUISkin menuSkin;
	public Texture2D gameOver;
	private float buttonHeight = Screen.width/6;
	private float betweenButton = Screen.width/4;

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {
		GUI.skin = menuSkin;
		GUI.Label (new Rect (0.08f * Screen.width, Screen.width / 8, 0.84f * Screen.width, 3 * buttonHeight), gameOver);
		GUI.BeginGroup (new Rect (Screen.width/4, Screen.height/2 - Screen.width/4, Screen.width/2, buttonHeight*4));
			if(GUI.Button(new Rect(0, 0, Screen.width/2, buttonHeight), "Play again?"))
			{
				Application.LoadLevel(Manager.Instance.prevLevel);
			}
			if(GUI.Button(new Rect(0, betweenButton, Screen.width/2, buttonHeight), "Main Menu"))
			{
				Application.LoadLevel("TitleScene");
			}
		GUI.EndGroup ();
	}

}
