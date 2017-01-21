using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour {
	[Header("Chasing Params:")]
	[SerializeField] float detectRadius = 1f;
	[SerializeField] float chaseRadius = 2f;

	LayerMask playerMask;
	Vector3 startPosition;
	bool isChasing;

	void Awake () {
		playerMask.value = LayerMask.GetMask("Player");
		startPosition = transform.position;
		isChasing = false;
	}

	public bool DetectPlayer(ref float moveDir) {
		Collider2D col = Physics2D.OverlapCircle (transform.position, detectRadius, playerMask);
		if (col && Vector3.Distance (startPosition, col.transform.position) < chaseRadius) {
			moveDir = Mathf.Sign (col.transform.position.x - transform.position.x);
			return isChasing = true;
		}

		return false;
	}

	public bool DetectPlayer(ref Vector2 moveDir) {
		Collider2D col = Physics2D.OverlapCircle (transform.position, detectRadius, playerMask);
		if (col && Vector3.Distance (startPosition, col.transform.position) < chaseRadius) {
			moveDir = col.transform.position - transform.position;
			moveDir.Normalize ();
			return isChasing = true;
		}

		return false;

	}

	public void ReturnFromChase(ref float moveDir, Vector3 nextPatrolPosition) {
		if (nextPatrolPosition.x != transform.position.x)
			moveDir = Mathf.Sign (nextPatrolPosition.x - transform.position.x);
		else
			moveDir = 0f;
		
		isChasing = false;
	}

	public void ReturnFromChase(ref Vector2 moveDir, Vector3 nextPatrolPosition) {
		moveDir = nextPatrolPosition - transform.position;
		moveDir.Normalize ();
		isChasing = false;
	}

	public bool IsChasing() {
		return isChasing;
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.cyan;
		if (Application.isPlaying) {
			Gizmos.DrawWireSphere (startPosition, chaseRadius);
		} else {
			Gizmos.DrawWireSphere (transform.position, chaseRadius);
		}

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, detectRadius);
	}
}
