using UnityEngine;
using System.Collections;

public class LadderZone : MonoBehaviour {
	public EdgeCollider2D topCol;

	PlayerController pController;

	// Use this for initialization
	void Start () {
		pController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		topCol = GetComponent<EdgeCollider2D> ();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			pController.EnterLadderZone ();
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player") {
			pController.ExitLadderZone ();
		}
	}

	public void SetPlatformTop(bool solid) {
		topCol.isTrigger = !solid;
	}
}
