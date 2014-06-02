/*
 * For all audio sources that are music, make sure they have gapless looping enabled.
 * 
 * This script should be attached to the main camera in the main menu.
 * For any music that needs to loop:
 * 		-When you click on the audiosource file in the unity navigator, chage the compression from WAV to MPEG
 * 		-Then, make sure "Gapless Looping" is checked.
 * 
 * Note on the audio sources:
 * 	In the array, the elements should be as follows:
 * 	0 - music intro
 * 	1 - music loop
*/

using UnityEngine;
using System.Collections;

public class SoundMainMenu : MonoBehaviour {

	public AudioClip [] Clips;
	public AudioSource[] audioSources;

	public bool playMusic;
	public bool playSoundFX;

	public bool changedSettings = false; //set to true if any sound settings changed

	private bool playedIntro; //have we played the portion of the music that is not to be looped?
	

	// Use this for initialization
	void Start () {
		//Audio
		audioSources = new AudioSource[Clips.Length];
		for (int i = 0; i < Clips.Length; i++) {
			print(i);
			audioSources[i] = this.gameObject.AddComponent("AudioSource") as AudioSource;
			audioSources[i].clip = Clips[i];
		}
		updatePrefs ();
		changedSettings = false;
		playedIntro = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (changedSettings) {
			updatePrefs ();
		}

		//Music
		if (playMusic && !playedIntro) {
			if (!audioSources[0].isPlaying) {
				audioSources[0].Play();
				playedIntro = true;
			}
		}
		else playedIntro = true; if (playMusic && playedIntro && !audioSources[0].isPlaying) {
			if (!audioSources[1].isPlaying) {
				audioSources[1].Play();
			}
		}
		else if (!playMusic) {
			for (int i = 0; i < 2; i++) { //0 and 1 should be music
				if (audioSources[i].isPlaying) {
					audioSources[i].Stop();
				}
			}
		}
		//End Music


	}

	//if changes are made to player prefs then we need to update sound settings
	void updatePrefs() {
		playMusic = PlayerPrefs.GetInt ("PlayMusic") == 1 ? true : false;
		playSoundFX = PlayerPrefs.GetInt ("PlaySoundFX") == 1 ? true : false;
	}
}
