using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (ActorController))]
public abstract class EnemyAttack : MonoBehaviour {
	[SerializeField] bool damageOnTouch = true;

	protected ActorController controller;
	Player player;

	protected virtual void Awake() {
		controller = GetComponent<ActorController> ();
	}

	protected virtual void Start() {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player" && damageOnTouch) {
			if (player.canMove && !player.knockBack) {
				player.PlayerKnockBack (transform.position);
			}
		}
	}
}
