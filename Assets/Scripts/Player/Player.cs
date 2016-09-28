using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerController))]
public class Player : MonoBehaviour {
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
	Vector2 input;

	bool canMove;
	bool knockBack;

	Animator anim;
	PlayerController controller;
	Renderer rend;

	void Start () {
		anim = GetComponent<Animator>();
		controller = GetComponent<PlayerController> ();
		rend = GetComponent<Renderer> ();

		gravity = -(2f * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2f);
		minJumpVelocity = Mathf.Sqrt (2f * Mathf.Abs (gravity) * minJumpHeight);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		canMove = true;
	}

	void Update () {
		if (canMove) {
			if (controller.collisions.above || controller.collisions.below)
				velocity.y = 0;
		
			input = InputManager.MainStick ();

			JumpCheck ();
			Move ();

			controller.OnTriggerCheck ();

			SetAnimatorStates ();
		}
	}

	void JumpCheck () {

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
	}

	void Move() {
		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);
	}

	void SetAnimatorStates() {
		anim.SetFloat("Speed", Mathf.Abs(input.x));
		anim.SetFloat("ClimbSpeed", Mathf.Abs(input.y));

		if(controller.collisions.below)
			anim.SetBool ("Grounded", true);
		else
			anim.SetBool ("Grounded", false);
	}

	//===== Public functions used from other scripts =====
	public void KillPlayer(bool kill) {
		if (kill) { //kill player
			canMove = false;
			velocity = Vector3.zero;
			rend.enabled = false;
		} else { //revive player
			canMove = true;
			rend.enabled = true;
			knockBack = false;
		}
	}

	/*
	public void PlayerKnockBack(Vector3 attacker) {
		knockBack = true;
		knockBackTimer = knockBackLength;

		if(transform.position.x < attacker.x) {
			knockBackVelocity = new Vector2(-knockBackSpeed, knockBackSpeed);
		} else {
			knockBackVelocity = new Vector2(knockBackSpeed, knockBackSpeed);		
		}

		audioSource.PlayOneShot (hurtClip, 1f);
	}

	public void EnterLadderZone() {
		onLadder = true;
		rb2D.gravityScale = 0f;
		anim.SetBool ("Climbing", true);
	}

	public void ExitLadderZone() {
		onLadder = false;
		rb2D.gravityScale = gravityStore;
		anim.SetBool("Climbing", false);
	}*/
}
