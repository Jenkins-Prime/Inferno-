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
	}

	protected override void Update () {
		GravityCheck (); //Handle gravity

		Move ();
		Animate ();
	}

	void GravityCheck() {
		if (controller.collisions.above || controller.collisions.below)
			velocity.y = 0f;

		if (controller.collisions.canJump) {
			velocity.y = jumpVelocity;
		}
	}

	//===== Movement Method =====
	protected override void Move() {
		//Check state and update move direction
		moveDirection.x = 0f;
		state = MoveState.Idle;

		if (enemyChase != null) {
			if (enemyChase.Chase (ref moveDirection.x)) {
				state = MoveState.Chase;	
			}
		}

		if (enemyPatrol != null && state != MoveState.Chase) {
			if (enemyPatrol.Patrol (ref moveDirection.x, controller.collisions.wallInFront)) {
				state = MoveState.Patrol;
			}
		}

		//Update velocity and move
		velocity.x = moveDirection.x * moveSpeed;
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, false);
	}

	//===== Animation Method =====
	protected override void Animate() {
		//Update Sprite
		if (moveDirection.x > 0f)
			rend.flipX = false;
		else if (moveDirection.x < 0f)
			rend.flipX = true;			
		else
			rend.flipX = initSpriteDirection;

		anim.SetBool("isMoving", moveDirection.x != 0f);
	}
}
