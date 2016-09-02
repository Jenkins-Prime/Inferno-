using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
    LevelManager levelManager;
    
    // Use this for initialization
    void Start() {
		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Player") {
			levelManager.SetCheckPoint (transform);
			gameObject.SetActive (false);
        }
    }
}
