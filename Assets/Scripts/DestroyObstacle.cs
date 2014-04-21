using UnityEngine;
using System.Collections;

public class DestroyObstacle : MonoBehaviour {

<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 52e416925498aae40c42c4ad390f2ebedeaf596d
	public float offset = 25.0f;
	
	public GameObject mainCam;
	public GameObject thisObstacle;
	
	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		thisObstacle = this.gameObject;
<<<<<<< HEAD
=======
	public float offset = 20.0f;
	
	public GameObject mainCam;

	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
>>>>>>> 871f1820af5437822f2597ffa8fb27be06e81d46
=======
>>>>>>> 52e416925498aae40c42c4ad390f2ebedeaf596d
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
<<<<<<< HEAD
		if(mainCam.transform.position.y >= thisObstacle.transform.position.y + offset) {
			Destroy(thisObstacle);
		}
=======
		if (mainCam.transform.position.y >= this.gameObject.transform.position.y + offset)
			Destroy (this.gameObject);
>>>>>>> 871f1820af5437822f2597ffa8fb27be06e81d46
=======
		if(mainCam.transform.position.y >= thisObstacle.transform.position.y + offset) {
			Destroy(thisObstacle);
		}
>>>>>>> 52e416925498aae40c42c4ad390f2ebedeaf596d
	}
}
