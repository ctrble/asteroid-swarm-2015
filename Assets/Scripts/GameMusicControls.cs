using UnityEngine;
using System.Collections;

public class GameMusicControls : MonoBehaviour {

	public GameObject mainTheme;
	public GameObject gameOverTheme;
	public bool hasSwitchedTunes;

	void Start() {

		mainTheme.SetActive (true);
		hasSwitchedTunes = false;
	}
	
	void Update() {

		if (PlayerControls.playerIsDead == true && hasSwitchedTunes == false) {

			mainTheme.SetActive (false);
			//gameOverTheme.SetActive (true);
			//hasSwitchedTunes = true;
		}
	}
}
