using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public static int currentScore;
	public static int highScore;
	public int asteroidHealth;
	public float timeSinceLevelLoad;
	public int gameTime;
	public GUISkin layout;

	void OnEnable() {

		//points for effort, not participation
		//gameTime = 0;
		currentScore = 0;

		//InvokeRepeating ("SetHighScore", 1F, 1F);
	}

	void OnDisable() {

		//update the high score
		SetHighScore();
		//gameTime = 0;
		currentScore = 0;
	}

	void FixedUpdate() {

		//gameTime = Mathf.RoundToInt (Time.timeSinceLevelLoad);

		/*
		if (PlayerControls.playerIsDead == false) {
			gameTime = Mathf.RoundToInt (Time.timeSinceLevelLoad);
		}

		if (PlayerControls.playerIsDead == true) {
			int stopGameTime = gameTime;
			gameTime = stopGameTime;
		}
		*/
		//currentScore = gameTime + currentScore;
	}
	
	public static void AddPoint(int addedPoints) {
		
		//increase score
		currentScore = currentScore + addedPoints;
		SetHighScore();

	}

	/*
	void Update() {

		int addedPoints = gameTime;

		//setHighScore();
		AddPoint (addedPoints);
	}
	*/

	public static void SetHighScore() {

		//Debug.Log ("set score");

		//check if the score needs to be updated
		if (currentScore > PlayerPrefs.GetInt("High Score")) {
			PlayerPrefs.SetInt("High Score", currentScore);
		}
	}

	void OnGUI() {

		//make a score thing at the top of the screen
		GUI.skin = layout;
		GUI.Label (new Rect ((Screen.width / 2) + 200, 20, 220, 50), "Current Score: " + (currentScore));
		GUI.Label (new Rect ((Screen.width / 2) + 200, 50, 220, 50), "High Score: " + PlayerPrefs.GetInt("High Score"));
	}
}