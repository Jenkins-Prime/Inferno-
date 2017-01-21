using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveGround : EnemyMove {
	[SerializeField] float jumpHeight = 0.2f;
	[SerializeField] float timeToJumpApex = 0.1f;

	float gravity;
	float jumpVelocity;

	void Awake () {
		//Set up gravity and jump parameters
		gravity = -(2f * jumpHeight) / Mathf.Pow (timeToJumpApex, 2f);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	protected override void Start () {
		base.Start();

		PatrolInit ();
	}

	protected override void Update () {
		GravityCheck (); //Handle gravity (can be done in playercontroller?)

		if(!ChaseCheck()) //If detects player then chase him
			PatrolCheck(); //else patrol

		Move ();
		AnimateEnemy ();
	}

	void GravityCheck() {
		if (controller.collisions.above || controller.collisions.below)
			velocity.y = 0f;

		if (controller.collisions.canJump) {
			velocity.y = jumpVelocity;
		}
	}

	//===== Patrolling Methods =====
	protected override void PatrolInit() {
		if (patrol != null)
			moveDirection.x = patrol.SetMoveDirection ().x;
	}

	protected override void PatrolCheck() {
		if (patrol != null)
			moveDirection.x = patrol.UpdateMoveDirection (moveDirection.x, controller.collisions.wallInFront);
	}

	//===== Chasing Method =====
	protected override bool ChaseCheck() {
		if (chase != null) {
			if (chase.DetectPlayer (ref moveDirection.x)) { //If it detects player then chase him
				return true;
			} else if (chase.IsChasing()) { //else if player has escape then return back to previous point
				if (patrol == null) {
					chase.ReturnFromChase (ref moveDirection.x, transform.position); //stop if there is not a patrol component
				} else {
					chase.ReturnFromChase (ref moveDirection.x, patrol.GetNextWaypoint ());
				}

				return true;
			}
		}

		return false;
	}

	//===== Movement Method =====
	protected override void Move() {
		velocity.x = moveDirection.x * moveSpeed;
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}

	//===== Animation Method =====
	protected override void AnimateEnemy() {
		rend.flipX = (moveDirection.x == -1); //Update sprite
		anim.SetBool("isMoving", moveDirection.x != 0f);
	}
}
