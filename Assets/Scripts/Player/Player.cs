using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerController))]
public class Player : MonoBehaviour {
	[Header("Movement Variables")]
	[SerializeField] float moveSpeed = 3f;
	[SerializeField] float climbSpeed = 2f;
	[SerializeField] float minJumpHeight = 0.2f;
	[SerializeField] float maxJumpHeight = 1f;
	[SerializeField] float timeToJumpApex = 0.4f;
	[SerializeField] float knockBackSpeed = 2f;
	[SerializeField] float knockBackLength = 0.2f;

	[Header("Audio Clips")]
	[SerializeField] AudioClip hurtClip;
	[SerializeField] AudioClip jumpClip;

	float gravity;
	float minJumpVelocity;
	float maxJumpVelocity;
	float velocityXSmoothing;
	float accelerationTimeAirborne = 0.2f;
	float accelerationTimeGrounded = 0.1f;
	public Vector3 velocity;

	[HideInInspector] public Vector2 input;
	[HideInInspector] public bool canMove;
	[HideInInspector] public bool knockBack;
	bool jump;
	bool hasDoubleJumped;

	float knockBackTimer;
	Vector2 knockBackVelocity;

	Animator anim;
	AudioSource audioSource;
	PlayerController controller;
	SpriteRenderer rend;

	void Start () {
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		controller = GetComponent<PlayerController> ();
		rend = GetComponent<SpriteRenderer> ();

		gravity = -(2f * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2f);
		minJumpVelocity = Mathf.Sqrt (2f * Mathf.Abs (gravity) * minJumpHeight);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		knockBackTimer = 0f;
		canMove = true;
	}

	void Update () {
		if (canMove) {
			if (controller.collisions.above || controller.collisions.below)
				velocity.y = 0;

			input = InputManager.MainStick ();

			JumpCheck ();
			Move ();

			SetAnimatorStates ();
		}
	}

	bool KnockBackCheck() {
		if (knockBack) {
			if (knockBackTimer > 0) {
				knockBackTimer -= Time.deltaTime;
			} else {
				knockBack = false;
			}
		}

		return !knockBack; //true if knockback ended
	}

	void JumpCheck () {
		if (!controller.collisions.onLadder) {
			if (InputManager.JumpButton ()) {
				if (controller.collisions.below || controller.collisions.onLadderAbove) {
					velocity.y = maxJumpVelocity;
					jump = true;
					audioSource.PlayOneShot (jumpClip, 1.0f);
				} else if (!hasDoubleJumped) {
					velocity.y = maxJumpVelocity;
					hasDoubleJumped = true;
					audioSource.PlayOneShot(jumpClip, 1.0f);
				}
			}

			if (InputManager.ReleaseJumpButton ()) {
				if (velocity.y > minJumpVelocity) {
					velocity.y = minJumpVelocity;
				}
				jump = false;
			}

			if (controller.collisions.below || controller.collisions.onLadderAbove) {
				hasDoubleJumped = false;
			}
		}
	}

	void Move() {
		if (KnockBackCheck ()) {
			float targetVelocityX;
			if (controller.collisions.onLadder)
				targetVelocityX = input.x * climbSpeed;
			else
				targetVelocityX = input.x * moveSpeed;
		
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

			if (controller.collisions.onLadder) {
				velocity.y = input.y * climbSpeed;
			} else if (controller.collisions.onLadderAbove) {
				if (input.y < 0)
					velocity.y = input.y * climbSpeed;
				else if (!jump)
					velocity.y = 0f;
			} else {
				velocity.y += gravity * Time.deltaTime;
			}
		} else {
			velocity = knockBackVelocity;
		}
	
		controller.Move (velocity * Time.deltaTime, input);
	}

	void SetAnimatorStates() {
		//Turn sprite
		rend.flipX = (velocity.x < 0) ? true : false;

		anim.SetFloat("Speed", Mathf.Abs(input.x));
		anim.SetFloat("ClimbSpeed", Mathf.Abs(input.y));

		if (controller.collisions.below || controller.collisions.onLadderAbove || controller.collisions.onLadderBelow)
			anim.SetBool ("Grounded", true);
		 else {
			anim.SetBool ("Grounded", false);
		}

		anim.SetBool ("Climbing", controller.collisions.onLadder && !controller.collisions.onLadderBelow);
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


	public void PlayerKnockBack(Vector3 attacker) {
		knockBack = true;
		knockBackTimer = knockBackLength;

		if (transform.position.x < attacker.x) {
			knockBackVelocity = new Vector2 (-knockBackSpeed, knockBackSpeed);
		} else {
			knockBackVelocity = new Vector2 (knockBackSpeed, knockBackSpeed);		
		}

		audioSource.PlayOneShot (hurtClip, 1f);
	}

	/*
	void JumpCheck() {
		if(InputManager.JumpButton() && !onLadder) { //Jump Check
			if(grounded) { //First jump
				audioSource.PlayOneShot(jumpClip, 1.0f);
				jump = true;
			} else if(!doubleJumped) { //Second Jump
				audioSource.PlayOneShot(jumpClip, 0.5f);
				doubleJumped = true;
				jump = true;
			}
		}
	}
	  
	void AttackCheck() {
		if (InputManager.FireButton()) {
			anim.SetBool ("Firing", true);

			if(shotTimer <= 0) { //change this to bool values
				Instantiate (bullet, firePoint.position, firePoint.rotation); //change this to .enable for cpu optimization
				shotTimer = shotDelay;
			}
		} else if (InputManager.MeleeButton()) {
			//Melee code here
			//anim.SetBool("Sword", true);
		}

		//TODO: Move To a better place
		if (shotTimer > 0)
			shotTimer -= Time.deltaTime;
	}
*/		
}
