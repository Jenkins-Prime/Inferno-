using UnityEngine;
using System.Collections;

public class PlayerController : ActorController {
    public LayerMask block;
	[HideInInspector]
	//public Vector2 playerInput; //does it fit here?

	public bool onLadder;
	public bool onLadderBelow;
	public bool onLadderAbove;

	protected override void Start() {
		base.Start ();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            MoveBlock();
        }
    }
		
	public override void Move(Vector3 velocity,bool standingOnPlatform) {
		Move (velocity, Vector2.zero, standingOnPlatform);
	}

	public override void Move(Vector3 velocity, Vector2 input,bool standingOnPlatform = false) {
		UpdateRaycastOrigins ();
		collisions.Reset (velocity);
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

		if (standingOnPlatform) { //Check this at later point
			collisions.below = true;
		}
	}
		
	void OnTriggerEnter2D(Collider2D other) {
		switch (other.tag) {
		case "Enemy":
			DamagePlayer dmg = other.GetComponent<DamagePlayer> ();
			if (dmg != null)
				dmg.DealDamage ();
			Debug.Log ("AEnemy");
			break;
		case "Pickup":
			Pickup pickup = other.GetComponent<Pickup> ();
			if (pickup != null)
				pickup.Collect ();
			break;
		case "Checkpoint":
			Checkpoint checkpoint = other.GetComponent<Checkpoint> ();
			if (checkpoint != null)
				checkpoint.SetCheckpoint ();
			break;
		case "Ladder":
			Ladder ladder = other.GetComponent<Ladder> ();
			if (ladder != null) {
				onLadderAbove = ladder.CheckPlayerPositionAbove (transform.position);
				onLadderBelow = ladder.CheckPlayerPositionBelow (transform.position);
				onLadder = !onLadderAbove;
			}
			break;
		}
	}

    private void MoveBlock()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 10.0f, block);

        if (hit)
        {
            Debug.Log("Hit");
        }
    }

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Ladder") {
			onLadderAbove = false;
			onLadderBelow = false;
			onLadder = false;
		}
	}
}
