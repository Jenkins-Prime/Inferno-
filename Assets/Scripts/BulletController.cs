using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float speed;

	public PlayerController player;

	public GameObject impactEffect;

	public int pointsForKill;


	public int damageToGive;

	private Rigidbody2D myrigidbody2D;

	// Use this for initialization
	void Start () {

		player = FindObjectOfType<PlayerController>();

		myrigidbody2D = GetComponent<Rigidbody2D>();

		if(player.transform.localScale.x < 0)
		{
			transform.localRotation = Quaternion.Euler (0, 180, 0);
			speed = -speed;
		}
		else
		{
				transform.localRotation = Quaternion.Euler (0, 0, 0);
		}



	}

	// Update is called once per frame
	void Update () {
		myrigidbody2D.velocity = new Vector2(speed, myrigidbody2D.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{

			other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
		}

		Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}