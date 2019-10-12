using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletGenerator : MonoBehaviour {
	
	public int pooledAmount = 30;
	public GameObject bullet;
	public Transform spread1;
	public Transform spread2;
	public Transform spread3;
	public Transform spread4;
	public Transform spread5;
	public List<GameObject> bullets;

	public static bool enabledGun1 = true;
	public static bool enabledGun2 = false;
	public static bool enabledGun3 = false;
	
	private float shotInterval;
	private float shootTime = 0.0F;
	private float gun1Interval = 0.2F;
	private float gun2Interval = 0.7F;
	private float gun3Interval = 0.5F;

	private float timer = 0.0F;
	private float timerMax = 10.0F;
	public static bool addTime = false;
	public bool showTimer = false;
	public bool startTimer = false;
	public bool resetTimer = false;
	public GUISkin layout;

	public AudioSource audioSource;
	
	public AudioClip shootSound;
	
	void Start() {

		if(audioSource == null) {
			audioSource = GetComponent<AudioSource> ();
		}

		//start the game with the default gun settings
		enabledGun1 = true;
		shotInterval = gun1Interval;

		//object pool for bullets
		bullets = new List<GameObject> ();
		for(int i = 0; i < pooledAmount; i++) {
			GameObject obj = (GameObject)Instantiate(bullet);
			obj.GetComponent<Bullet1>().enabled = true;
			obj.SetActive(false);
			bullets.Add(obj);
		}	
	}

	void Update() {

		//choose your weapons and use em!
		GunPicker();
		ShootControls();
		GunReset();
	}
	

	void GunPicker() {

		//on all bullets, active or not
		for(int i = 0; i < bullets.Count; i++){

			//turn on gun #1
			if(enabledGun1 == true) {

				//can only use this gun and sets its own shot speed
				enabledGun1 = true;
				enabledGun2 = false;
				enabledGun3 = false;
				shotInterval = gun1Interval;
			}

			//turn on gun #2
			if(enabledGun2 == true) {

				//can only use this gun and sets its own shot speed
				enabledGun1 = false;
				enabledGun2 = true;
				enabledGun3 = false;
				shotInterval = gun2Interval;
				startTimer = true;
			}

			//turn on gun #3
			if(enabledGun3 == true) {

				//can only use this gun and sets its own shot speed
				enabledGun1 = false;
				enabledGun2 = false;
				enabledGun3 = true;
				shotInterval = gun3Interval;
				startTimer = true;
			}
		}
	}

	void GunReset() {

		//only start once getting new weapons
		if(startTimer == true) {

			//bonus time
			if(addTime == true) {

				//add another 10 seconds
				timer = timer + timerMax;
				addTime = false;
			}

			//show timer
			showTimer = true;

			//countdown
			timer -= Time.deltaTime;

			//out of time
			if(timer < 0) {

				//reset and hide timer
				resetTimer = true;
			}

			//hide and set to 0 seconds
			if(resetTimer == true) {
				
				//default gun settings
				enabledGun1 = true;
				shotInterval = gun1Interval;

				//hide timer
				showTimer = false;

				//reset timer for next time
				startTimer = false;
				timer = 0F;
				resetTimer = false;
			}
		}
	}

	void OnGUI() {
		
		//ammo timer
		if(showTimer == true && PlayerControls.playerIsDead == false) {

			//convert to seconds and milliseconds
			GUI.skin = layout;
			int minutes = Mathf.FloorToInt(timer / 60F);
			int seconds = Mathf.FloorToInt(timer - minutes * 60);
			int milliseconds = Mathf.FloorToInt((timer * 100) % 100);
			string niceTime = string.Format("{0:00}:{1:00}", seconds, milliseconds);

			//position below player
			GUI.Label (new Rect ((Screen.width / 2) - 60, (Screen.height / 2) + 100, 60, 30), "" + niceTime);
		}
	}

	void ShootControls() {

		//while holding space, immediately shoot a bullet every fireTime seconds
		if(Input.GetKey("space") && Time.time >= shootTime) {

			//only fire so quickly
			shootTime = Time.time + shotInterval;
			Fire();
			audioSource.PlayOneShot(shootSound, 1F);
		}
	}

	void Fire() {

		//only shoot the enabled gun but allow hot switching
		ShootGun1();
		ShootGun2();
		ShootGun3();

	}

	void ShootGun1(){

		//keep track of every bullet
		for(int i = 0; i < bullets.Count; i++) {

			//don't change bullet types on those already fired
			if(bullets[i].activeSelf == false) {
				bullets[i].GetComponent<Bullet1>().enabled = true;
				bullets[i].GetComponent<Bullet2>().enabled = false;
				bullets[i].GetComponent<Bullet3>().enabled = false;
			}

			//turn on bullet at the player's location
			if(!bullets[i].activeInHierarchy && enabledGun1 == true) {
				bullets[i].transform.position = spread1.transform.position;
				bullets[i].transform.rotation = spread1.transform.rotation;
				bullets[i].SetActive(true);	
				break;
			}
		}
	}

	void ShootGun2(){

		//shoot 5 bullets in a spread
		for(int i = 0; i < bullets.Count; i++) {

			//don't change bullet types on those already fired
			if(bullets[i].activeSelf == false) {
				bullets[i].GetComponent<Bullet1>().enabled = false;
				bullets[i].GetComponent<Bullet2>().enabled = true;
				bullets[i].GetComponent<Bullet3>().enabled = false;
			}
			
			//turn on bullet at the spread1 location
			if(!bullets[i].activeInHierarchy && enabledGun2 == true) {
				bullets[i].transform.position = spread1.transform.position;
				bullets[i].transform.rotation = spread1.transform.rotation;
				bullets[i].SetActive(true);	
				break;
			}
		}

		for(int i = 0; i < bullets.Count; i++) {

			//don't change bullet types on those already fired
			if(bullets[i].activeSelf == false) {
				bullets[i].GetComponent<Bullet1>().enabled = false;
				bullets[i].GetComponent<Bullet2>().enabled = true;
				bullets[i].GetComponent<Bullet3>().enabled = false;
			}
			
			//turn on bullet at the spread2 location
			if(!bullets[i].activeInHierarchy && enabledGun2 == true) {
				bullets[i].transform.position = spread2.transform.position;
				bullets[i].transform.rotation = spread2.transform.rotation;
				bullets[i].SetActive(true);	
				break;
			}
		}

		for(int i = 0; i < bullets.Count; i++) {

			//don't change bullet types on those already fired
			if(bullets[i].activeSelf == false) {
				bullets[i].GetComponent<Bullet1>().enabled = false;
				bullets[i].GetComponent<Bullet2>().enabled = true;
				bullets[i].GetComponent<Bullet3>().enabled = false;
			}
			
			//turn on bullet at the spread3 location
			if(!bullets[i].activeInHierarchy && enabledGun2 == true) {
				bullets[i].transform.position = spread3.transform.position;
				bullets[i].transform.rotation = spread3.transform.rotation;
				bullets[i].SetActive(true);	
				break;
			}
		}
		
		for(int i = 0; i < bullets.Count; i++) {

			//don't change bullet types on those already fired
			if(bullets[i].activeSelf == false) {
				bullets[i].GetComponent<Bullet1>().enabled = false;
				bullets[i].GetComponent<Bullet2>().enabled = true;
				bullets[i].GetComponent<Bullet3>().enabled = false;
			}
			
			//turn on bullet at the spread4 location
			if(!bullets[i].activeInHierarchy && enabledGun2 == true) {
				bullets[i].transform.position = spread4.transform.position;
				bullets[i].transform.rotation = spread4.transform.rotation;
				bullets[i].SetActive(true);	
				break;
			}
		}
		
		for(int i = 0; i < bullets.Count; i++) {

			//don't change bullet types on those already fired
			if(bullets[i].activeSelf == false) {
				bullets[i].GetComponent<Bullet1>().enabled = false;
				bullets[i].GetComponent<Bullet2>().enabled = true;
				bullets[i].GetComponent<Bullet3>().enabled = false;
			}
			
			//turn on bullet at the spread5 location
			if(!bullets[i].activeInHierarchy && enabledGun2 == true) {
				bullets[i].transform.position = spread5.transform.position;
				bullets[i].transform.rotation = spread5.transform.rotation;
				bullets[i].SetActive(true);	
				break;
			}
		}
	}

	void ShootGun3(){

		//keep track of every bullet
		for(int i = 0; i < bullets.Count; i++) {

			//don't change bullet types on those already fired
			if(bullets[i].activeSelf == false) {
				bullets[i].GetComponent<Bullet1>().enabled = false;
				bullets[i].GetComponent<Bullet2>().enabled = false;
				bullets[i].GetComponent<Bullet3>().enabled = true;
			}

			//turn on bullet at the player's location
			if(!bullets[i].activeInHierarchy && enabledGun3 == true) {
				bullets[i].transform.position = spread1.transform.position;
				bullets[i].transform.rotation = spread1.transform.rotation;
				bullets[i].SetActive(true);	
				break;
			}
		}
	}
}
