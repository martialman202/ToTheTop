/*
 * This script should be attached to the main camera.
 * 
 * Note on the audio sources:
 * 	In the array, the elements should be as follows:
 * 	0 - main music
 * 	1 - win music
 * 	2 - lose music
*/

using UnityEngine;
using System.Collections;

public class SoundMainScene : MonoBehaviour {

	public bool playMusic = true;
	public AudioClip [] Clips;
	private AudioSource[] audioSources;

	// Use this for initialization
	void Start () {
		//Audio
		audioSources = new AudioSource[Clips.Length];
		for (int i = 0; i < Clips.Length; i++) {
			print(i);
			audioSources[i] = this.gameObject.AddComponent("AudioSource") as AudioSource;
			audioSources[i].clip = Clips[i];
		//end Audio
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (playMusic && !(AudioSource [0].isPlaying())) {
			AudioSource.Play();
		}
	}
}
