using UnityEngine;
using System.Collections;

public class PlayerAudioControls : MonoBehaviour {
	
	public AudioSource audioSource;
	
	public AudioClip engineStartupSound;
	public AudioClip engineHumSound;

	void Start() {
		
		if(audioSource == null) {
			audioSource = GetComponent<AudioSource> ();
		}

		PlayEngineStartSound ();
	}

	void Update() {

		if (PlayerControls.playerIsDead == true) {

			audioSource.Stop();
		}
	}

	void PlayEngineStartSound() {
		
		audioSource.PlayOneShot (engineStartupSound, 1F);
		PlayEngineHumSound ();
	}

	void PlayEngineHumSound() {

		audioSource.volume = 0.1F;
		audioSource.PlayDelayed (1F);
	}
}
