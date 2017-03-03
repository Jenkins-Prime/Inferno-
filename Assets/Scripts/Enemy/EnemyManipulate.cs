using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ActorController))]
public class EnemyManipulate : MonoBehaviour
{
	public bool controlMode;

	EnemyMove move;
	void OnEnable()
    {
		move.AddEnemyComponent<EnemyManipulate> ();
	}

	void OnDisable()
    {
		move.RemoveEnemyComponent<EnemyManipulate> ();
	}

	void Awake()
    {
		move = GetComponent<EnemyMove> ();
		controlMode = false;
	}

	public void GetInput(ref float moveDir, ref bool jumpInput)
    {
		moveDir = InputManager.Instance.MainStick ().x;
		jumpInput = InputManager.Instance.JumpButton ();
	}

	public void GetInput(ref Vector2 moveDir)
    {
		moveDir = InputManager.Instance.MainStick ();
	}

	public void SetControlEnemy(bool b)
    {
		controlMode = b;
	}

	public bool CanControlEnemy()
    {
		return controlMode;
	}
}
