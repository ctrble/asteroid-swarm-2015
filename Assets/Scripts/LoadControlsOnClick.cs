using UnityEngine;
using System.Collections;

public class LoadControlsOnClick : MonoBehaviour {
	
	public GameObject controlsImage;
	
	void Update() {
		
		if(Input.anyKeyDown) {
			
			//turn off the credits image
			controlsImage.SetActive(false);
		}
	}
	
	public void LoadControls() {
		
		controlsImage.SetActive(true);
	}
}