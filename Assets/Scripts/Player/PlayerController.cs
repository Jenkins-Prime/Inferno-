using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	//Change Notes: 
	//Changed any public variable that has no use to be accessed by other scripts to a private, if the variable should be changed in the editor then added [SerializeField].
	//Changed GroundCheck to Update, since input is handled in update
	//Changed any movement line to the fixedupdate, in order to achieve a smooth movement.
	//Add a lot of functions in order to make update() function easy to read.
	//Removed any GetComponent<>() call in the update, it's less cpu heavy to cache a variable in the start()

	[Header("Movement Variables")]
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] float jumpHeight = 3.5f;
	[SerializeField] float knockBackSpeed = 2f;
	[SerializeField] float climbSpeed = 2f;
// <<<<<<< HEAD
// =======

// >>>>>>> Cleaning_up_the_code

	[Header("Check Variables")]
	[SerializeField] LayerMask groundLayer;
	[SerializeField] Transform groundCheck;
	[SerializeField] float groundCheckRadius = 0.1f;
	[SerializeField] float knockBackLength = 0.2f;

	[Header("Head Stomp Variables")]
	[SerializeField] int damageToGive;
	[SerializeField] float enemyBounceHeight = 2f;

	[Header("Ranged Attack Variables")]
	public Transform firePoint;
	public GameObject bullet;
	[SerializeField] float shotDelay = 2f;

	[Header("Other")]
	public AudioClip jumpClip;

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
	AudioSource audio;
	Rigidbody2D rb2D;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
		rb2D = GetComponent<Rigidbody2D>();

		gravityStore = rb2D.gravityScale;
		knockBackTimer = 0f;
	}

	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 0f) { //change this to a bool isPaused on LevelManager script
			return;
		}

		if (KnockBackCheck ()) {
			inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //GetInput
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
		if (knockBack) {
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

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy")  {
			//Add a check to see if enemy can be hurt with headstomp
			other.GetComponent<EnemyHealthManager> ().giveDamage (damageToGive);
			rb2D.velocity = new Vector2 (rb2D.velocity.x, enemyBounceHeight);
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
		if(Input.GetButtonDown("Jump")) { //Jump Check
			if(grounded) { //First jump
				audio.PlayOneShot(jumpClip, 1.0f);
				jump = true;
			} else if(!doubleJumped) { //Second Jump
				audio.PlayOneShot(jumpClip, 0.5f);
				doubleJumped = true;
				jump = true;
			}
		}
	}

	void AttackCheck() {
		if (Input.GetButtonDown ("Fire1")) {
			anim.SetBool ("Firing", true);

			if (shotTimer > 0) {
				shotTimer -= Time.deltaTime; //change this to time.delta
			} else {
				Instantiate (bullet, firePoint.position, firePoint.rotation); //change this to .enable for cpu optimization
				shotTimer = shotDelay;
			}
		} else if (Input.GetButtonDown ("Fire2")) {
			//Melee code here
			//anim.SetBool("Sword", true);
		}
	}
		
	//===== Public functions used from other scripts =====
	public void PlayerKnockBack(Vector3 attacker) {
		knockBack = true;
		knockBackTimer = knockBackLength;

		if(transform.position.x < attacker.x) {
			knockBackVelocity = new Vector2(-knockBackSpeed, knockBackSpeed);
		} else {
			knockBackVelocity = new Vector2(knockBackSpeed, knockBackSpeed);		
		}
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
