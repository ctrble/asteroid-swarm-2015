using UnityEngine;
using System.Collections;

public class LoadCreditsOnClick : MonoBehaviour {

	public GameObject creditsImage;
	
	void Update() {
		
		if(Input.anyKeyDown) {
			
			//turn off the credits image
			creditsImage.SetActive(false);
		}
	}
	
	public void LoadCredits() {
		
		creditsImage.SetActive(true);
	}
}
