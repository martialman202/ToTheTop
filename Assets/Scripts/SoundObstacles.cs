/*
 *	Attach this to any obstacles that have a sound clip. (Beehive, snake). 
 */

using UnityEngine;
using System.Collections;

public class SoundObstacles : MonoBehaviour {

	public bool playSoundFX;

	// Use this for initialization
	void Start () {
		playSoundFX = PlayerPrefs.GetInt ("PlaySoundEffects") == 1 ? true : false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playSoundFX && !audio.isPlaying) {
			audio.Play();
		}
	}
}
