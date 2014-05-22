using UnityEngine;
using System.Collections;

public class Coconut : MonoBehaviour {
	public float speed = 10.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.down * speed * Time.deltaTime);
	}
}
