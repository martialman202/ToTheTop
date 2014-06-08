using UnityEngine;
using System.Collections;

public class CollisionWPlayer : MonoBehaviour {

	private Animation animation;
	private ParticleSystem ps;
	private GameObject monkey;

	private Transform[] bees;

	private int obstacleID;

	void Start() {
		monkey = GameObject.FindGameObjectWithTag("Player");
		string thisObstacle = this.name;

		if( thisObstacle == "Model_Beehive" ) {
			obstacleID = 0;
			bees = new Transform[2];
			bees[0] = this.transform.GetChild(0);
			bees[1] = this.transform.GetChild(2);
		}
		else if( thisObstacle == "Model_Snake" ) {
			obstacleID = 1;
			animation = this.GetComponent<Animation> ();
		}
	}

	void Update() {
		if( obstacleID == 0 && monkey.transform.position.y >= this.transform.position.y + 15.0f ) {
			bees[0].particleSystem.enableEmission = false;
			bees[1].particleSystem.enableEmission = false;
		}
		else if( obstacleID == 0 && monkey.transform.position.y >= this.transform.position.y - 12.0f ) {
			bees[0].Rotate(new Vector3(90.0f*Time.deltaTime,360.0f*Time.deltaTime,90.0f*Time.deltaTime));
			bees[1].Rotate(new Vector3(90.0f*Time.deltaTime,-360.0f*Time.deltaTime,90.0f*Time.deltaTime));
		}
		else if( obstacleID == 1 && monkey.transform.position.y >= this.transform.position.y + 15.0f ) {
			if( animation != null ) animation.Stop();
		}
		else if( obstacleID == 1 && monkey.transform.position.y >= this.transform.position.y - 7.0f ) {
			if( animation != null ) animation.CrossFade("attack",0.2f);
		}
	}
}
