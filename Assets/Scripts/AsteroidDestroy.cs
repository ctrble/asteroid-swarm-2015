using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidDestroy : MonoBehaviour {

	public int pooledAmount = 1;
	public GameObject pickup;
	public List<GameObject> pickups;
	public float randomNumber;
	public int asteroidHealth;
	public Rigidbody2D rigidBody2D;
	public GameObject gameManager;
	public Vector3 asteroidPosition;
	public Vector2 collisionPosition;
	public int startingHealth;
	public float asteroidMass;
	public static bool playerCanDie = true;
	//public AudioClip playerDeadSound;
	public AudioClip asteroidBumpSound;
	public AudioClip shieldBumpSound;
	public AudioSource audioSource;
	public Animator animator;
	public bool asteroidIsDead;
	public PolygonCollider2D polygonCollider2D;

	void Awake() {
		
		//get the rigidbody2d component
		if(rigidBody2D == null) {
			rigidBody2D = GetComponent<Rigidbody2D>();
		}

		if(audioSource == null) {
			audioSource = GetComponent<AudioSource> ();
		}

		//get the gamemanager object
		if(gameManager == null) {
			gameManager = GameObject.Find("GameManager");
		}

		//get the animator
		if (animator == null) {
			animator = GetComponent<Animator> ();
		}	

		if(polygonCollider2D == null) {
			polygonCollider2D = GetComponent<PolygonCollider2D>();
		}

		playerCanDie = true;
		asteroidIsDead = false;
	}
	
	void Start() {

		//object pool for pickup
		pickups = new List<GameObject> ();
		for(int i = 0; i < pooledAmount; i++) {
			GameObject obj = (GameObject)Instantiate(pickup);
			obj.GetComponent<PickupControls>().enabled = true;
			obj.SetActive(false);
			pickups.Add(obj);
		}

		playerCanDie = true;
	}

	void OnEnable() {

		//randomly choose whether to give a pickup
		randomNumber = Random.Range(1, 100);

		polygonCollider2D.enabled = true;

		//health is based off mass
		asteroidHealth = Mathf.RoundToInt(rigidBody2D.mass);
		if (asteroidHealth == 0) {
			asteroidHealth = 1;	//make sure nothing has 0 health
		}

		//make asteroids twice as strong
		startingHealth = 0;
		//asteroidHealth = asteroidHealth * 2;
		startingHealth = asteroidHealth;
	}

	void Update() {

		//always monitor asteroid health
		if (asteroidIsDead == false) {
			HealthTracker();	
		}

	}

	void HealthTracker() {

		//die at zero health
		if(asteroidHealth <= 0) {

			asteroidIsDead = true;
			polygonCollider2D.enabled = false;
			KillAsteroid();
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		
		//kills asteroids and gives points when shot
		if(coll.gameObject.tag == "Bullet") {

			//audioSource.PlayOneShot (hitAsteroidSound, 1F);
			gameManager.SendMessage("PlayHitAsteroid");

			//also removes the bullet
			coll.gameObject.SetActive(false);

			//hurts
			asteroidHealth = asteroidHealth - 1;

			//create particle system at hit location
			ContactPoint2D contact = coll.contacts[0];
			asteroidPosition = contact.point;

			//trigger particle system
			Explosion();
			//StartCoroutine("Explosion");
		}

		if(coll.gameObject.tag == "Asteroid") {

			audioSource.PlayOneShot(asteroidBumpSound, 0.1F);
		
		}

		if(coll.gameObject.tag == "Shield") {
			
			audioSource.PlayOneShot(shieldBumpSound, 0.1F);
			
		}

		if(coll.gameObject.tag == "Player" && PlayerControls.playerIsDead == false) {

			if (playerCanDie == true) {
				coll.gameObject.SendMessage("KillPlayer");
				//audioSource.PlayOneShot(playerDeadSound, 1F);
				gameManager.SendMessage("PlayPlayerDeadSound");
			}

			if (playerCanDie == false) {
				return;
			}
		}
	}	

	void KillAsteroid() {

		//get points based on how big the asteroid is
		//GUIManager.currentScore = GUIManager.currentScore + startingHealth * 2;
		//GUIManager.SetHighScore();
		int addedPoints = startingHealth * 2;
		GUIManager.AddPoint(addedPoints);
		
		//random chance to instantiate the pickup
		//91 is the default
		if(randomNumber >= 91) {
			CreatePickup();
		}

		//get rid of far away asteroids
		//RemoveAsteroid();
		
		//split asteroid
		TriggerBreak();
		
		//trigger particle system death explosion
		Explosion();
		//StartCoroutine("Explosion");
	}

	void OnTriggerExit2D(Collider2D other) {

		//get rid of far away asteroids
		if(other.gameObject.tag == "MainCamera") {
			RemoveAsteroid();
		}
	}

	void CreatePickup() {

		//activate a pickup object where the asteroid was
		for(int i = 0; i < pickups.Count; i++) {
			if(!pickups[i].activeInHierarchy) {
				pickups[i].transform.position = transform.position;
				pickups[i].transform.rotation = transform.rotation;
				pickups[i].SetActive(true);	
				break;
			}
		}
	}

	void TriggerBreak() {

		//asteroid splitter
		if(startingHealth >= 3) {
			
			//split asteroid
			BreakAsteroid();
			
			if(startingHealth >= 4) {
				
				//split asteroid in two
				BreakAsteroid();
			}
		}
	}

	void BreakAsteroid() {

		//make another asteroid in place of the dead one
		gameManager.SendMessage("SetMass", startingHealth);
		gameManager.SendMessage("CreateBrokenAsteroids", asteroidPosition);
	}

	void Explosion() {

		//yield return new WaitForSeconds (0.667F);

		animator.SetTrigger ("killAsteroid");

		//make a pooled particle system
		gameManager.SendMessage("CreateExplosion", asteroidPosition);
	}

	void RemoveAsteroid() {
		
		//turn off asteroid
		gameObject.SetActive(false);
		//StartCoroutine ("DelayRemove");
	}

}
