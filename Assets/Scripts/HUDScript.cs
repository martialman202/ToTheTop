using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {
	public GUISkin menuSkin;
	
	public bool displayLife = true;
	public Texture2D heart;
	public float heartScale = 1.0f;

	public Texture2D pauseTexture;
	private bool paused = false;
	private float buttonWidth = 0.7f * Screen.width;
	private float buttonHeight = 0.17f * Screen.width;
	private float betweenButton = 0.22f * Screen.width;

	private GameObject player;
	private Transform coconut;
	private testAutoMonkey monkeyScript;
	private float heartSize = 16.0f;

	public bool displayWin = false;

	public AudioClip winAudioClip;
	private AudioSource winAudioSource;
	private bool startAudioLoop = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		monkeyScript = player.GetComponent<testAutoMonkey>();

		winAudioSource = this.gameObject.AddComponent ("AudioSource") as AudioSource;
		winAudioSource.clip = winAudioClip;
		winAudioSource.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
		coconut = player.transform.parent.transform.Find ("Coconut");
	}

	void OnGUI()
	{
		GUI.skin = menuSkin;
		
		//Life Display
		float heartD = 0.05f * Screen.width;
		if (displayLife) {
			for (int i = 0; i < monkeyScript.lifePoints; i++) {
				GUI.DrawTexture (new Rect (heartD + (heartD * i), heartD, heartD, heartD), heart);
			}
		}

		//Pause Button
		int originalSize = GUI.skin.button.fontSize;
		int pauseSize = originalSize / 2;
		GUI.skin.button.fontSize = pauseSize;
		if(GUI.Button(new Rect(0.85f * Screen.width, Screen.height - (0.15f * Screen.width), 0.1f * Screen.width, 0.1f * Screen.width), "| |")) {
			if(!paused) paused = true;
			else paused = false;
		}
		GUI.skin.button.fontSize = originalSize;

		if (paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;

		//Win Screen
		if (displayWin) {
			paused = false;
			Time.timeScale = 0;
			GUI.BeginGroup (new Rect (0.15f*Screen.width, Screen.height/2 - Screen.width/4, buttonWidth, buttonHeight*4));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

			//play win audio loop
			if( !startAudioLoop ) {
				startAudioLoop = true;
				winAudioSource.Play();
			}

			// We'll make a box so you can see where the group is on-screen.
			if (Manager.Instance.levelIndex+1 < Manager.Instance.levels.Length) {
				if (GUI.Button (new Rect (0, 0, buttonWidth, buttonHeight), "Next Level")) {
					//paused = false;
					Manager.Instance.levelIndex++;
					Manager.Instance.levelFileName = Manager.Instance.levels[Manager.Instance.levelIndex];
					PlayerPrefs.Save();
					Application.LoadLevel ("LevelFromFile");
				}
			}
			if (GUI.Button (new Rect (0, betweenButton, buttonWidth, buttonHeight), "Play Again")) {
				PlayerPrefs.Save();
				Application.LoadLevel(Manager.Instance.prevLevel);
			}
			if (GUI.Button (new Rect (0, 2*betweenButton, buttonWidth, buttonHeight), "Main Menu")) {
				PlayerPrefs.Save();
				Application.LoadLevel ("TitleScene");
			}
			
			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		}

		//Pause Menu
		if (paused) {
			GUI.Label(new Rect(0.1f*Screen.width, 0.13f * Screen.height, 0.8f*Screen.width, 4* buttonHeight), pauseTexture);
			GUI.BeginGroup (new Rect (0.15f * Screen.width, Screen.height/2 - Screen.width/4, buttonWidth, buttonHeight*4));
				// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
				
				// We'll make a box so you can see where the group is on-screen.
				if (GUI.Button (new Rect (0, 0, buttonWidth, buttonHeight), "Resume")) {
					paused = false;
				}
				if (GUI.Button (new Rect (0, betweenButton, buttonWidth, buttonHeight), "Restart")) {
					Application.LoadLevel(Manager.Instance.prevLevel);
				}
				if (GUI.Button (new Rect (0, 2*betweenButton, buttonWidth, buttonHeight), "Main Menu")) {
					Application.LoadLevel ("TitleScene");
				}
				
			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		}

		//Coconut warning
		//TODO: show only if monkey is under coconut
		if (coconut != null) {
			int warningSize = originalSize*3;
			GUI.skin.label.fontSize = warningSize;
			Color originalColor = GUI.skin.label.normal.textColor;
			GUI.skin.label.normal.textColor = Color.red;
			GUI.Label(new Rect(Screen.width*.45f, Screen.width*.7f, 0.3f*Screen.width, 0.3f*Screen.width), "!");
			GUI.skin.label.fontSize = originalSize;
			GUI.skin.label.normal.textColor = originalColor;
		}

	}
}
