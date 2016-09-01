using UnityEngine;
using System.Collections;

public class HurtPlayerOnContact : MonoBehaviour {

    public int damageToGive;

	PlayerController pController;

	// Use this for initialization
	void Start () {
		pController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            HealthManager.HurtPlayer(damageToGive);
            other.GetComponent<AudioSource>().Play();

			pController.PlayerKnockBack (transform.position);
        }
    }
}
