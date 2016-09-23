using UnityEngine;
using System.Collections;

public class HurtPlayerOnContact : MonoBehaviour {
	[SerializeField] int damageAmount = 100;
	PlayerController pController;
	LevelManager levelManager;

	// Use this for initialization
	void Start () {
		pController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            pController.isDead = true;
			levelManager.DecreaseHealth (damageAmount);
			pController.PlayerKnockBack (transform.position);
        }
    }
}
