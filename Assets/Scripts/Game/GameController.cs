using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public static GameController instance = null;

	public List<FloorData> floors;
	public int curFloor;

	void Awake() {
		//Singleton pattern
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		InitGame();
	}

	void InitGame () {
		//Init game params here
		//Load Player lives
		//Load Player Abilities unlocked scriptableobject
		//Load Unlocked Levels
		//Load options params
	}

	public void NewGame() {
		PlayerPrefs.DeleteAll();
		//PlayerPrefs.SetInt ("PlayerCurrentLives", playerLives);
		//PlayerPrefs.SetInt ("CurrentPlayerScore", 0); //arbitrary
		//PlayerPrefs.SetInt ("PlayerCurrentHealth", playerHealth);  //arbitrary
		//PlayerPrefs.SetInt ("PlayerMaxHealth", playerHealth);  //arbitrary
		//PlayerPrefs.SetInt(level1Tag, 1); //improve this
		//PlayerPrefs.SetInt ("PlayerLevelSelectPosition", 0);
		//Application.LoadLevel(floors[0].floorScene);
		//UnityEngine.SceneManagement.Scene scene;
		curFloor = 0;
		SceneManager.LoadScene(floors[curFloor].floorSceneName);

	}

	public void LoadGame() {
		//PlayerPrefs.SetInt ("PlayerCurrentLives", playerLives);
		//PlayerPrefs.SetInt ("CurrentPlayerScore", 0); //arbitrary
		//PlayerPrefs.SetInt ("PlayerCurrentHealth", playerHealth);  //arbitrary
		//PlayerPrefs.SetInt ("PlayerMaxHealth", playerHealth);  //arbitrary
		//PlayerPrefs.SetInt(level1Tag, 1);
		//if(!PlayerPrefs.HasKey("PlayerLevelSelectPosition")) {
//			PlayerPrefs.SetInt ("PlayerLevelSelectPosition", 0);
//		}

//		Application.LoadLevel(levelSelect);
	}
}
