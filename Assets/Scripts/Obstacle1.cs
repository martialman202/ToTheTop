using UnityEngine;
using System.Collections;

public class Obstacle1 : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Debug.Break();
		}
	}
}
