/*
 * This script should be attached to the main camera in the main menu.
 * 
 * Note on the audio sources:
 * 	In the array, the elements should be as follows:
 * 	0 - music intro
 * 	1 - portion of music to be looped
*/

using UnityEngine;
using System.Collections;

public class SoundMainMenu : MonoBehaviour {

	public AudioClip [] Clips;
	public AudioSource[] audioSources;

	public bool playMusic;
	public bool playSoundFX;

	private bool changedSettings; //set to true if any sound settings changed

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
		/*if (playMusic && !playedIntro) {
			if (!audioSources[0].isPlaying) {
				audioSources[0].Play();
				playedIntro = true;
			}
		}
		else*/ playedIntro = true; if (playMusic && playedIntro && !audioSources[0].isPlaying) {
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
		playSoundFX = PlayerPrefs.GetInt ("PlaySoundEffects") == 1 ? true : false;
	}
}
