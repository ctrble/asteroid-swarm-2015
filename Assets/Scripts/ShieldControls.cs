using UnityEngine;
using System.Collections;

public class ShieldControls : MonoBehaviour {

	public Animator animator;
	public BoxCollider2D boxCollider2D;
	public Transform shield;
	public Transform target;
	public Vector3 shipPosition;
	public Vector3 shieldRotation;
	private Transform thisTransform;

	public AudioSource audioSource;
	
	public AudioClip shieldHumSound;

	void Awake() {
		
		//get the animator component
		if(animator == null) {
			animator = GetComponent<Animator>();
		}

		//get the boxCollider2D component
		if(boxCollider2D == null) {
			boxCollider2D = GetComponent<BoxCollider2D>();
		}

		if(audioSource == null) {
			audioSource = GetComponent<AudioSource> ();
		}
			
		//the shield
		thisTransform = transform;
	}

	void LateUpdate() {

		//rotate around the player
		shipPosition = target.position;
		transform.TransformPoint(shipPosition);
		transform.RotateAround(shipPosition, Vector3.forward, 300 * Time.deltaTime);
		thisTransform.position = shipPosition;
	}

	void PlayShieldHumSound() {

		audioSource.PlayOneShot (shieldHumSound, 1F);
	}

}
