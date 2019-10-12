using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet3 : MonoBehaviour {

	public float speed = 30;
	public Transform target;
	public bool hasTarget = false;

	void OnEnable() {

		//reminder to forget asteroids
		hasTarget = false;
		target = null;
	}

	void OnDisable() {

		//forget any previous asteroids
		hasTarget = false;
		target = null;
	}

	void FixedUpdate() {

		//bullet speed!
		transform.Translate(0, speed * Time.deltaTime, 0);	

		///*
		//get a target
		FindClosestAsteroid();
		//*/

		//when there's a target...
		if(hasTarget == true) {

			//rotate the bullet towards the target
			Vector3 vectorToTarget = target.position - transform.position;
			float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90; //adjusted for 2D space
			Quaternion rotationToTarget = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, (speed * 0.1F) * Time.deltaTime);
		}
	}

	void FindClosestAsteroid() {

		//cycle through all active asteroids and find the closest one on screen
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Asteroid");
		GameObject closest = null;
		hasTarget = false;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach(GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if(curDistance < distance) {
				closest = go;
				distance = curDistance;
				hasTarget = true;
				target = closest.transform;
			}
		}
	}
}
