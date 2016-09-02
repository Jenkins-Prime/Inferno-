using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerData))]

public class PlayerController : MonoBehaviour {
	public PlayerData playerData;

	[SerializeField] LayerMask groundLayer;
	[SerializeField] Transform groundCheck;
	[SerializeField] float groundCheckRadius = 0.1f;
	[SerializeField] Transform firePoint;

	bool isDead;
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
	}

	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 0f) { //change this to a bool isPaused on LevelManager script
			return;
		}

		if (isDead) { //improve this
			return;
		}

		if (KnockBackCheck ()) {
			inputVector = InputManager.MainStick(); //GetInput
			anim.SetFloat("Speed", Mathf.Abs(inputVector.x));

			GroundCheck ();
			JumpCheck ();

			//Turn sprite
			if (rb2D.velocity.x >= 0)
				transform.localScale = new Vector3(1f, 1f, 1f);
			else
				transform.localScale = new Vector3(-1f, 1f, 1f);

			AttackCheck();
		}
    }

	void FixedUpdate() {
		if (isDead) {
			rb2D.velocity = Vector2.zero;
		} else if (knockBack) {
			rb2D.velocity = knockBackVelocity;
		} else if(onLadder) { 
			rb2D.velocity = new Vector2(inputVector.x * playerData.moveSpeed, inputVector.y * playerData.climbSpeed);
		} else if (jump) {
			rb2D.velocity = new Vector2 (inputVector.x * playerData.moveSpeed, playerData.jumpHeight);
			jump = false;
		} else {
			rb2D.velocity = new Vector2 (inputVector.x * playerData.moveSpeed, rb2D.velocity.y);
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
	//=====

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy")  {
			//Add a check to see if enemy can be hurt with headstomp
			other.GetComponent<EnemyHealthManager> ().giveDamage (playerData.damageToGive);
			rb2D.velocity = new Vector2 (rb2D.velocity.x, playerData.enemyBounceHeight);
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
		if(InputManager.JumpButton()) { //Jump Check
			if(grounded) { //First jump
				audioSource.PlayOneShot(playerData.jumpClip, 1.0f);
				jump = true;
			} else if(!doubleJumped) { //Second Jump
				audioSource.PlayOneShot(playerData.jumpClip, 0.5f);
				doubleJumped = true;
				jump = true;
			}
		}
	}

	void AttackCheck() {
		if (InputManager.FireButton()) {
			anim.SetBool ("Firing", true);

			if(shotTimer <= 0) { //change this to bool values
				Instantiate (playerData.bullet, firePoint.position, firePoint.rotation); //change this to .enable for cpu optimization
				shotTimer = playerData.shotDelay;
			}
		} else if (InputManager.MeleeButton()) {
			//Melee code here
			//anim.SetBool("Sword", true);
		}

		//Move To a better place
		if (shotTimer > 0)
			shotTimer -= Time.deltaTime;
	}
		
	//===== Public functions used from other scripts =====
	public void KillPlayer(bool kill) {
		if (kill) { //kill player
			isDead = true;
			rb2D.gravityScale = 0f;
			rend.enabled = false;
		} else { //revive player
			isDead = false;
			rb2D.gravityScale = gravityStore;
			rend.enabled = true;
			knockBack = false;
		}
	}

	public void PlayerKnockBack(Vector3 attacker) {
		knockBack = true;
		knockBackTimer = playerData.knockBackLength;

		if(transform.position.x < attacker.x) {
			knockBackVelocity = new Vector2(-playerData.knockBackSpeed, playerData.knockBackSpeed);
		} else {
			knockBackVelocity = new Vector2(playerData.knockBackSpeed, playerData.knockBackSpeed);		
		}

		audioSource.PlayOneShot (playerData.hurtClip, 1f);
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
}
