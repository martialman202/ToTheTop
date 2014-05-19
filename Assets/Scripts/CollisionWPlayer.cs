using UnityEngine;
using System.Collections;
using UnityEditor;

public class CollisionWPlayer : MonoBehaviour {

	private Animation animation;
	private ParticleSystem ps;
	private GameObject monkey;

	private Transform[] bees;

	private int obstacleID;

	void Start() {
		monkey = GameObject.FindGameObjectWithTag("Player");
		string thisObstacle = this.name;

		if( thisObstacle == "ReducedCartoonBeehive" ) {
			obstacleID = 0;
			bees = new Transform[2];
			bees[0] = this.transform.GetChild(0);
			bees[1] = this.transform.GetChild(2);
		}
		else if( thisObstacle == "CartoonSnakePrefab" ) {
			obstacleID = 1;
			animation = this.GetComponent<Animation> ();
		}
	}

	void Update() {
		if( obstacleID == 0 && monkey.transform.position.y >= this.transform.position.y - 12.0f ) {
			bees[0].Rotate(new Vector3(90.0f*Time.deltaTime,360.0f*Time.deltaTime,90.0f*Time.deltaTime));
			bees[1].Rotate(new Vector3(90.0f*Time.deltaTime,-360.0f*Time.deltaTime,90.0f*Time.deltaTime));
			/*
			ParticleSystem.Particle[] p = new ParticleSystem.Particle[ps.particleCount+1];
			int l = ps.GetParticles(p);
			
			int i = 0;
			while (i < l) {
				p[i].velocity
				i++;
			}
			
			ps.SetParticles(p, l);
			*/
		}
		else if( obstacleID == 1 && monkey.transform.position.y >= this.transform.position.y - 7.0f ) {
			if( animation != null ) animation.CrossFade("attack",0.2f);
		}
	}
}
