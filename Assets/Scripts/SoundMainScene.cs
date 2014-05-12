/*
 * This script should be attached to the main camera.
 * 
 * Note on the audio sources:
 * 	In the array, the elements should be as follows:
 * 	0 - main music
 * 	1 - win music 
 * 	2 - lose music
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

	// Use this for initialization
	void Start () {
		//Audio
		audioSources = new AudioSource[Clips.Length];
		for (int i = 0; i < Clips.Length; i++) {
			print(i);
			audioSources[i] = this.gameObject.AddComponent("AudioSource") as AudioSource;
			audioSources[i].clip = Clips[i];
		}
		audioSources [0].priority = 0; //Set music to highest priority
		audioSources [0].volume = musicVolume; //turn it down a little bit
	}
	
	// Update is called once per frame
	void Update () {
		if (playMusic && !(audioSources [0].isPlaying)) {
			audioSources[0].Play();
		}
		else if (!playMusic) {
			if (audioSources[0].isPlaying)
				audioSources[0].Stop ();
		}
	}
}
