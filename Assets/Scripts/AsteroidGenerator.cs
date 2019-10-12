using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidGenerator : MonoBehaviour {

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
	public Vector3 asteroidPosition;

	void Awake() {

		//pool some asteroids
		AsteroidPooler1();
		AsteroidPooler2();
		AsteroidPooler3();
	}

	void Start() {	

		//make some asteroids
		/*
		 * make too many asteroids
		InvokeRepeating("CreateAsteroid", Random.Range(0.5F, 2F), Random.Range(2F, 4F));
		InvokeRepeating("CreateAsteroid", Random.Range(0.5F, 2F), Random.Range(2F, 4F));
		InvokeRepeating("CreateAsteroid", Random.Range(0.5F, 2F), Random.Range(2F, 4F));
		InvokeRepeating("CreateAsteroid", Random.Range(0.5F, 2F), Random.Range(2F, 4F));
		*/
		InvokeRepeating("CreateAsteroid", 1F, 1F);
	}

	void Update() {

		//track the spawn location
		asteroidPosition = gameObject.transform.position;
		asteroidPosition.x = asteroidPosition.x + (Random.Range(-5, 5));
		asteroidPosition.y = asteroidPosition.y + (Random.Range(-5, 5));

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

	void CreateAsteroid() {
		
		//picks a random asteroid from the array
		int asteroidArrayIndex = Random.Range(0,asteroidArray.Length);
		GameObject randomAsteroid = asteroidArray[asteroidArrayIndex];
		
		//creates an asteroid and puts it on the spawner
		if(randomAsteroid.name == "Asteroid1") {
			for(int i = 0; i < asteroids1.Count; i++) {
				if(!asteroids1[i].activeInHierarchy) {
					asteroids1[i].SetActive(true);
					asteroidPosition.z = 10F;
					asteroids1[i].transform.position = asteroidPosition;
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
					asteroids2[i].transform.position = asteroidPosition;					
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
					asteroids3[i].transform.position = asteroidPosition;					
					break;
				}
			}	
		}
	}
}
