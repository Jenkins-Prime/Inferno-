using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player Data", order = 1)]
public class PlayerData : ScriptableObject {
	[Header("Movement Variables")]
	public float moveSpeed = 2f;
	public float jumpHeight = 3.5f;
	public float knockBackSpeed = 2f;
	public float climbSpeed = 2f;

	[Header("Head Stomp Variables")]
	public int damageToGive = 1;
	public float enemyBounceHeight = 2f;

	[Header("Ranged Attack Variables")]
	public GameObject bullet;
	public float shotDelay = 2f;

	[Header("Other")]
	public AudioClip jumpClip;
	public float knockBackLength = 0.2f;
}
