using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (ActorController))]
public abstract class EnemyMove : MonoBehaviour {
	[SerializeField] protected float moveSpeed = 1f;

	protected Vector3 velocity;
	protected Vector2 moveDirection;

	protected SpriteRenderer rend;
	protected Animator anim;
	protected ActorController controller;
	protected EnemyPatrol patrol;
	protected EnemyChase chase;

	protected virtual void Start () {
		rend = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		controller = GetComponent<ActorController> ();
		patrol = GetComponent<EnemyPatrol> ();
		chase = GetComponent<EnemyChase> ();
	}

	protected abstract void Update ();

	protected abstract void PatrolInit ();
	protected abstract void PatrolCheck ();

	protected abstract bool ChaseCheck();

	protected abstract void Move();
	protected abstract void AnimateEnemy();
}
