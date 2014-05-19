/*
 * This script should be attached to the main camera in scenes with gameplay.
 * 
 * Note on the audio sources:
 * 	In the array, the elements should be as follows:
 * 	0 - main music
 * 	1 - win music 	--classified as sound effect
 * 	2 - lose music 	--classified as sound effect
 * 	3 - monkeyHurt
 * 	4 - monkeyJump
*/

using UnityEngine;
using System.Collections;

public class SoundMainScene : MonoBehaviour {

	public bool playMusic = true;
	public float musicVolume = 0.7f; //0.0 - 1.0
	public bool playSoundEffects = true;
	public AudioClip [] Clips;
	public AudioSource[] audioSources;

	public bool changedSettings; //this is set by another script if player prefs change

	private bool playedLose;
	private bool playedWin;

	// Use this for initialization
	void Start () {
		//Audio
		audioSources = new AudioSource[Clips.Length];
		for (int i = 0; i < Clips.Length; i++) {
			print(i);
			audioSources[i] = this.gameObject.AddComponent("AudioSource") as AudioSource;
			audioSources[i].clip = Clips[i];
		}
		changedSettings = false;
		audioSources [0].priority = 0; //Set music to highest priority
		audioSources [0].volume = musicVolume; //turn it down a little bit

		updatePrefs ();
		playedLose = false;
		playedWin = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (changedSettings) {
			updatePrefs();
		}

		if (playMusic && !(audioSources [0].isPlaying)) {
			audioSources[0].Play();
		}
		else if (!playMusic) {
			if (audioSources[0].isPlaying)
				audioSources[0].Stop ();
		}
	}

	void updatePrefs() {
		playMusic = PlayerPrefs.GetInt ("PlayMusic") == 1 ? true : false;
		playSoundEffects = PlayerPrefs.GetInt ("PlaySoundEffects") == 1 ? true : false;
	}
}
