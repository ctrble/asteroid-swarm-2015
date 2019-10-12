using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionManager : MonoBehaviour {

	public int pooledAmount = 10;
	public GameObject explosion;
	public List<GameObject> explosions;
	public Vector3 asteroidPosition;
	public int startingHealth;
	public float newMass;

	public GameObject[] asteroidArray;
	public int pooledAmount1 = 3;
	public int pooledAmount2 = 3;
	public int pooledAmount3 = 3;
	List<GameObject> asteroids1;
	List<GameObject> asteroids2;
	List<GameObject> asteroids3;
	public GameObject asteroid1;
	public GameObject asteroid2;
	public GameObject asteroid3;

	//public Rigidbody2D rigidBody2D;

	void Awake() {
		
		//object pool for explosion
		explosions = new List<GameObject> ();
		for(int i = 0; i < pooledAmount; i++) {
			GameObject obj = (GameObject)Instantiate(explosion);
			obj.GetComponent<AsteroidParticleControls>().enabled = true;
			obj.SetActive(false);
			explosions.Add(obj);
		}

		//pool some asteroids
		AsteroidPooler1();
		AsteroidPooler2();
		AsteroidPooler3();
	}

	void CreateExplosion(Vector3 asteroidPosition) {
		
		//activate an explosion where the asteroid was
		for(int i = 0; i < explosions.Count; i++) {
			if(!explosions[i].activeInHierarchy) {
				explosions[i].transform.Translate(asteroidPosition);
				explosions[i].SetActive(true);
				break;
			}
		}
	}

	void AsteroidPooler1() {
		
		//pooling1
		asteroids1 = new List<GameObject>();
		for(int i = 0; i < pooledAmount1; i++) {
			GameObject obj = (GameObject)Instantiate(asteroid1);
			obj.SetActive(false);
			asteroids1.Add(obj);
		}	
	}
	
	void AsteroidPooler2() {
		
		//pooling2
		asteroids2 = new List<GameObject>();
		for(int i = 0; i < pooledAmount2; i++) {
			GameObject obj = (GameObject)Instantiate(asteroid2);
			obj.SetActive(false);
			asteroids2.Add(obj);
		}	
	}
	
	void AsteroidPooler3() {
		
		//pooling3
		asteroids3 = new List<GameObject>();
		for(int i = 0; i < pooledAmount3; i++) {
			GameObject obj = (GameObject)Instantiate(asteroid3);
			obj.SetActive(false);
			asteroids3.Add(obj);
		}	
	}

	void SetMass(int startingHealth) {
		
		//divides asteroid in two, rounds up to 1 if health is <1
		//newMass = Mathf.FloorToInt(startingHealth * 0.5F);
		//newMass = Mathf.CeilToInt(startingHealth * 0.5F);
		newMass = 1.5F;
		//Debug.Log(newMass);
	}
	
	void CreateBrokenAsteroids(Vector3 asteroidPosition) {



		//picks a random asteroid from the array
		int asteroidArrayIndex = Random.Range(0,asteroidArray.Length);
		GameObject randomAsteroid = asteroidArray[asteroidArrayIndex];
		
		//creates an asteroid and puts it on the spawner
		if(randomAsteroid.name == "Asteroid1") {
			for(int i = 0; i < asteroids1.Count; i++) {
				if(!asteroids1[i].activeInHierarchy) {
					asteroids1[i].SetActive(true);
					asteroidPosition.z = 10F;
					//asteroidPosition.y = (Random.Range(asteroidPosition.y - newMass, asteroidPosition.y + newMass));
					//asteroidPosition.x = (Random.Range(asteroidPosition.x - newMass, asteroidPosition.x + newMass));
					asteroids1[i].transform.position = asteroidPosition;

					asteroids1[i].SendMessage("NotFreshAsteroid");

					//size based off previous asteroid
					Rigidbody2D rigidBody2D = asteroids1[i].GetComponent<Rigidbody2D>();
					rigidBody2D.mass = newMass;
					rigidBody2D.centerOfMass = new Vector3(Random.Range(-newMass * 0.5F, newMass * 0.5F), Random.Range(-newMass * 0.5F, newMass * 0.5F));
					asteroids1[i].transform.localScale = new Vector3(newMass, newMass, newMass);
					break;
				}
			}	
		}
		
		//creates an asteroid and puts it on the spawner
		if(randomAsteroid.name == "Asteroid2") {
			for(int i = 0; i < asteroids2.Count; i++) {
				if(!asteroids2[i].activeInHierarchy) {
					asteroids2[i].SetActive(true);	
					asteroidPosition.z = 10F;
					//asteroidPosition.y = (Random.Range(asteroidPosition.y - newMass, asteroidPosition.y + newMass));
					//asteroidPosition.x = (Random.Range(asteroidPosition.x - newMass, asteroidPosition.x + newMass));
					asteroids2[i].transform.position = asteroidPosition;	

					asteroids2[i].SendMessage("NotFreshAsteroid");

					//size based off previous asteroid
					Rigidbody2D rigidBody2D = asteroids2[i].GetComponent<Rigidbody2D>();
					rigidBody2D.mass = newMass;
					rigidBody2D.centerOfMass = new Vector3(Random.Range(-newMass * 0.5F, newMass * 0.5F), Random.Range(-newMass * 0.5F, newMass * 0.5F));
					asteroids2[i].transform.localScale = new Vector3(newMass, newMass, newMass);
					break;
				}
			}	
		}
		
		//creates an asteroid and puts it on the spawner
		if(randomAsteroid.name == "Asteroid3") {
			for(int i = 0; i < asteroids3.Count; i++) {
				if(!asteroids3[i].activeInHierarchy) {
					asteroids3[i].SetActive(true);	
					asteroidPosition.z = 10F;
					//asteroidPosition.y = (Random.Range(asteroidPosition.y - newMass, asteroidPosition.y + newMass));
					//asteroidPosition.x = (Random.Range(asteroidPosition.x - newMass, asteroidPosition.x + newMass));
					asteroids3[i].transform.position = asteroidPosition;

					asteroids3[i].SendMessage("NotFreshAsteroid");

					//size based off previous asteroid
					Rigidbody2D rigidBody2D = asteroids3[i].GetComponent<Rigidbody2D>();
					rigidBody2D.mass = newMass;
					rigidBody2D.centerOfMass = new Vector3(Random.Range(-newMass * 0.5F, newMass * 0.5F), Random.Range(-newMass * 0.5F, newMass * 0.5F));
					asteroids3[i].transform.localScale = new Vector3(newMass, newMass, newMass);
					break;
				}
			}	
		}
	}
}
