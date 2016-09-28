using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	LevelManager levelManager;

    void Start() {
		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

	public void SetCheckpoint() {
		levelManager.SetCheckPoint (transform);
		//add fx
		gameObject.SetActive(false);
	}
}
