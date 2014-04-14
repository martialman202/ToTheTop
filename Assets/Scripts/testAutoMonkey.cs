using UnityEngine;
using System.Collections;

public class testAutoMonkey : MonoBehaviour {

	public Vector3 moveDirection = new Vector3(0,0,1);
	public float moveSpeed = 2.0f;
	public float rotationSpeed = 0.0f;

	// Use this for initialization
	void Start () {
		print (gameObject.name + " has been started.");
	}

	void OnCollisionEnter(Collision other)
	{
		print(gameObject.name + " hit " + other.gameObject.name);
		if (other.gameObject.tag == "Tree")
			moveDirection = new Vector3(0,1,0);

	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
	}
}
