using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : ActorController {
	public LayerMask playerMask;
	public int damageAmount = 1;

	public bool canJump;
	public bool wallInFront;
	public bool damagePlayer;

	Player player;
	//LevelManager levelManager; replace health on player's function calls

	protected override void Start() {
		base.Start ();

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		//levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}

	public override void Move(Vector3 velocity,bool standingOnPlatform) {
		Move (velocity, Vector2.zero, standingOnPlatform);
	}

	public override void Move(Vector3 velocity, Vector2 input,bool standingOnPlatform = false) {
		UpdateRaycastOrigins ();
		collisions.Reset (velocity);
		canJump = false;
		wallInFront = false;
		damagePlayer = false;

		if (velocity.y < 0) {
			DescendSlope (ref velocity);
		}
		if (velocity.x != 0) {
			HorizontalCollisions (ref velocity);
		}
		if (velocity.y != 0) {
			VerticalCollisions (ref velocity);
		}
			
		if (damagePlayer && player.canMove && !player.knockBack) {
			player.PlayerKnockBack (transform.position);
			//levelManager.DecreaseHealth (damageAmount);
		}

		transform.Translate (velocity);
	}
		
	protected override void HorizontalCollisions(ref Vector3 velocity) {
		base.HorizontalCollisions (ref velocity);

		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + skinWidth;

		Vector2 rayOrigin;
		RaycastHit2D hit;

		rayOrigin = (directionX == -1) ? raycastOrigins.topLeft : raycastOrigins.topRight;
		hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
		if (hit) {
			wallInFront = true;
		} else if((collisions.left || collisions.right) && collisions.below) {
			canJump = true;
		}

		//Detect Player
		hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, playerMask);
		if (hit) {
			damagePlayer = true;
		}
	}

	/*
	void VerticalCollisions(ref Vector3 velocity) {
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				collisions.below = (directionY == -1);
				collisions.above = (directionY == 1);
			}

			//Detect Player
			hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, playerMask);
			if (hit) {
				collisions.damagePlayer = true;
			}
		}
	}*/
}
