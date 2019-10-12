using UnityEngine;
using System.Collections;

public class AsteroidParticleControls : MonoBehaviour {
	
	public ParticleSystem explosionSystem;
	public float reset;
	
	void Awake() {
		
		//get the rigidbody2d component
		if(explosionSystem == null) {
			explosionSystem = GetComponent<ParticleSystem>();
		}
	}

	void Start() {

		//find out how long the particles play in the inspector
		reset = explosionSystem.duration;
	}


	void OnEnable() {

		//play and auto reset after the duration is over
		explosionSystem.Play();
		Invoke("ResetParticles", reset);
	}

	void ResetParticles() {

		//reset particle system location
		transform.position = new Vector3(0, 0, 0);
		RemoveParticles ();
	}

	void RemoveParticles() {

		//turn off particles
		gameObject.SetActive(false);
	}


	void OnDisable() {
		
		//stop removing particles if it's already gone
		CancelInvoke();
	}
}
