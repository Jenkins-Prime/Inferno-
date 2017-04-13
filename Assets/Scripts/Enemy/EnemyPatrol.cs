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

	Vector2 prevDir;

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

	//Patrol on the x axis only
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
		currentDirection = UpdateMoveDirectionX();

		Debug.Log (Mathf.Abs (transform.position.x - nextWaypoint.x));
		if (Mathf.Abs (transform.position.x - nextWaypoint.x) < minDist || facingWall) {
			SwitchWaypoints ();
		}

		return true; //patrolling
	}

	//Patrol on both axis
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
		currentDirection = UpdateMoveDirection();
		currentDirection.Normalize ();

		if (Vector3.Distance (transform.position, nextWaypoint) < minDist || facingWallOrCeiling) {
			SwitchWaypoints ();
		}

		return true; //patrolling
	}

	//Patrol on wall climbing (TODO: simplify the params)
	public bool Patrol(ref Vector2 currentDirection, bool wallMoveLeft, bool wallMoveRight, bool wallMoveUp, bool wallMoveDown, bool left, bool right, bool above, bool below) {
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

		//Switch check here or inside below OR keep it like it is.

		//Update move direction, and check which axis to move
		prevDir = currentDirection;
		currentDirection = UpdateMoveDirection ();

		if (currentDirection.x < 0) { //going left
			if (wallMoveLeft && !left) {
				currentDirection.y = 0f;
			}
		} else if (currentDirection.x > 0) { //going right
			if (wallMoveRight && !right) {
				currentDirection.y = 0f;
			}
		}
			
		if (currentDirection.y > 0) { //going up
			if (wallMoveUp && !above) {
				currentDirection.x = 0f;

			}
		} else if (currentDirection.y < 0) { //going down
			if (wallMoveDown && !below) {
				currentDirection.x = 0f;
			}
		}

		currentDirection.Normalize ();

		//Debug.Log (currentDirection + " wLeft: " + wallMoveLeft + " left: " + left + " wUp: " + wallMoveUp);
		//Wall checks

		bool doSwitch = false;
		Vector2 tmpD = currentDirection;


		if((currentDirection.y == 0f) && (Mathf.Abs(transform.position.x - nextWaypoint.x) < minDist) ||
		   (currentDirection.x == 0f) && (Mathf.Abs(transform.position.y - nextWaypoint.y) < minDist)) {
			SwitchWaypoints ();
		}

		if (currentDirection != Vector2.zero)
			return false; //not patrolling
		else
			return true; //patrolling
	}

	Vector2 UpdateMoveDirection() {
		if ((prevWaypoint != nextWaypoint) || (Vector2.Distance (prevWaypoint, transform.position) > minDist)) {
			return nextWaypoint - transform.position;
		}
			
		return Vector2.zero;
	}

	float UpdateMoveDirectionX() { //TODO: check the initPosition to change to prevWaypoint
		if ((prevWaypoint.x != globalWaypointEnd.x) || (Mathf.Abs (prevWaypoint.x - transform.position.x) > minDist)) {
			return Mathf.Sign (nextWaypoint.x - transform.position.x);
		}
			
		return 0f;
	}

	float UpdateMoveDirectionY() {
		if ((prevWaypoint.y != globalWaypointEnd.y) || (Mathf.Abs (prevWaypoint.y - transform.position.y) > minDist)) {
			return Mathf.Sign (nextWaypoint.y - transform.position.y);
		}

		return 0f;
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
