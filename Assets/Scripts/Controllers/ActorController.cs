using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ActorController : RaycastController {
	const float wallClimbOffset = 0.002f; //It is used to extend the ray a bit more (for wall climbing)

	float maxClimbAngle = 80;
	float maxDescendAngle = 75;

	Rigidbody2D rb;
	public CollisionInfo collisions;
	public Vector2 playerInput; //Used for CameraController

	protected override void Awake () {
		base.Awake ();

		//Set up rigidbody
		rb = GetComponent<Rigidbody2D> ();
		if (rb == null)
			rb = gameObject.AddComponent<Rigidbody2D> ();
		rb.isKinematic = true;
	}

	protected override void Start () {
		base.Start ();
	}

	public void Move(Vector3 velocity,bool standingOnPlatform) {
		Move (velocity, Vector2.zero, standingOnPlatform);
	}

	public void Move(Vector3 velocity, Vector2 input,bool standingOnPlatform = false) {
		UpdateRaycastOrigins ();
		collisions.Reset (velocity);
		playerInput = input;

		if (velocity.y < 0) {
			DescendSlope (ref velocity);
		}

		if (velocity.x != 0)
			HorizontalCollisions (ref velocity);

		if (velocity.y != 0)
			VerticalCollisions (ref velocity);

		transform.Translate (velocity);


		if (standingOnPlatform) { //Check this at later point
			collisions.below = true;
		}
	}

	public void MoveClimb(Vector3 velocity, bool standingOnPlatform = false) {
		UpdateRaycastOrigins ();
		collisions.ResetWall (velocity);

		//add a check if itsn't attached to a wall, to disable climbing mode
		//run the horizontalcollisionsclimb better to always be on the wall

		ClimbCheck (velocity);

		if (velocity.x != 0f)
			HorizontalCollisionsClimb (ref velocity);

		if (velocity.y != 0f)
			VerticalCollisionsClimb (ref velocity);

		transform.Translate (velocity);
	}

	public void ClimbCheck(Vector3 velocity) { //could make an overloaded method with void params to init all raycasts
		float rayLength = skinWidth + wallClimbOffset;

		bool topLeftX, topLeftY;
		bool topRightX, topRightY;
		bool bottomLeftX, bottomLeftY;
		bool bottomRightX, bottomRightY;

		//Up-Left corner
		topLeftX = Physics2D.Raycast ((Vector3)raycastOrigins.topLeft + velocity, Vector2.right * (-1.0f), rayLength, collisionMask);
		topLeftY = Physics2D.Raycast ((Vector3)raycastOrigins.topLeft + velocity, Vector2.up * (1.0f), rayLength, collisionMask);

		Debug.DrawRay ((Vector3)raycastOrigins.topLeft + velocity, Vector2.right * (-1.0f) * rayLength, Color.blue);
		Debug.DrawRay ((Vector3)raycastOrigins.topLeft + velocity, Vector2.up * (1.0f) * rayLength, Color.blue);

		//RaycastHit2D inf = Physics2D.Raycast ((Vector3)raycastOrigins.topLeft + velocity, Vector2.up * (1.0f), rayLength, collisionMask);
		//Debug.Log (inf.distance);
		//Up-right corner
		topRightX = Physics2D.Raycast ((Vector3)raycastOrigins.topRight + velocity, Vector2.right * (1.0f), rayLength, collisionMask);
		topRightY = Physics2D.Raycast ((Vector3)raycastOrigins.topRight + velocity, Vector2.up * (1.0f), rayLength, collisionMask);

		Debug.DrawRay ((Vector3)raycastOrigins.topRight + velocity, Vector2.right * (1.0f) * rayLength, Color.blue);
		Debug.DrawRay ((Vector3)raycastOrigins.topRight + velocity, Vector2.up * (1.0f) * rayLength, Color.blue);

		//Down-Left corner
		bottomLeftX = Physics2D.Raycast ((Vector3)raycastOrigins.bottomLeft + velocity, Vector2.right * (-1.0f), rayLength, collisionMask);
		bottomLeftY = Physics2D.Raycast ((Vector3)raycastOrigins.bottomLeft + velocity, Vector2.up * (-1.0f), rayLength, collisionMask);

		Debug.DrawRay ((Vector3)raycastOrigins.bottomLeft + velocity, Vector2.right * (-1.0f) * rayLength, Color.blue);
		Debug.DrawRay ((Vector3)raycastOrigins.bottomLeft + velocity, Vector2.up * (-1.0f) * rayLength, Color.blue);

		//Down-right corner
		bottomRightX = Physics2D.Raycast ((Vector3)raycastOrigins.bottomRight + velocity, Vector2.right * (1.0f), rayLength, collisionMask);
		bottomRightY = Physics2D.Raycast ((Vector3)raycastOrigins.bottomRight + velocity, Vector2.up * (-1.0f), rayLength, collisionMask);

		Debug.DrawRay ((Vector3)raycastOrigins.bottomRight + velocity, Vector2.right * (1.0f) * rayLength, Color.blue);
		Debug.DrawRay ((Vector3)raycastOrigins.bottomRight + velocity, Vector2.up * (-1.0f) * rayLength, Color.blue);

		/*
		collisions.wallMoveLeft = (!topLeftX && topLeftY) || (!bottomLeftX && bottomLeftY);
		collisions.wallMoveRight = (!topRightX && topRightY) || (!bottomRightX && bottomRightY);
		collisions.wallMoveDown = (bottomLeftX && !bottomLeftY) || (bottomRightX && !bottomRightY);
		collisions.wallMoveUp = (topLeftX && !topLeftY) || (topRightX && !topRightY);
		*/


		collisions.wallMoveLeft = topLeftY || bottomLeftY;
		collisions.wallMoveRight = topRightY || bottomRightY;
		collisions.wallMoveUp = topLeftX || topRightX;
		collisions.wallMoveDown = bottomLeftX || bottomRightX;
	}

	void HorizontalCollisions(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;

		Vector2 rayOrigin;
		RaycastHit2D hit;

		for (int i = 0; i < horizontalRayCount; i++) {
			rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
			Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLength, Color.red);

			if (hit) {

				if (hit.distance == 0) {
					continue;
				}

				float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);

				if (i == 0 && slopeAngle <= maxClimbAngle) {
					if (collisions.descendingSlope) {
						collisions.descendingSlope = false;
						velocity = collisions.velocityOld;
					}
					float distanceToSlopeStart = 0f;
					if (slopeAngle != collisions.slopeAngleOld) {
						distanceToSlopeStart = hit.distance - skinWidth;
						velocity.x -= distanceToSlopeStart * directionX;
					}

					ClimbSlope (ref velocity, slopeAngle);
					velocity.x += distanceToSlopeStart * directionX;
				}

				if (!collisions.climbingSlope || slopeAngle > maxClimbAngle) {

					velocity.x = (hit.distance - skinWidth) * directionX;
					rayLength = hit.distance;


					if (collisions.climbingSlope) {
						velocity.y = Mathf.Tan (collisions.slopeAngle * Mathf.Deg2Rad * Mathf.Abs (velocity.x));
					}


					collisions.left = (directionX == -1);
					collisions.right = (directionX == 1);
				}
			}
		}

		//Used for enemy patrolling == Change this to a seperate part
		rayOrigin = (directionX == -1) ? raycastOrigins.topLeft : raycastOrigins.topRight;
		hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
		if (hit) {
			collisions.wallInFront = true;
		} else if ((collisions.left || collisions.right) && collisions.below) {
			collisions.canJump = true;
		}
	}

	void VerticalCollisions(ref Vector3 velocity) {
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLength, Color.red);
			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;


				if (collisions.climbingSlope) {
					velocity.x = velocity.y / Mathf.Tan (collisions.slopeAngle * Mathf.Deg2Rad * Mathf.Sign (velocity.x));
				}

				collisions.below = (directionY == -1);
				collisions.above = (directionY == 1);
			}
		}


		if (collisions.climbingSlope) {
			float directionX = Mathf.Sign (velocity.x);
			rayLength = Mathf.Abs (velocity.x) + skinWidth;
			Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * velocity.y;
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			if (hit) {
				float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);
				if (slopeAngle != collisions.slopeAngle) {
					velocity.x = (hit.distance - skinWidth) * directionX;
					collisions.slopeAngle = slopeAngle;
				}
			}
		}
	}


	void HorizontalCollisionsClimb(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;
		Vector2 rayOrigin;
		RaycastHit2D hit;

		for (int i = 0; i < horizontalRayCount; i++) {
			rayOrigin = (directionX == -1f) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
			Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLength, Color.red);

			if (hit) {
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collisions.left = (directionX == -1);
				collisions.right = (directionX == 1);
			}
		}
	}

	void VerticalCollisionsClimb(ref Vector3 velocity) {
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;
		Vector2 rayOrigin;
		RaycastHit2D hit;

		for (int i = 0; i < verticalRayCount; i++) {
			rayOrigin = (directionY == -1f) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
			Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLength, Color.red);

			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				collisions.below = (directionY == -1);
				collisions.above = (directionY == 1);
			}
		}
	}

	void ClimbSlope(ref Vector3 velocity, float slopeAngle) {
		float moveDistance = Mathf.Abs (velocity.x); //find the x distance needed to move
		float climbVelocityY = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDistance; 

		if (velocity.y <= climbVelocityY) {
			velocity.y = climbVelocityY;
			velocity.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (velocity.x);
			collisions.below = true;
			collisions.climbingSlope = true;
			collisions.slopeAngle = slopeAngle;
		}
	}

	void DescendSlope(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

		if (hit) {
			float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);
			if (slopeAngle != 0 && slopeAngle <= maxDescendAngle) {
				if (Mathf.Sign (hit.normal.x) == directionX) {
					if (hit.distance - skinWidth <= Mathf.Tan (slopeAngle * Mathf.Deg2Rad * Mathf.Abs (velocity.x))) {
						float moveDistance = Mathf.Abs (velocity.x);
						float descendVelocityY = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDistance;
						velocity.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (velocity.x);
						velocity.y -= descendVelocityY;

						collisions.slopeAngle = slopeAngle;
						collisions.descendingSlope = true;
						collisions.below = true;
					}
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		switch (other.tag) {
		case "Enemy":
			/*DamagePlayer dmg = other.GetComponent<DamagePlayer> ();
			if (dmg != null)
				dmg.DealDamage ();
			*/
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
		}
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;
		public bool climbingSlope;
		public bool descendingSlope;

		public bool wallMoveLeft, wallMoveRight;
		public bool wallMoveDown, wallMoveUp;

		public bool wallInFront;
		public bool canJump;

		public float slopeAngle, slopeAngleOld;
		public Vector3 velocityOld;

		public void Reset(Vector3 vel) {
			above = below = false;
			left = right = false;
			climbingSlope = false;
			descendingSlope = false;

			wallInFront = false;
			canJump = false;

			slopeAngleOld = slopeAngle;
			slopeAngle = 0;

			velocityOld = vel;
		}

		public void ResetWall(Vector3 vel) {
			above = below = false;
			left = right = false;

			wallMoveLeft = wallMoveRight = false;
			wallMoveDown = wallMoveUp = false;

			velocityOld = vel;
		}
	}
}
