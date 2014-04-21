using UnityEngine;
using System.Collections;

public class DestroyObstacle : MonoBehaviour {

<<<<<<< HEAD
	public float offset = 25.0f;
	
	public GameObject mainCam;
	public GameObject thisObstacle;
	
	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		thisObstacle = this.gameObject;
=======
	public float offset = 20.0f;
	
	public GameObject mainCam;

	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
>>>>>>> 871f1820af5437822f2597ffa8fb27be06e81d46
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		if(mainCam.transform.position.y >= thisObstacle.transform.position.y + offset) {
			Destroy(thisObstacle);
		}
=======
		if (mainCam.transform.position.y >= this.gameObject.transform.position.y + offset)
			Destroy (this.gameObject);
>>>>>>> 871f1820af5437822f2597ffa8fb27be06e81d46
	}
}
