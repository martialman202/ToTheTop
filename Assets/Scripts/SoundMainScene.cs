/*
 * This script should be attached to the main camera in scenes with gameplay.
 * 
 * Note on the audio sources:
 * 	In the array, the elements should be as follows:
 * 	0 - main music loop
 * 	1 - win sound 	--classified as sound effect
 * 	2 - lose sound 	--classified as sound effect
 * 	3 - monkeyHurt
 * 	4 - monkeyJump
 * 	5 - main music intro 
*/

using UnityEngine;
using System.Collections;

public class SoundMainScene : MonoBehaviour {

	public bool playMusic = true;
	public float musicVolume = 1.0f; //0.0 - 1.0
	public bool playSoundEffects = true;
	public AudioClip [] Clips;
	public AudioSource[] audioSources;

	public bool changedSettings; //this is set by another script if player prefs change

	private bool playedIntro;
	private bool playedLose;
	private bool playedWin;

	// Use this for initialization
	void Start () {
		//Audio
		audioSources = new AudioSource[Clips.Length];
		for (int i = 0; i < Clips.Length; i++) {
			audioSources[i] = this.gameObject.AddComponent("AudioSource") as AudioSource;
			audioSources[i].clip = Clips[i];
		}
		changedSettings = false;
		audioSources [0].priority = 0; //Set music to highest priority
		audioSources [0].volume = musicVolume; //turn it down a little bit
		audioSources [5].priority = 0;
		audioSources [5].volume = musicVolume;

		//raise priority of win/lose sounds
		audioSources [1].priority = 1;
		updatePrefs ();
		audioSources [2].priority = 1;
		//End Audio

		playedLose = false;
		playedWin = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (changedSettings) {
		//	updatePrefs();
		}

		if (playMusic && !playedIntro) {
			if (!audioSources[5].isPlaying) {
				audioSources[5].Play();
				playedIntro = true;
			}
		}
		else if (playMusic && playedIntro && (!audioSources [5].isPlaying)) {
			if (!audioSources[0].isPlaying) {
				audioSources[0].Play();
			}
		}
		else if (!playMusic) {
			if (audioSources[0].isPlaying)
				audioSources[0].Stop ();
			if (audioSources[5].isPlaying)
				audioSources[5].Stop();
		}
	}

	void updatePrefs() {
		playMusic = PlayerPrefs.GetInt ("PlayMusic") == 1 ? true : false;
		playSoundEffects = PlayerPrefs.GetInt ("PlaySoundFX") == 1 ? true : false;
	}
}
