using UnityEngine;
using System.Collections;

public class EnemyBulletController : MonoBehaviour {
	[SerializeField] float speed = 3f;
	[SerializeField] int damageAmount = 1;

	//public GameObject enemyDeathEffect;
	public GameObject impactEffect;
	//public int pointsForKill;

	Rigidbody2D rb2D;
	ActorController controller;
	LevelManager levelManager;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		controller = GameObject.FindGameObjectWithTag ("Player").GetComponent<ActorController> ();
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();

		if(controller.transform.localScale.x < transform.position.x) {
			transform.localRotation = Quaternion.Euler (0, 180, 0);
			speed = -speed;
		} else {
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		
		}
	}

	void FixedUpdate () {
		rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			levelManager.DecreaseHealth (damageAmount);
		}
		
		Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}