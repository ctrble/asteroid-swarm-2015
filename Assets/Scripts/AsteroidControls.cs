using UnityEngine;
using System.Collections;

public class AsteroidControls : MonoBehaviour {

	public Vector2 asteroidVelocity;
	public float randomNumber;
	public float adjustedNumber;
	public bool freshAsteroid = true;
	public Rigidbody2D rigidBody2D;

	void Awake() {

		//get the rigidbody2d component
		if(rigidBody2D == null) {
			rigidBody2D = GetComponent<Rigidbody2D>();
		}

		//at start of game, all asteroids are new
		freshAsteroid = true;
	}

	void OnEnable() {

		//invisible asteroid
		gameObject.tag = "IgnoreAsteroid";

		//needs to be here so number can be reset
		randomNumber = Random.Range(1F, 100F);

		//random asteroid size
		GenerateMass();

		//random asteroid movement
		GenerateDirection();
	}

	void OnDisable() {

		//invisible asteroid
		gameObject.tag = "IgnoreAsteroid";
	}

	void NotFreshAsteroid() {
		freshAsteroid = false;
	}

	void GenerateMass() {

		if(freshAsteroid == true){

			//pick size of asteroid
			if(randomNumber > 90) {

				//probably not gonna happen
				if(randomNumber >= 99.9) {
					adjustedNumber = Random.Range(7F, 10F);
				}

				//not likely
				if(randomNumber < 99.9) {
					adjustedNumber = Random.Range(2.5F, 3F);
				}
			}

			if(randomNumber <= 90) {

				//kinda likely
				if(randomNumber < 60) {
					adjustedNumber = Random.Range(1.5F, 2F);
				}

				//most likely
				if(randomNumber <= 60) {
					adjustedNumber = Random.Range(1F, 1.5F);
				}
			}

			//size based off random number
			rigidBody2D.mass = adjustedNumber;
			rigidBody2D.centerOfMass = new Vector3(Random.Range(-adjustedNumber * 0.5F, adjustedNumber * 0.5F), Random.Range(-adjustedNumber * 0.5F, adjustedNumber * 0.5F));
			transform.localScale = new Vector3(adjustedNumber, adjustedNumber, adjustedNumber);
		}

		if(freshAsteroid == false){

			//reset back to default
			freshAsteroid = true;
		}
	}

	void GenerateDirection() {

		if(freshAsteroid == true){

			//pick a direction and rotation
			asteroidVelocity = new Vector2(Random.Range(-15F, 15F), Random.Range(-15F, 15F));
			rigidBody2D.AddRelativeForce(asteroidVelocity * adjustedNumber, ForceMode2D.Impulse);
			rigidBody2D.AddTorque(Random.Range(-180F, 180F));
		}

		if(freshAsteroid == false){
			
			//reset back to default
			freshAsteroid = true;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		//off screen asteroids don't count for homing missiles
		//is toggle
		if (coll.gameObject.tag == "BulletZone") {
			gameObject.tag = "Asteroid";
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		
		//off screen asteroids don't count for homing missiles
		//is toggle
		if (coll.gameObject.tag == "BulletZone") {
			gameObject.tag = "IgnoreAsteroid";
		}
	}
}
