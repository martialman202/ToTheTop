using UnityEngine;
using System.Collections;

public class CollisionWPlayer : MonoBehaviour {

	Animation animation;

	void Start() {
		animation = this.GetComponent<Animation> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			//Debug.Break ();
			//Application.LoadLevel (Application.loadedLevelName); //Change scene
			if (animation != null) animation.Play("attack");
		}
	}
}
