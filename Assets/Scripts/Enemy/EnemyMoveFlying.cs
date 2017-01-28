﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFlying : EnemyMove {
	protected override void Start () {
		base.Start ();
	}

	protected override void Update () {
		Move ();
		Animate ();
	}

	//===== Movement Method =====
	protected override void Move() {
		//Check state and update move direction
		moveDirection = Vector2.zero;
		state = MoveState.Idle;

		if (enemyChase != null) {
			if (enemyChase.Chase (ref moveDirection)) {
				state = MoveState.Chase;	
			}
		}

		if (enemyPatrol != null && state != MoveState.Chase) {
			if (enemyPatrol.Patrol (ref moveDirection, controller.collisions.wallInFront || controller.collisions.above || controller.collisions.below)) {
				state = MoveState.Patrol;
			}
		}

		//Update velocity and move
		velocity = moveDirection * moveSpeed;
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

		anim.SetBool("isMoving", moveDirection != Vector2.zero);
	}
}
