using UnityEngine;
using System.Collections;

public class GravityControls : MonoBehaviour {

	public Rigidbody2D rigidBody2D;
	public CircleCollider2D circleCollider2D;
	public PointEffector2D pointEffector2D;
	public float radius;
	public float intensity;

	
	void Awake() {

		//get the rigidbody2d component
		if(rigidBody2D == null) {
			rigidBody2D = GetComponentInParent<Rigidbody2D>();
		}

		//get the circlecollider2d component
		if(circleCollider2D == null) {
			circleCollider2D = GetComponent<CircleCollider2D>();
		}

		//get the pointeffector2d component
		if(pointEffector2D == null) {
			pointEffector2D = GetComponent<PointEffector2D>();
		}
	}

	void OnEnable() {

		//what's the mass?
		intensity = rigidBody2D.mass;

		//what's the range
		circleCollider2D.radius = (intensity * 1F);
		radius = circleCollider2D.radius;

		//how strong is it?
		pointEffector2D.forceMagnitude = -(intensity * 1F);
	}
}
