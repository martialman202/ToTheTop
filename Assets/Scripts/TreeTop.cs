using UnityEngine;
using System.Collections;

public class TreeTop : MonoBehaviour {
	private bool spawned = false;
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Tree") {
			Destroy(other.gameObject);
			if(!spawned)
				this.transform.Translate(new Vector3(0,-1,0));
			spawned = true;
		}
	}
}
