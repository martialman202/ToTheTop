using UnityEngine;
using System.Collections;

public class CollisionWPlayer : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Debug.Break ();
			//Application.LoadLevel (Application.loadedLevelName); //Change scene
		}
	}
}
