using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (EnemyController))]
public abstract class EnemyAttack : MonoBehaviour {
	protected EnemyController controller;

	protected virtual void Start () {
		controller = GetComponent<EnemyController> ();
	}
}
