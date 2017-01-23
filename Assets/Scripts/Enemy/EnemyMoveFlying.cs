using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFlying : EnemyMove {
	protected override void Start () {
		base.Start ();

		PatrolInit ();
	}

	protected override void Update () {
		if(!ChaseCheck()) //If detects player then chase him
			PatrolCheck(); //else patrol

		Move ();
		AnimateEnemy ();
	}

	//===== Patrolling Methods =====
	protected override void PatrolInit() {
		if (patrol != null)
			moveDirection = patrol.SetMoveDirection ();
	}

	protected override void PatrolCheck() {
		if (patrol != null)
			moveDirection = patrol.UpdateMoveDirection (moveDirection, controller.wallInFront, controller.collisions.above || controller.collisions.below);
	}

	//===== Chasing Methods =====
	protected override bool ChaseCheck() {
		if (chase != null) {
			if (chase.DetectPlayer (ref moveDirection)) { //If it detects player then chase him
				return true;
			} else if (chase.IsChasing()) { //else if player has escape then return back to previous point
				if (patrol == null) {
					chase.ReturnFromChase (ref moveDirection, transform.position); //stop if there is not a patrol component
				} else {
					chase.ReturnFromChase (ref moveDirection, patrol.GetNextWaypoint ());
				}

				return true;
			}
		}

		return false;
	}

	//===== Movement Method =====
	protected override void Move() {
		velocity = moveDirection * moveSpeed;
		controller.Move (velocity * Time.deltaTime, false);
	}

	//===== Animation Method =====
	protected override void AnimateEnemy() {
		rend.flipX = (moveDirection.x == -1); //Update sprite
		anim.SetBool("isMoving", moveDirection != Vector2.zero);
	}
}
