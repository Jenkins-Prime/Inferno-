using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveGround : EnemyMove {
	[SerializeField] float jumpHeight = 0.4f;
	[SerializeField] float timeToJumpApex = 0.2f;

	float gravity;
	float jumpVelocity;

	void Awake () {
		//Set up gravity and jump parameters
		gravity = -(2f * jumpHeight) / Mathf.Pow (timeToJumpApex, 2f);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	protected override void Update () {
		Move ();
		Animate ();
	}

	//===== Movement Method =====
	protected override void Move() {
		//Check state and update move direction
		moveDirection.x = 0f;
		state = MoveState.Idle;

		if (enemyManipulate != null) {
			if (enemyManipulate.CanControlEnemy ()) {
				enemyManipulate.GetInput (ref moveDirection.x, ref controller.collisions.canJump);
				state = MoveState.Manipulate;
			}
		}

		if (state != MoveState.Manipulate) {
			if (enemyChase != null) {
				if (enemyChase.Chase (ref moveDirection.x)) {
					state = MoveState.Chase;	
				}
			}

			if (enemyPatrol != null && state != MoveState.Chase) {
				if (enemyPatrol.Patrol (ref moveDirection.x, controller.collisions.wallInFront)) { //change that pls
					state = MoveState.Patrol;
				}
			}
		}

		//Update velocity and move
		velocity.x = moveDirection.x * moveSpeed;

		if (controller.collisions.above || controller.collisions.below) //Don't go through floors/ceilings
			velocity.y = 0f;
		if (controller.collisions.canJump)
			velocity.y = jumpVelocity;
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
