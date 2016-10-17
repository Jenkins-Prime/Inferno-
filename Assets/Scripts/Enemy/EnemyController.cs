using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : RaycastController {
	public LayerMask playerMask;
	public int damageAmount = 1;
	public CollisionInfo collisions;

	Player player;
	LevelManager levelManager;

	public override void Start() {
		base.Start ();

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}
	
	public void Move (Vector3 velocity) {
		UpdateRaycastOrigins ();
		collisions.Reset ();

		if (velocity.y != 0)
			VerticalCollisions (ref velocity);
		if (velocity.x != 0)
			HorizontalCollisions (ref velocity);
			
		if (collisions.damagePlayer && player.canMove && !player.knockBack) {
			player.PlayerKnockBack (transform.position);
			levelManager.DecreaseHealth (damageAmount);
		}

		transform.Translate (velocity);
	}

	void HorizontalCollisions(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + skinWidth;

		Vector2 rayOrigin;
		RaycastHit2D hit;
		for (int i = 0; i < horizontalRayCount - 1; i++) {
			rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			if (hit) {
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collisions.left = (directionX == -1);
				collisions.right = (directionX == 1);
			}

			//Detect Player
			hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, playerMask);
			if (hit) {
				collisions.damagePlayer = true;
			}
		}

		rayOrigin = (directionX == -1) ? raycastOrigins.topLeft : raycastOrigins.topRight;
		hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
		if (hit) {
			collisions.wallInFront = true;
		} else if((collisions.left || collisions.right) && collisions.below) {
			collisions.canJump = true;
		}

		//Detect Player
		hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, playerMask);
		if (hit) {
			collisions.damagePlayer = true;
		}
	}

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
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;
		public bool canJump;
		public bool wallInFront;
		public bool damagePlayer;

		public void Reset() {
			above = below = false;
			left = right = false;
			canJump = false;
			wallInFront = false;
			damagePlayer = false;
		}
	}
}
