using UnityEngine;
using System.Collections;

[RequireComponent(typeof (EnemyController))]
public class EnemyFlyingMovement : MonoBehaviour {
	[Header("Waypoints:")]
	[SerializeField] Vector2 localWaypointStart;
	[SerializeField] Vector2 localWaypointEnd;
	[Header("Movement Params:")]
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] float chaseSpeed = 2f;
	[Header("Chasing Params:")]
	[SerializeField] bool canChase;
	[SerializeField] LayerMask playerMask;
	[SerializeField] float detectRadius = 1f;
	[SerializeField] float chaseRadius = 2f;

	bool isChasing;
	Vector3 initPosition;
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

		initPosition = transform.position;
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
		if (canChase)
			ChasePlayer ();

		if (!isChasing) {
			if (CheckEnemyDirectionChange ())
				ChangeEnemyDirection ();

			moveDirection = targetWaypoint - transform.position;
			moveDirection.Normalize ();
			velocity = moveDirection * moveSpeed;
		}

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

	void ChasePlayer() {
		isChasing = false;

		Collider2D col = Physics2D.OverlapCircle (transform.position, detectRadius, playerMask);
		if (col) {
			if (Vector3.Distance (initPosition, col.transform.position) < chaseRadius) {
				moveDirection = col.transform.position - transform.position;
				moveDirection.Normalize ();
				velocity = moveDirection * chaseSpeed;
				isChasing = true;

				Debug.DrawRay (transform.position, col.transform.position - transform.position, Color.red);
			}
		}
	}
	
	void OnDrawGizmos() {
		if(canChase)
			Gizmos.DrawWireSphere (initPosition, chaseRadius);
		
		Gizmos.color = Color.cyan;
		if (Application.isPlaying) {
			Gizmos.DrawLine (globalWaypointLeftDown, globalWaypointRightUp);
		} else {
			Gizmos.DrawLine (new Vector3(localWaypointStart.x, localWaypointStart.y) + transform.position, new Vector3(localWaypointEnd.x, localWaypointEnd.y) + transform.position);
		}
	}
}
