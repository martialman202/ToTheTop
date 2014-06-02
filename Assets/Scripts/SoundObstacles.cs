/*
 *	Attach this to any obstacles that have a sound clip. (Beehive, snake). 
 */

using UnityEngine;
using System.Collections;

public class SoundObstacles : MonoBehaviour {

	public bool playSoundFX;
	public float volume = 0.8f;
	public int priority = 128;

	// Use this for initialization
	void Start () {
		playSoundFX = PlayerPrefs.GetInt ("PlaySoundFX") == 1 ? true : false;
		audio.volume = volume;
		audio.priority = priority;
	}
	
	// Update is called once per frame
	void Update () { 
		//looping should be set to true, but if it isnt, this takes care of it anyways
		if (playSoundFX && !audio.isPlaying) {
			audio.Play();
		}
		else if (!playSoundFX) {
			if (audio.isPlaying)
				audio.Stop();
		}

	}
}
