using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMove))]
public class EnemyPatrol : MonoBehaviour {
	[SerializeField] Vector2 targetWaypoint;
	Vector2 storedTargetWaypoint;
	Vector2 initPosition;

	const float minDist = 0.05f;

	Vector3 globalWaypointStart;
	Vector3 globalWaypointEnd;
	Vector3 prevWaypoint;
	Vector3 nextWaypoint;

	EnemyMove move;
	void OnEnable() {
		move.AddEnemyComponent<EnemyPatrol> ();
	}

	void OnDisable() {
		move.RemoveEnemyComponent<EnemyPatrol> ();
	}

	void Awake () {
		move = GetComponent<EnemyMove> ();

		storedTargetWaypoint = targetWaypoint;
		initPosition = transform.position;
		prevWaypoint = globalWaypointStart = initPosition;
		nextWaypoint = globalWaypointEnd = initPosition + targetWaypoint;
	}

	void SwitchWaypoints() {
		Vector3 tmp = prevWaypoint;
		prevWaypoint = nextWaypoint;
		nextWaypoint = tmp;
	}

	public bool Patrol(ref float currentDirection, bool facingWall) {
		if (targetWaypoint != storedTargetWaypoint) { //If the target has changed in inspector then change the patrol route
			storedTargetWaypoint = targetWaypoint;
			if (nextWaypoint == globalWaypointEnd) {
				globalWaypointEnd = initPosition + targetWaypoint;
				nextWaypoint = globalWaypointEnd;
			} else {
				globalWaypointEnd = initPosition + targetWaypoint;
				prevWaypoint = globalWaypointEnd;
			}
		}

		//Update move direction
		if (initPosition.x != globalWaypointEnd.x) {
			currentDirection = Mathf.Sign (nextWaypoint.x - transform.position.x);
		} else { //target = [0,0]
			if (Mathf.Abs (initPosition.x - transform.position.x) > minDist*2f) { //Return to [0,0]
				currentDirection = Mathf.Sign (nextWaypoint.x - transform.position.x);
			} else { //Stop if it's near [0,0]
				return false; //idle
			}
		}

		if (Mathf.Abs (transform.position.x - nextWaypoint.x) < minDist || facingWall) {
			SwitchWaypoints ();
		}

		return true; //patrolling
	}

	public bool Patrol(ref Vector2 currentDirection, bool facingWallOrCeiling) {
		if (targetWaypoint != storedTargetWaypoint) { //If the target has changed in inspector then change the patrol route
			storedTargetWaypoint = targetWaypoint;
			if (nextWaypoint == globalWaypointEnd) {
				globalWaypointEnd = initPosition + targetWaypoint;
				nextWaypoint = globalWaypointEnd;
			} else {
				globalWaypointEnd = initPosition + targetWaypoint;
				prevWaypoint = globalWaypointEnd;
			}
		}

		//Update move direction
		if (prevWaypoint != nextWaypoint) {
			currentDirection = nextWaypoint - transform.position;
		} else { //target = [0,0]
			if (Vector2.Distance (initPosition, transform.position) > minDist*2f) { //Return to [0,0]
				currentDirection = nextWaypoint - transform.position;
			} else { //Stop if it's near [0,0]
				currentDirection = Vector2.zero;
				return false; //idle
			}
		}
		currentDirection.Normalize ();

		if (Vector3.Distance (transform.position, nextWaypoint) < minDist || facingWallOrCeiling) {
			SwitchWaypoints ();
		}

		return true; //patrolling
	}

	void OnDrawGizmosSelected() {
		Vector2 topBorder = new Vector2 (0f, 0.01f);
		Vector2 bottomBorder = new Vector2 (0f, -0.01f);
		Vector2 leftBorder = new Vector2 (-0.01f, 0f);
		Vector2 rightBorder = new Vector2 (0.01f, 0f);

		Gizmos.color = Color.yellow;
		if (Application.isPlaying) {
			Gizmos.DrawLine (globalWaypointStart + (Vector3)topBorder, globalWaypointEnd + (Vector3)topBorder);
			Gizmos.DrawLine (globalWaypointStart + (Vector3)bottomBorder, globalWaypointEnd + (Vector3)bottomBorder);
			Gizmos.DrawLine (globalWaypointStart + (Vector3)leftBorder, globalWaypointEnd + (Vector3)leftBorder);
			Gizmos.DrawLine (globalWaypointStart + (Vector3)rightBorder, globalWaypointEnd + (Vector3)rightBorder);
		} else {
			Gizmos.DrawLine(transform.position + (Vector3)topBorder, transform.position + (Vector3)targetWaypoint + (Vector3)topBorder);
			Gizmos.DrawLine(transform.position + (Vector3)bottomBorder, transform.position + (Vector3)targetWaypoint + (Vector3)bottomBorder);
			Gizmos.DrawLine(transform.position + (Vector3)leftBorder, transform.position + (Vector3)targetWaypoint + (Vector3)leftBorder);
			Gizmos.DrawLine(transform.position + (Vector3)rightBorder, transform.position + (Vector3)targetWaypoint + (Vector3)rightBorder);
		}
	}
}
