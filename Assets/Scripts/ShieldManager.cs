using UnityEngine;
using System.Collections;

public class ShieldManager : MonoBehaviour {

	public GameObject shieldObject;
	public static bool enabledShield = false;

	private float timer = 0.0F;
	private float timerMax = 10.0F;
	public static bool addTime = false;
	public bool showTimer = false;
	public bool startTimer = false;
	public bool resetTimer = false;
	public GUISkin layout;
	public GameObject player;

	void Update() {

		//track the shield state
		ShieldReset();
		ShieldActivater();
	}

	void ShieldActivater() {

		//get the player
		if(player == null) {
			player = GameObject.FindWithTag("Player");
		}

		//turn on the shield and start the timer
		if (enabledShield == true && PlayerControls.playerIsDead == false) {
			enabledShield = true;
			shieldObject.SetActive(true);
			startTimer = true;
			AsteroidDestroy.playerCanDie = false;
		}

		//turn off the shield and stop the timer
		if (enabledShield == false) {
			enabledShield = false;
			shieldObject.SetActive(false);
			startTimer = false;
			AsteroidDestroy.playerCanDie = true;
		}
	}

	void ShieldReset() {
		
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
				
				//default shield settings
				enabledShield = false;
				
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

		//shield timer
		if(showTimer == true && PlayerControls.playerIsDead == false) {

			//convert to seconds and milliseconds
			GUI.skin = layout;
			int minutes = Mathf.FloorToInt(timer / 60F);
			int seconds = Mathf.FloorToInt(timer - minutes * 60);
			int milliseconds = Mathf.FloorToInt((timer * 100) % 100);
			string niceTime = string.Format("{0:00}:{1:00}", seconds, milliseconds);

			//position below player
			GUI.skin = layout;
			GUI.Label (new Rect ((Screen.width / 2) + 0, (Screen.height / 2) + 100, 60, 30), "" + niceTime);
		}
	}

}
