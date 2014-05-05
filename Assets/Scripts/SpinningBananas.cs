using UnityEngine;
using System.Collections;

public class SpinningBananas : MonoBehaviour {

	public float rotateSpeed = 30.0f;

	private GameObject bananas;

	// Use this for initialization
	void Start () {
		bananas = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		bananas.transform.Rotate (Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
	}
}
