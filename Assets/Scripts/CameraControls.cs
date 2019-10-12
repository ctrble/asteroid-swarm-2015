using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.3f;
	private Transform thisTransform;
	private Vector2 velocity;
	public bool stickyCamera = false;

	void Awake () {

		//the camera
		thisTransform = transform;
		stickyCamera = false;
	}
	
	void LateUpdate () {

		//track and follow the player
		/*
		//this must be in FixedUpdate, not LateUpdate
		Vector3 vector = thisTransform.position;
		vector.x = Mathf.SmoothDamp(thisTransform.position.x, (target.position.x), ref velocity.x, smoothTime);
		vector.y = Mathf.SmoothDamp(thisTransform.position.y, (target.position.y), ref velocity.y, smoothTime);
		thisTransform.position = vector;
		*/

		///*
		//directly match the player's movement
		Vector2 targetPosition = target.position;
		thisTransform.position = targetPosition;
		//*/

		if(Input.GetKeyDown("tab")) {
			stickyCamera = !stickyCamera;
		}

		if(stickyCamera == true) {

			//sickening mode
			thisTransform.rotation = target.rotation;
		}

	}
}
