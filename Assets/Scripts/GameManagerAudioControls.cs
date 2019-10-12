using UnityEngine;
using System.Collections;

public class GameManagerAudioControls : MonoBehaviour {

	public AudioSource audioSource;
	
	public AudioClip hitAsteroidSound;
	public AudioClip getPickupSound;
	public AudioClip playerDeadSound;
	
	void Start() {
		
		if(audioSource == null) {
			audioSource = GetComponent<AudioSource> ();
		}		
	}

	void PlayHitAsteroid() {

		audioSource.PlayOneShot (hitAsteroidSound, 0.2F);
	}

	void PlayPickupSound() {

		audioSource.PlayOneShot (getPickupSound, 0.3F);
	}

	void PlayPlayerDeadSound() {

		audioSource.PlayOneShot(playerDeadSound, 1F);
	}
}
