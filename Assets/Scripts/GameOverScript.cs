using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public GUISkin menuSkin;
	public Texture2D gameOver;
	private float buttonWidth = 0.7f * Screen.width;
	private float buttonHeight = 0.17f * Screen.width;
	private float betweenButton = 0.22f * Screen.width;

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {
		GUI.skin = menuSkin;
		GUI.Label (new Rect (0.08f * Screen.width, Screen.width / 8, 0.84f * Screen.width, 3 * buttonHeight), gameOver);
		GUI.BeginGroup (new Rect (0.15f * Screen.width, Screen.height/2 - Screen.width/4, buttonWidth, buttonHeight*4));
			if(GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight), "Play again?"))
			{
				PlayerPrefs.Save();
				Application.LoadLevel(Manager.Instance.prevLevel);
			}
			if(GUI.Button(new Rect(0, betweenButton, buttonWidth, buttonHeight), "Main Menu"))
			{
				PlayerPrefs.Save();
				Application.LoadLevel("TitleScene");
			}
		GUI.EndGroup ();
	}

}
