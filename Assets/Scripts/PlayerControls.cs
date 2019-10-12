using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	
	public Transform player;

	public static bool playerIsDead;

	public float speedDefault = 1F;
	public float topSpeedDefault = 1.5F;
	public float linearDragDefault = 2.8F; //use 5F for more control
	public float angularDragDefault = 30F; //use 60F for more control

	public float speedCurrent;
	public float topSpeedCurrent;
	public float linearDragCurrent; 
	public float angularDragCurrent; 

	public bool brakes = false;
	private Vector2 enginePower;
	private Vector2 sidewinder;
	private float yVelocity = 0.0F;
	public Rigidbody2D rigidBody2D;
	public PolygonCollider2D polygonCollider2D;

	void Awake() {
		
		//get the rigidbody2d component
		if(rigidBody2D == null) {
			rigidBody2D = GetComponent<Rigidbody2D>();
		}

		if(polygonCollider2D == null) {
			polygonCollider2D = GetComponent<PolygonCollider2D>();
		}

		playerIsDead = false;
	}

	void Start() {

		//remember the defaults
		speedCurrent = speedDefault;
		topSpeedCurrent = topSpeedDefault;
		brakes = false;
	
		//update these variables
		enginePower = new Vector2(0, speedCurrent);
		sidewinder = new Vector2(speedCurrent, GetComponent<Rigidbody2D>().velocity.y);
	}

	void FixedUpdate() {

		//control the ship!
		if (playerIsDead == false) {
			ShipControls ();
		} else if (playerIsDead == true) {
			polygonCollider2D.enabled = false;
		}
	}

	void ShipControls() {

		//brakes on
		if(Input.GetKeyDown(KeyCode.LeftShift)) {

			//slow down
			rigidBody2D.drag = linearDragDefault * 5F;
			rigidBody2D.angularDrag = angularDragDefault * 2F;
		}

		//brakes off
		if(Input.GetKeyUp(KeyCode.LeftShift)) {

			//normal speed
			rigidBody2D.drag = linearDragDefault;
			rigidBody2D.angularDrag = angularDragDefault;
		}

		//engines on
		if(Input.GetKey("w")) {
			
			//scoot scoot go
			speedCurrent = Mathf.SmoothDamp(speedCurrent, topSpeedDefault, ref yVelocity, 0.3F);
			enginePower = new Vector2(0, speedCurrent);
			rigidBody2D.AddRelativeForce(enginePower, ForceMode2D.Impulse);
		}
				
		//engines off
		if(Input.GetKeyUp("w")) {
			speedCurrent = speedDefault;
		}
		
		//scoot backwards
		if(Input.GetKey("s")) {
			rigidBody2D.AddRelativeForce(-enginePower/2, ForceMode2D.Impulse);
		}
		
		//twirl
		if(Input.GetKey("a")) {
			rigidBody2D.AddTorque(60 - speedCurrent);
		}
		
		//twirl
		if(Input.GetKey("d")) {
			rigidBody2D.AddTorque(-(60 - speedCurrent));
		}
		
		//strafe
		if(Input.GetKey("q")) {
			rigidBody2D.AddRelativeForce(-sidewinder, ForceMode2D.Impulse);
		}
		
		//strafe
		if(Input.GetKey("e")) {
			rigidBody2D.AddRelativeForce(sidewinder, ForceMode2D.Impulse);
		}
	}
}
