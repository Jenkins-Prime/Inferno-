using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour {
	[SerializeField] float moveSpeed = 3f;
	[SerializeField] float minJumpHeight = 0.2f;
	[SerializeField] float maxJumpHeight = 1f;
	[SerializeField] float timeToJumpApex = 0.4f;

	float gravity;
	float minJumpVelocity;
	float maxJumpVelocity;
	float velocityXSmoothing;

	float accelerationTimeAirborne = 0.2f;
	float accelerationTimeGrounded = 0.1f;
	Vector3 velocity;


	PlayerController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController> ();

		gravity = -(2f * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2f);
		minJumpVelocity = Mathf.Sqrt (2f * Mathf.Abs (gravity) * minJumpHeight);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.collisions.above || controller.collisions.below)
			velocity.y = 0;
		
		Vector2 input = InputManager.MainStick ();

		if (InputManager.JumpButton ()) {
			if (controller.collisions.below) {
				velocity.y = maxJumpVelocity;
			}
		}
		if (InputManager.ReleaseJumpButton()){
			if (velocity.y > minJumpVelocity) {
				velocity.y = minJumpVelocity;
			}
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);
	}
}
