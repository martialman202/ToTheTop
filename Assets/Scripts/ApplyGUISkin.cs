using UnityEngine;
using System.Collections;

public class ApplyGUISkin : MonoBehaviour {

	public GUISkin customSkin;

	void OnGUI() {
		GUI.skin = customSkin;
	}
}
