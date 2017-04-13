using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveWall : EnemyMove {
	[SerializeField] bool isClimbing = false;
	[SerializeField] float jumpHeight = 0.4f;
	[SerializeField] float timeToJumpApex = 0.2f;

	float gravity;
	float jumpVelocity;

	void Awake() {
		gravity = -(2f * jumpHeight) / Mathf.Pow (timeToJumpApex, 2f);
	}

	protected override void Start() {
		base.Start ();

		controller.MoveClimb (Vector3.zero, false);
	}

	protected override void Update () {
		if (isClimbing)
			Climb ();
		else
			Move ();
		
		Animate ();
	}
		
	protected override void Move() {
		moveDirection = Vector2.zero;

		if (enemyPatrol != null) {
			if (enemyPatrol.Patrol (ref moveDirection.x, controller.collisions.wallInFront)) {
				state = MoveState.Patrol;
			}
		}

		//Update velocity and move
		velocity.x = moveDirection.x * moveSpeed;

		if (controller.collisions.above || controller.collisions.below)
			velocity.y = 0;

		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, false);
	}

	void Climb() {
		moveDirection = Vector2.zero; //maybe replace with gravited content

		if (enemyPatrol != null) {
			if (enemyPatrol.Patrol (ref moveDirection, controller.collisions.wallMoveLeft, controller.collisions.wallMoveRight, controller.collisions.wallMoveUp, controller.collisions.wallMoveDown, controller.collisions.left, controller.collisions.right, controller.collisions.above, controller.collisions.below)) {
				state = MoveState.Patrol;
				//project the moveDirection
			}
		}

		//Update velocity and move
		velocity = moveDirection * moveSpeed;
		//Debug.Log (velocity * Time.deltaTime);
		controller.MoveClimb (velocity * Time.deltaTime, false);
	}

	protected override void Animate() {

	}
}
