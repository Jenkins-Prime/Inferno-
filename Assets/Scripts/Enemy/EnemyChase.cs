using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMove))]
public class EnemyChase : MonoBehaviour {
	[Header("Chasing Params:")]
	[SerializeField] float detectRadius = 1f;
	[SerializeField] float chaseRadius = 2f;

	LayerMask playerMask;
	Vector3 startPosition;

	EnemyMove move;
	void OnEnable() {
		move.AddEnemyComponent<EnemyChase> ();
	}

	void OnDisable() {
		move.RemoveEnemyComponent<EnemyChase> ();
	}

	void Awake () {
		move = GetComponent<EnemyMove> ();

		playerMask.value = LayerMask.GetMask("Player");
		startPosition = transform.position;
	}

	public bool Chase(ref float moveDir) {
		Collider2D col = Physics2D.OverlapCircle (transform.position, detectRadius, playerMask);
		if (col && Vector3.Distance (startPosition, col.transform.position) < chaseRadius) {
			moveDir = Mathf.Sign (col.transform.position.x - transform.position.x);
			return true;
		}

		return false;
	}

	public bool Chase(ref Vector2 moveDir) {
		Collider2D col = Physics2D.OverlapCircle (transform.position, detectRadius, playerMask);
		if (col && Vector3.Distance (startPosition, col.transform.position) < chaseRadius) {
			moveDir = col.transform.position - transform.position;
			moveDir.Normalize ();
			return true;
		}

		return false;
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
