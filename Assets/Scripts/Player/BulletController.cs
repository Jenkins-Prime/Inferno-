using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float speed;
	public PlayerController pController;
	public GameObject impactEffect;
	public int pointsForKill;
	public int damageToGive;

	Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
		pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		rb2D = GetComponent<Rigidbody2D>();

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
			other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
		} else if (other.tag == "Destructible")  {
			Destroy (other.gameObject); //improve with adding fx/particles
		}

		Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}