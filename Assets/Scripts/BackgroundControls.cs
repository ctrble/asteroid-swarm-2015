using UnityEngine;
using System.Collections;

public class BackgroundControls : MonoBehaviour {

	//from cameracontrols script to track player
	public Transform target;
	public float smoothTime = 0F;
	public float distance = 0.05F;
	private Transform thisTransform;
	private Vector2 velocity;

	//for the offset
	public float scrollSpeed;
	private Vector2 savedOffset;

	public float currentDirectionX;
	public float currentDirectionY;

	void Start () {
		
		//the background
		thisTransform = transform;

		//original position
		savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset ("_MainTex");
	}

	void FixedUpdate () {

		//the quad follows the player
		Vector3 vector = thisTransform.position;
		vector.x = Mathf.SmoothDamp(thisTransform.position.x, target.position.x, ref velocity.x, smoothTime);
		vector.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y, ref velocity.y, smoothTime);
		thisTransform.position = vector;

		//offset the texture by player speed and direction
		Vector2 offset = vector * distance; //change the float to speed up or slow down the scrolling
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset / 2.5F);
	}

	void OnDisable() {

		//reset the stars
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}
