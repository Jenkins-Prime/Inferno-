using UnityEngine;
using System.Collections;

public class LadderZone : MonoBehaviour {

	PlayerController pController;

	// Use this for initialization
	void Start () {
		pController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player")
        {
			pController.EnterLadderZone ();
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player")
        {
			pController.ExitLadderZone ();
		}
	}
}
