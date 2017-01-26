using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
	[SerializeField] Vector2 targetWaypoint;

	Vector3 globalWaypointStart;
	Vector3 globalWaypointEnd;
	Vector3 prevWaypoint;
	Vector3 nextWaypoint;

	void Awake () {
		prevWaypoint = globalWaypointStart = transform.position;
		nextWaypoint = globalWaypointEnd = new Vector3 (targetWaypoint.x + transform.position.x, targetWaypoint.y + transform.position.y, transform.position.z);
	}

	void SwitchWaypoints() {
		Vector3 tmp = prevWaypoint;
		prevWaypoint = nextWaypoint;
		nextWaypoint = tmp;
	}

	public Vector2 SetMoveDirection() {
		Vector2 moveDir;

		moveDir = globalWaypointEnd - globalWaypointStart;
		moveDir.Normalize ();

		return moveDir;
	}
		
	public float UpdateMoveDirection(float currentDirection, bool wallInFront) {
		float moveDir = currentDirection;

		if (prevWaypoint.x != nextWaypoint.x) {
			moveDir = Mathf.Sign (nextWaypoint.x - transform.position.x);
		} else { //to avoid bugs when target is set to [0,0]
			moveDir = 0f;
		}

		if (moveDir != 0f && Mathf.Abs (transform.position.x - nextWaypoint.x) < 0.05f || wallInFront) {
			SwitchWaypoints ();
		}
			
		return moveDir;
	}

	public Vector2 UpdateMoveDirection(Vector2 currentDirection, bool wallInFront, bool aboveBelow) {
		Vector2 moveDir = currentDirection;

		if (prevWaypoint.x != nextWaypoint.x) {
			moveDir = nextWaypoint - transform.position;
		} else { //to avoid bugs when target is set to [0,0]
			moveDir = Vector2.zero;
		}
		moveDir.Normalize ();

		if (moveDir != Vector2.zero && Vector3.Distance (transform.position, nextWaypoint) < 0.05f || wallInFront || aboveBelow) {
			SwitchWaypoints ();
		}

		return moveDir;
	}

	public Vector3 GetNextWaypoint() {
		return nextWaypoint;
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		if (Application.isPlaying) {
			Gizmos.DrawLine (globalWaypointStart, globalWaypointEnd);
		} else {
			Gizmos.DrawLine (transform.position, new Vector3(targetWaypoint.x + transform.position.x, targetWaypoint.y + transform.position.y));
		}
	}
}
