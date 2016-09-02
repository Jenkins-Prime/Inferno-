using UnityEngine;
using System.Collections;

public class HurtPlayerOnContact : MonoBehaviour {
	[SerializeField] int damageAmount = 1;
	PlayerController pController;
	LevelManager levelManager;

	// Use this for initialization
	void Start () {
		pController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
			levelManager.DecreaseHealth (damageAmount);
			pController.PlayerKnockBack (transform.position);
        }
    }
}
