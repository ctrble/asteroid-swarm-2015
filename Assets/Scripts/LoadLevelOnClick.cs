using UnityEngine;
using System.Collections;

public class LoadLevelOnClick : MonoBehaviour {
	
	public GameObject levelSelectImage;
	public GameObject loadingImage;
	public GameObject levelMusic;
	public GameObject menuMusic;

	public int selectedLevel;

	/*
	public void LoadLevelSelect() {
		
		levelSelectImage.SetActive(true);
	}

	public void BackToMenu() {
		
		levelSelectImage.SetActive(false);
	}
	*/

	public void LoadScene(int level) {

		selectedLevel = level;

		//if (selectedLevel == 1 | selectedLevel == 2 | selectedLevel == 3 | selectedLevel == 4 | selectedLevel == 5 | selectedLevel == 6) {
		
			//menuMusic.SetActive(false);
			//levelMusic.SetActive(true);
			loadingImage.SetActive(true);

			//Application.LoadLevel(level);
			//StartCoroutine ("LoadLevelAfterMusic");
			Application.LoadLevel(selectedLevel);
		//}

		/*
		else if (selectedLevel == 7) {

			Application.LoadLevel(selectedLevel);
		}

		else if (selectedLevel == 14) {
			
			menuMusic.SetActive(false);
			loadingImage.SetActive(true);
			Application.LoadLevel(selectedLevel);
		}
		*/
	}

	/*
	IEnumerator LoadLevelAfterMusic() {

		yield return new WaitForSeconds (2F);
		Application.LoadLevel(selectedLevel);
	}
	*/
}