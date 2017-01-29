using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ActorController))]
public class Player : MonoBehaviour {
	[Header("Movement Variables")]
	[SerializeField] float moveSpeed = 3f;
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

	float knockBackTimer;
	Vector2 knockBackVelocity;

	Animator anim;
	AudioSource audioSource;
	ActorController controller;
	SpriteRenderer rend;

	void Start () {
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		controller = GetComponent<ActorController> ();
		rend = GetComponent<SpriteRenderer> ();

		gravity = -(2f * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2f);
		minJumpVelocity = Mathf.Sqrt (2f * Mathf.Abs (gravity) * minJumpHeight);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		knockBackTimer = 0f;
		canMove = true;
	}

	void Update ()
    {
		if (canMove)
        {
            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }
				
			input = InputManager.Instance.MainStick ();

			JumpCheck ();
			Move ();
			SetAnimatorStates ();
		}
	}

	bool KnockBackCheck()
    {
		if (knockBack)
        {
			if (knockBackTimer > 0)
            {
				knockBackTimer -= Time.deltaTime;
			} else
            {
				knockBack = false;
			}
		}
		return !knockBack; //true if knockback ended
	}

	void JumpCheck () {
		if (!controller.collisions.onLadder)
        {
			if (InputManager.Instance.JumpButton ())
            {
				if (controller.collisions.below || controller.collisions.onLadderAbove)
                {
					velocity.y = maxJumpVelocity;
					jump = true;
					audioSource.PlayOneShot (jumpClip, 1.0f);
				}
			}

			if (InputManager.Instance.ReleaseJumpButton ())
            {
				if (velocity.y > minJumpVelocity)
                {
					velocity.y = minJumpVelocity;
				}

				jump = false;
			}
		}
	}

	void Move()
    {
		if (KnockBackCheck ())
        {
			float targetVelocityX;
		    targetVelocityX = input.x * moveSpeed;
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
			velocity.y += gravity * Time.deltaTime;
		}
        else
        {
			velocity = knockBackVelocity;
		}

		controller.Move (velocity * Time.deltaTime, input);
	}

	void SetAnimatorStates() {
		//Turn sprite
		rend.flipX = (velocity.x < 0) ? true : false;

		anim.SetFloat("Speed", Mathf.Abs(input.x));

        if (controller.collisions.below)
        {
            anim.SetBool("Grounded", true);
        }
		else
        {
			anim.SetBool ("Grounded", false);
		}
	}

	//===== Public functions used from other scripts =====
	public void KillPlayer(bool kill)
    {
		if (kill)
        { //kill player
			canMove = false;
			velocity = Vector3.zero;
			rend.enabled = false;
		} else
        { //revive player
			canMove = true;
			rend.enabled = true;
			knockBack = false;
		}
	}


	public void PlayerKnockBack(Vector3 attacker)
    {
		knockBack = true;
		knockBackTimer = knockBackLength;

		if (transform.position.x < attacker.x)
        {
			knockBackVelocity = new Vector2 (-knockBackSpeed, knockBackSpeed);
		} else
        {
			knockBackVelocity = new Vector2 (knockBackSpeed, knockBackSpeed);		
		}

		audioSource.PlayOneShot (hurtClip, 1f);
	}
}
