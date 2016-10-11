using UnityEngine;
using System.Collections;

[RequireComponent(typeof (EnemyController))]
public class EnemyMovement : MonoBehaviour {
	[SerializeField] Vector3 localWaypointStart;
	[SerializeField] Vector3 localWaypointEnd;
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] float jumpHeight = 0.2f;
	[SerializeField] float timeToJumpApex = 0.1f;

	Vector3 velocity;
	Vector3 globalWaypointLeft;
	Vector3 globalWaypointRight;
	Vector3 targetWaypoint;
	float moveDirection;
	float gravity;
	float jumpVelocity;

	SpriteRenderer rend;
	EnemyController controller;

	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		controller = GetComponent<EnemyController> ();

		if (localWaypointEnd.x < localWaypointStart.x) {
			globalWaypointLeft = localWaypointEnd + transform.position;
			globalWaypointRight = localWaypointStart + transform.position;
			targetWaypoint = globalWaypointLeft;
			moveDirection = Mathf.Sign (localWaypointEnd.x - localWaypointStart.x);
		} else if (localWaypointEnd.x > localWaypointStart.x) {
			globalWaypointLeft = localWaypointStart + transform.position;
			globalWaypointRight = localWaypointEnd + transform.position;
			targetWaypoint = globalWaypointRight;
			moveDirection = Mathf.Sign (localWaypointEnd.x - localWaypointStart.x);
		} else {
			moveDirection = 0;
		}
			
		rend.flipX = (moveDirection == -1);

		gravity = -(2f * jumpHeight) / Mathf.Pow (timeToJumpApex, 2f);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	void Update () {
		if ((moveDirection != 0) && (Mathf.Abs(transform.position.x - targetWaypoint.x) < 0.1f || controller.collisions.wallInFront)) {
			ChangeEnemyDirection ();
		}

		if (controller.collisions.above || controller.collisions.below)
			velocity.y = 0f;

		if (controller.collisions.canJump) {
			velocity.y = jumpVelocity;
		}

		velocity.x = moveDirection * moveSpeed;
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}

	public void ChangeEnemyDirection() {
		if (moveDirection == -1) {
			targetWaypoint = globalWaypointRight;
			moveDirection = 1;
			rend.flipX = false;
		} else {
			targetWaypoint = globalWaypointLeft;
			moveDirection = -1;
			rend.flipX = true;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		if (Application.isPlaying) {
			Gizmos.DrawLine (globalWaypointLeft, globalWaypointRight);
		} else {
			Gizmos.DrawLine (localWaypointStart + transform.position, localWaypointEnd + transform.position);
		}
	}
}
