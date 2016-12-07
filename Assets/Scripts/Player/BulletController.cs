using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float speed;
	public GameObject impactEffect;
	public int pointsForKill;
	public int damageToGive;

	Rigidbody2D rb2D;
	PlayerController pController;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		if(pController.transform.localScale.x < 0) {
			transform.localRotation = Quaternion.Euler (0, 180, 0);
			speed = -speed;
		} else {
				transform.localRotation = Quaternion.Euler (0, 0, 0);
		}
	}

	// Update is called once per frame
	void Update () {
		rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy") {
			other.GetComponent<EnemyHealthManager>().GiveDamage(damageToGive);
		} else if (other.tag == "Destructible")  {
			Destroy (other.gameObject); //improve with adding fx/particles
		}

		Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}