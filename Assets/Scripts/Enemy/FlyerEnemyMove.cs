using UnityEngine;
using System.Collections;

public class FlyerEnemyMove : MonoBehaviour {

	private PlayerController player;
	public float moveSpeed;
	public float playerRange;

	public LayerMask playerLayer;
	private bool playerInRange;
	private bool FacingRight;


	// Use this for initialization
	void Start () {
	
		player = FindObjectOfType<PlayerController>();

	}
	
	// Update is called once per frame
	void Update () {

		playerInRange = Physics2D.OverlapCircle (transform.position, playerRange, playerLayer);

		if (playerInRange) 
		{
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, moveSpeed * Time.deltaTime);
		}
			
		if (player.transform.position.x > transform.position.x)
		{
			FacingRight = false;
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
		else
		{
			FacingRight = true;
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}
		



	void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere (transform.position, playerRange);
	}
}
