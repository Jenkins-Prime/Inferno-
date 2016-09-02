using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	public string levelToLoad;
	public string levelTag;

	bool playerInZone;

	// Use this for initialization
	void Start () {
		playerInZone = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Submit") && playerInZone == true)  {
			LoadLevel ();
		}
	}

	public void LoadLevel() {
		PlayerPrefs.SetInt (levelTag, 1);
		Application.LoadLevelAsync(levelToLoad);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Player")  {
			playerInZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.name == "Player")  {
			playerInZone = false;
		}
	}
}
