using UnityEngine;
using System.Collections;

[RequireComponent(typeof (EnemyController))]
public class EnemyFlyingMovement : MonoBehaviour {
	[SerializeField] Vector2 localWaypointStart;
	[SerializeField] Vector2 localWaypointEnd;
	[SerializeField] float moveSpeed = 1f;

	Vector3 velocity;
	Vector3 globalWaypointLeftDown;
	Vector3 globalWaypointRightUp;
	Vector3 targetWaypoint;
	Vector2 moveDirection;

	SpriteRenderer rend;
	EnemyController controller;

	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		controller = GetComponent<EnemyController> ();

		targetWaypoint = transform.position;
		//Check the X-Axis
		if (localWaypointEnd.x < localWaypointStart.x) {
			globalWaypointLeftDown = new Vector3(localWaypointEnd.x, 0f);
			globalWaypointRightUp = new Vector3(localWaypointStart.x, 0f);
			targetWaypoint = new Vector3(targetWaypoint.x + globalWaypointLeftDown.x, targetWaypoint.y);
		} else if (localWaypointEnd.x > localWaypointStart.x) {
			globalWaypointLeftDown = new Vector3(localWaypointStart.x, 0f);
			globalWaypointRightUp = new Vector3(localWaypointEnd.x, 0f);
			targetWaypoint = new Vector3(targetWaypoint.x + globalWaypointRightUp.x, targetWaypoint.y);
		}

		//Check the Y-Axis
		if (localWaypointEnd.y < localWaypointStart.y) {
			globalWaypointLeftDown = new Vector3 (globalWaypointLeftDown.x + transform.position.x, localWaypointEnd.y + transform.position.y);
			globalWaypointRightUp = new Vector3 (globalWaypointRightUp.x + transform.position.x, localWaypointStart.y + transform.position.y);
			targetWaypoint = new Vector3(targetWaypoint.x, globalWaypointLeftDown.y);
		} else if (localWaypointEnd.y > localWaypointStart.y) {
			globalWaypointLeftDown = new Vector3 (globalWaypointLeftDown.x + transform.position.x, localWaypointStart.y + transform.position.y);
			globalWaypointRightUp = new Vector3 (globalWaypointRightUp.x + transform.position.x, localWaypointEnd.y + transform.position.y);
			targetWaypoint = new Vector3(targetWaypoint.x, globalWaypointRightUp.y);
		}
			
		moveDirection = targetWaypoint - transform.position;
		moveDirection.Normalize ();
		
		rend.flipX = (moveDirection.x == -1);
	}

	void Update () {
		if (CheckEnemyDirectionChange ())
			ChangeEnemyDirection ();

		velocity = moveDirection * moveSpeed;
		controller.Move (velocity * Time.deltaTime);
	}

	bool CheckEnemyDirectionChange() {
		return 	((moveDirection.x != 0) && (Mathf.Abs (transform.position.x - targetWaypoint.x) < 0.1f || controller.collisions.wallInFront)) ||
				((moveDirection.y != 0) && (Mathf.Abs (transform.position.y - targetWaypoint.y) < 0.1f || controller.collisions.above || controller.collisions.below));
	}

	void ChangeEnemyDirection() {
		if (moveDirection.x < 0) {
			targetWaypoint = new Vector3(globalWaypointRightUp.x, globalWaypointRightUp.y);
			rend.flipX = false;
		} else {
			targetWaypoint = new Vector3(globalWaypointLeftDown.x, globalWaypointLeftDown.y);
			rend.flipX = true;
		}

		moveDirection = targetWaypoint - transform.position;
		moveDirection.Normalize ();
	}
	
	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		if (Application.isPlaying) {
			Gizmos.DrawLine (globalWaypointLeftDown, globalWaypointRightUp);
		} else {
			Gizmos.DrawLine (new Vector3(localWaypointStart.x, localWaypointStart.y) + transform.position, new Vector3(localWaypointEnd.x, localWaypointEnd.y) + transform.position);
		}
	}
}
