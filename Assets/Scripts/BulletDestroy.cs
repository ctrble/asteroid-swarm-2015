using UnityEngine;
using System.Collections;

public class BulletDestroy : MonoBehaviour {

	//recycles bullets every F seconds
	void OnEnable() {
		Invoke ("RemoveBullet", 2.0F);
	}

	void OnTriggerExit2D(Collider2D coll) {

		//don't get points for accidentally killing asteroids you can't see
		if (coll.gameObject.tag == "BulletZone") {
			RemoveBullet();
		}
	}
	
	void RemoveBullet() {
		
		//turn off bullet
		gameObject.SetActive (false);
	}

	void OnDisable() {

		//stop removing bullets if it's already gone
		CancelInvoke() ;
	}
}
