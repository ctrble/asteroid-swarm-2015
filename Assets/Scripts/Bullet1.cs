using UnityEngine;
using System.Collections;

public class Bullet1 : MonoBehaviour {

	public float speed = 50;

	void FixedUpdate() {

		//bullet speed!
		transform.Translate (0, speed * Time.deltaTime, 0);
	}
}
