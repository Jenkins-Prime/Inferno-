using UnityEngine;
using System.Collections;

public class PlayerController : RaycastController {
	float maxClimbAngle = 80;
	float maxDescendAngle = 75;

	public CollisionInfo collisions;
	[HideInInspector]
	public Vector2 playerInput;

	public bool isDead; //temp var
	public bool canMove;

	public override void Start() {
		base.Start ();
	}

	public void Move(Vector3 velocity,bool standingOnPlatform) {
		Move (velocity, Vector2.zero, standingOnPlatform);
	}

	public void Move(Vector3 velocity, Vector2 input,bool standingOnPlatform = false) {
		UpdateRaycastOrigins ();
		collisions.Reset ();
		collisions.velocityOld = velocity;
		playerInput = input;

		if (velocity.y < 0) {
			DescendSlope (ref velocity);
		}
		if (velocity.x != 0) {
			HorizontalCollisions (ref velocity);
		}
		if (velocity.y != 0) {
			VerticalCollisions (ref velocity);
		}

		transform.Translate (velocity);

		if (standingOnPlatform) {
			collisions.below = true;
		}
	}

	public void OnTriggerCheck() {
		Collider2D triggerCol = Physics2D.OverlapArea(col.bounds.min, col.bounds.max, triggerMask);
		if (triggerCol != null) {
			string layerName = LayerMask.LayerToName (triggerCol.gameObject.layer);

			switch (layerName) {
			case "Enemy":
				DamagePlayer dmg = triggerCol.GetComponent<DamagePlayer> ();
				if (dmg != null)
					dmg.DealDamage ();
				break;
			case "Pickup":
				Pickup pickup = triggerCol.GetComponent<Pickup> ();
				if (pickup != null)
					pickup.Collect ();
				break;
			case "Checkpoint":
				Checkpoint checkpoint = triggerCol.GetComponent<Checkpoint> ();
				if (checkpoint != null)
					checkpoint.SetCheckpoint ();
				break;
			}
		}
	}

	void HorizontalCollisions(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + skinWidth;

		for (int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLength, Color.red);

			if (hit) {
				if (hit.distance == 0) {
					continue;
				}

				float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);

				if (i == 0 && slopeAngle <= maxClimbAngle) {
					if (collisions.descendingSlope) {
						collisions.descendingSlope = false;
						velocity = collisions.velocityOld;
					}
					float distanceToSlopeStart = 0f;
					if (slopeAngle != collisions.slopeAngleOld) {
						distanceToSlopeStart = hit.distance - skinWidth;
						velocity.x -= distanceToSlopeStart * directionX;
					}

					ClimbSlope (ref velocity, slopeAngle);
					velocity.x += distanceToSlopeStart * directionX;
				}

				if (!collisions.climbingSlope || slopeAngle > maxClimbAngle) {
					velocity.x = (hit.distance - skinWidth) * directionX;
					rayLength = hit.distance;

					if (collisions.climbingSlope) {
						velocity.y = Mathf.Tan (collisions.slopeAngle * Mathf.Deg2Rad * Mathf.Abs (velocity.x));
					}

					collisions.left = (directionX == -1);
					collisions.right = (directionX == 1);
				}
			}
		}
	}

	void VerticalCollisions(ref Vector3 velocity) {
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLength, Color.red);

			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				if (collisions.climbingSlope) {
					velocity.x = velocity.y / Mathf.Tan (collisions.slopeAngle * Mathf.Deg2Rad * Mathf.Sign (velocity.x));
				}

				collisions.below = (directionY == -1);
				collisions.above = (directionY == 1);
			}
		}

		if (collisions.climbingSlope) {
			float directionX = Mathf.Sign (velocity.x);
			rayLength = Mathf.Abs (velocity.x) + skinWidth;
			Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * velocity.y;
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			if (hit) {
				float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);
				if (slopeAngle != collisions.slopeAngle) {
					velocity.x = (hit.distance - skinWidth) * directionX;
					collisions.slopeAngle = slopeAngle;
				}
			}
		}
	}

	void ClimbSlope(ref Vector3 velocity, float slopeAngle) {
		float moveDistance = Mathf.Abs (velocity.x);
		float climbVelocityY = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDistance;

		if (velocity.y <= climbVelocityY) {
			velocity.y = climbVelocityY;
			velocity.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (velocity.x);
			collisions.below = true;
			collisions.climbingSlope = true;
			collisions.slopeAngle = slopeAngle;
		}
	}

	void DescendSlope(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

		if (hit) {
			float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);
			if (slopeAngle != 0 && slopeAngle <= maxDescendAngle) {
				if (Mathf.Sign (hit.normal.x) == directionX) {
					if (hit.distance - skinWidth <= Mathf.Tan (slopeAngle * Mathf.Deg2Rad * Mathf.Abs (velocity.x))) {
						float moveDistance = Mathf.Abs (velocity.x);
						float descendVelocityY = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDistance;
						velocity.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (velocity.x);
						velocity.y -= descendVelocityY;

						collisions.slopeAngle = slopeAngle;
						collisions.descendingSlope = true;
						collisions.below = true;
					}
				}
			}
		}
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;

		public bool climbingSlope;
		public bool descendingSlope;
		public float slopeAngle, slopeAngleOld;
		public Vector3 velocityOld;

		public void Reset() {
			above = below = false;
			left = right = false;
			climbingSlope = false;
			descendingSlope = false;

			slopeAngleOld = slopeAngle;
			slopeAngle = 0;
		}
	}


	public void PlayerKnockBack(Vector3 tmp) { //temp function

	}

	public void EnterLadderZone() { //temp

	}

	public void ExitLadderZone() { //temp

	}

	public void KillPlayer(bool b) { //temp

	}
	/*
	[Header("Movement Variables")]
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] float jumpHeight = 3.5f;
	[SerializeField] float knockBackSpeed = 2f;
	[SerializeField] float climbSpeed = 2f;
	[SerializeField] float knockBackLength = 0.2f;

	//[Header("Head Stomp Variables")]
	//[SerializeField] int damageToGive = 1;
	//[SerializeField] float enemyBounceHeight = 2f;

	[Header("Ranged Attack Variables")]
	[SerializeField] GameObject bullet;
	[SerializeField] float shotDelay = 2f;

	[Header("Audio Clips")]
	[SerializeField] AudioClip hurtClip;
	[SerializeField] AudioClip jumpClip;

	[SerializeField] LayerMask groundLayer;
	[SerializeField] Transform groundCheck;
	[SerializeField] float groundCheckRadius = 0.1f;
	[SerializeField] Transform firePoint;

	public bool canMove;
    [HideInInspector]
	public bool isDead;
	bool grounded;
	bool doubleJumped;
	bool jump;
	bool onLadder;
	bool knockBack;
	Vector2 inputVector;
	Vector2 knockBackVelocity;
	float gravityStore;
	float knockBackTimer;
	float shotTimer;

	Animator anim;
	AudioSource audioSource;
	Renderer rend;
	Rigidbody2D rb2D;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
		rend = GetComponent<Renderer> ();
		rb2D = GetComponent<Rigidbody2D>();

		gravityStore = rb2D.gravityScale;
		knockBackTimer = 0f;
		canMove = true;
	}

	// Update is called once per frame
	void Update () {
		if (canMove && KnockBackCheck ()) {
			inputVector = InputManager.MainStick(); //GetInput
			anim.SetFloat("Speed", Mathf.Abs(inputVector.x));
			anim.SetFloat("ClimbSpeed", Mathf.Abs(inputVector.y));

			GroundCheck ();
			JumpCheck ();

			//Turn sprite
			if (rb2D.velocity.x > 0.01f)
				transform.localScale = new Vector3(1f, 1f, 1f);
			else if (rb2D.velocity.x < -0.01f)
				transform.localScale = new Vector3(-1f, 1f, 1f);

			AttackCheck(); //TODO: Move to Attack script
		}
    }

	void FixedUpdate() {
		if (!canMove) {
			rb2D.velocity = Vector2.zero;
		} else if (knockBack) {
			rb2D.velocity = knockBackVelocity;
		} else if(onLadder) { 
			rb2D.velocity = new Vector2(inputVector.x * moveSpeed, inputVector.y * climbSpeed);
		} else if (jump) {
			rb2D.velocity = new Vector2 (inputVector.x * moveSpeed, jumpHeight);
			jump = false;
		} else {
			rb2D.velocity = new Vector2 (inputVector.x * moveSpeed, rb2D.velocity.y);
		}
	}

	//===== Move those two to moving platform with ontrigger extra
	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.tag == "Platforms")  {
			transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.transform.tag == "Platforms") {
			transform.parent = null;
		}
	}

	//===== Private Functions =====
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

	void GroundCheck() {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

		if(grounded)
			doubleJumped = false;
		anim.SetBool("Grounded", grounded);
	}

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
		
	//===== Public functions used from other scripts =====
	public void KillPlayer(bool kill) {
		if (kill) { //kill player
			//isDead = true;
			canMove = false;
			rb2D.gravityScale = 0f;
			rend.enabled = false;
		} else { //revive player
			//isDead = false;
			canMove = true;
			rb2D.gravityScale = gravityStore;
			rend.enabled = true;
			knockBack = false;
		}
	}

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
	}
	*/
}
