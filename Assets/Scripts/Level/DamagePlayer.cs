using UnityEngine;
using System.Collections;

public class DamagePlayer : MonoBehaviour {
	[SerializeField] bool killPlayer = false;
	[SerializeField] int damageAmount = 1;

	LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (killPlayer)
				levelManager.DecreaseLife (1);
			else
				levelManager.DecreaseHealth (damageAmount);
		}
	}
}
