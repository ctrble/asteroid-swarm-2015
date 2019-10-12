using UnityEngine;
using System.Collections;

public class PickupControls : MonoBehaviour {

	public GameObject playerObject;
	public GameObject shieldObject;
	public GameObject gameManager;
	public Vector3 playerTransform;
	public bool goToPlayer = false;
	public float randomNumber;
	public AudioSource audioSource;
	
	public AudioClip pickupHumSound;

	void Awake() {

		//know the player
		if(playerObject == null) {
			playerObject = GameObject.FindWithTag("Player");
			playerTransform = playerObject.transform.position;
		}

		//know the gamemanager, use it to find the shield
		if(gameManager == null) {
			gameManager = GameObject.FindWithTag("GameManager");
		}

		if(audioSource == null) {
			audioSource = GetComponent<AudioSource> ();
		}
	}
	
	void OnEnable() {

		//randomly choose which ammo to give, but it's a 50/50 chance
		randomNumber = Random.Range(0, 100);

		//recycle
		Invoke ("RemovePickup", 5.0F);
	}
	
	void FixedUpdate() {

		//track how far away the player is
		Vector3 offset = playerObject.transform.position - transform.position;
		float sqrLen = offset.sqrMagnitude;
		if(sqrLen < 3F * 3F) {
			goToPlayer = true;
		}

		//player is nearby
		if(goToPlayer == true) {
			MoveToPlayer();	
		}
	}
	
	void MoveToPlayer() {

		//move forward
		transform.Translate(0, 30 * Time.deltaTime, 0);	

		//rotate towards player
		Vector3 vectorToTarget = playerObject.transform.position - transform.position;
		float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90; //adjusted for 2D space
		Quaternion rotationToTarget = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, (90 * 0.25F) * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		//if hitting the player
		if(coll.gameObject.tag == "Player") {
			
			//give ammo and delete itself
			GiveAmmo();
			RemovePickup();

			//audioSource.PlayOneShot (getPickupSound, 1F);
			gameManager.SendMessage("PlayPickupSound");
		}
	}
	
	void GiveAmmo() {

		//randomly choose which ammo to give, but it's a 50/50 chance
		//randomNumber = Random.Range(0, 100);

		if(randomNumber > 33 ) {
			//pick gun 2
			if(randomNumber > 66) {
				//add a small change to drop a weapon unlock/ammo
				BulletGenerator.enabledGun1 = false;
				BulletGenerator.enabledGun2 = true;
				BulletGenerator.enabledGun3 = false;
				BulletGenerator.addTime = true;
			}

			//pick gun 3
			if(randomNumber <= 66) {
				BulletGenerator.enabledGun1 = false;
				BulletGenerator.enabledGun2 = false;
				BulletGenerator.enabledGun3 = true;
				BulletGenerator.addTime = true;
			}
		}

		//pick shields
		if(randomNumber <= 33) {

			//send message to gamemanager to activate the shield
			//GameManager.SendMessage("ActivateShield");

			ShieldManager.enabledShield = true;
			ShieldManager.addTime = true;
		}
	}

	void PlayPickupHumSound() {

		audioSource.PlayOneShot (pickupHumSound, 1F);
	}

	void RemovePickup() {
		
		//turn off pickup
		gameObject.SetActive(false);
	}
	
	void OnDisable() {

		//reset ammo type
		goToPlayer = false;
	}
}
