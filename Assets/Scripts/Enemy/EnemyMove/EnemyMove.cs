using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //Used for the label in Gizmos

[RequireComponent(typeof (ActorController))]
[DisallowMultipleComponent]
public abstract class EnemyMove : MonoBehaviour {
	[SerializeField] protected float moveSpeed = 1f;

	protected bool initSpriteDirection;
	protected Vector3 velocity;
	protected Vector2 moveDirection;

	protected enum MoveState {Idle, Patrol, Chase, Manipulate};
	protected MoveState state;

	protected SpriteRenderer rend;
	protected Animator anim;
	protected ActorController controller;
	protected EnemyPatrol enemyPatrol;
	protected EnemyChase enemyChase;
	protected EnemyManipulate enemyManipulate;

	protected virtual void Start () {
		rend = GetComponent<SpriteRenderer> ();
		initSpriteDirection = rend.flipX;

		anim = GetComponent<Animator> ();
		controller = GetComponent<ActorController> ();

		state = MoveState.Idle;
	}

	protected abstract void Update ();

	protected abstract void Move();
	protected abstract void Animate();

	public void AddEnemyComponent<T>() {
		if (typeof(T) == typeof(EnemyPatrol)) {
			enemyPatrol = GetComponent<EnemyPatrol> ();
		} else if (typeof(T) == typeof(EnemyChase)) {
			enemyChase = GetComponent<EnemyChase> ();
		} else if (typeof(T) == typeof(EnemyManipulate)) {
			enemyManipulate = GetComponent<EnemyManipulate> ();
		}
	}

	public void RemoveEnemyComponent<T>() {
		if(typeof(T) == typeof(EnemyPatrol)) {
			enemyPatrol = null;
		} else if (typeof(T) == typeof(EnemyChase)) {
			enemyChase = null;
		} else if (typeof(T) == typeof(EnemyManipulate)) {
			enemyManipulate = null;
		}
	}

	void OnDrawGizmosSelected() {
		if(Application.isPlaying)
			Handles.Label (new Vector3(rend.bounds.center.x - 0.1f, rend.bounds.max.y + 0.2f, 0f), state.ToString ());
	}
}
