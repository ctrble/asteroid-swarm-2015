using UnityEngine;
using System.Collections;

public class LoadMenuFromSplash : MonoBehaviour {

	void Update() {

		Invoke ("LoadMenu", 5F);

		if(Input.anyKeyDown) {

			LoadMenu();
		}
	}

	void LoadMenu() {

		Application.LoadLevel("Menu");
	}
}
