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
    public LayerMask enemyLayer;

    [SerializeField]
    private float possessDistance;

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

    private bool isPossessed;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<ActorController>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Start ()
    {
		gravity = -(2f * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2f);
		minJumpVelocity = Mathf.Sqrt (2f * Mathf.Abs (gravity) * minJumpHeight);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		knockBackTimer = 0f;
		canMove = true;
        isPossessed = false;

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

            if (InputManager.Instance.PossessEnemy() && !isPossessed)
            {
                ShootBolt();
            }
        }

        if (Input.GetKeyDown(KeyCode.N) && isPossessed)
        {
            EjectEnemy();
            isPossessed = false;
            
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
		}
        else
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
		}
        else
        {
			knockBackVelocity = new Vector2 (knockBackSpeed, knockBackSpeed);		
		}

		audioSource.PlayOneShot (hurtClip, 1f);
	}

    private void ShootBolt()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, possessDistance, enemyLayer);

        //bool isHit = Physics2D.Raycast(bolt.transform.position, Vector2.right, 100.0f, enemyLayer);

        if (hitInfo.transform != null)
        {
            if (hitInfo.collider.tag == "Enemy")
            {
                transform.parent = hitInfo.transform;
                transform.position = hitInfo.transform.position;
               // hitInfo.transform.gameObject.GetComponent<EnemyManipulate>().controlMode = true;
                PossessEnemy();
            }
        }
    }

    private void PossessEnemy()
    {
        rend.enabled = false;
        isPossessed = true;
        canMove = false;
    }

    private void EjectEnemy()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, possessDistance, enemyLayer);
        hitInfo.transform.gameObject.GetComponent<EnemyManipulate>().controlMode = false;
        rend.enabled = true;
        isPossessed = false;
        canMove = true;
        transform.position = new Vector2(transform.position.x - 1.0f, transform.position.y);
        transform.parent = null;
    }

    //private void ControlEnemy()
    //{
    //    Ray2D ray = new Ray2D(transform.position, Vector2.right);
    //    RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction, possessDistance);

    //    if (hitInfo.collider.tag == "Enemy")
    //    {
    //        hitInfo.transform.GetComponent<EnemyManipulate>().controlMode = true;
    //        transform.GetComponent<Player>().enabled = false;
    //        rend.enabled = false;
    //    }
    //}


}
