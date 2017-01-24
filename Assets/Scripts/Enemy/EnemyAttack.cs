using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (ActorController))]
public abstract class EnemyAttack : MonoBehaviour {
	protected ActorController controller;

	protected virtual void Start () {
		controller = GetComponent<ActorController> ();
	}
}
