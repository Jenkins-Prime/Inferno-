using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour {
	public static GameController instance = null;

	public PlayerData playerData;
	public int currentFloor;
	public int currentLevel;
	public List<Floor> floors;

	[HideInInspector] public int saveId;
	[HideInInspector] public int saveCount;

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
		
	}
		
	public void CheckForSavedGames() {
		saveId = saveCount = 0;
		while (File.Exists (Application.persistentDataPath + "/game" + saveCount + ".sav"))
			saveCount++;	
	}

	public void NewGame() {
		//Reset
		foreach (Floor f in floors) {
			f.isUnlocked = false;
			foreach (LevelData l in f.levelData) {
				l.isUnlocked = false;
				l.score = 0;
			}
		}

		saveId = 0; //TODO: change this, make it by adding to the stack of loaded saves
		currentFloor = 0;
		currentLevel = 0;
		floors [currentFloor].isUnlocked = true;
		floors [currentFloor].levelData [currentLevel].isUnlocked = true;

		//Save to file
		SaveGame();

		//Load scene
		SceneManager.LoadScene(floors[currentFloor].sceneName);
	}

	public void LoadGame(int i) {
		saveId = i;
		if (File.Exists (Application.persistentDataPath + "/game" + saveId + ".sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = new FileStream (Application.persistentDataPath + "/game" + saveId + ".sav", FileMode.Open);

			currentFloor = (int)bf.Deserialize (file);
			currentLevel = (int)bf.Deserialize (file);
			floors = (List<Floor>)bf.Deserialize (file);
			file.Close ();

			//Load scene
			SceneManager.LoadScene (floors [currentFloor].sceneName);
		} else {
			//Show UI error
			Debug.Log ("Error: Couldn't load saved game #" + saveId);
		}

	}

	public void SaveGame() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = new FileStream (Application.persistentDataPath + "/game" + saveId + ".sav", FileMode.Create);

		bf.Serialize (file, currentFloor);
		bf.Serialize (file, currentLevel);
		bf.Serialize (file, floors);
		file.Close ();
	}

	public void QuitGame() {
		Debug.Log ("Game Exited");
		Application.Quit();
	}

	public LevelData GetLevelData(int i) {
		return floors [currentFloor].levelData [i];
	}

	public LevelData GetCurrentLevelData() {
		return floors [currentFloor].levelData [currentLevel];
	}
}

[System.Serializable]
public class PlayerData {
	public int curLives;

	public int maxLives = 10;
	public int maxHealth = 5;
}

[System.Serializable]
public class Floor {
	public string name;
	public string sceneName;
	public bool isUnlocked;
	public List<LevelData> levelData = new List<LevelData>();
}

[System.Serializable]
public class LevelData {
	public bool isUnlocked;
	public string sceneName;
	public int score;
	public float startTime;
}
