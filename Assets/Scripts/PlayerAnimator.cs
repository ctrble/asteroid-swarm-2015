using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

	//the animator
	public Animator animator;

	public GameObject engine1;
	public GameObject engine2;

	void Start() {
		
		//get the animator
		if (animator == null) {
			animator = GetComponent<Animator> ();
		}	
	}

	void KillPlayer() {

		animator.SetTrigger ("killPlayer");
		engine1.SetActive (false);
		engine2.SetActive (false);
		PlayerControls.playerIsDead = true;
	}

	IEnumerator GameOver() {

		yield return new WaitForSeconds (2.5F);

		Application.LoadLevel("Game Over");
	}
}
