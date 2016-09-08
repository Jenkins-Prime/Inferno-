﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class GameController : MonoBehaviour {
	public static GameController instance = null;

	public int currentFloor;
	public int currentLevel;
	public List<Floor> floors;

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
		//Check if save game exists
		//if true then enable the load button
	}

	public void NewGame() {
		//Reset
		foreach (Floor f in floors) {
			f.isUnlocked = false;
			foreach (Level l in f.levels) {
				l.isUnlocked = false;
				l.score = 0;
			}
		}

		currentFloor = 0;
		currentLevel = 0;
		floors [currentFloor].isUnlocked = true;
		floors [currentFloor].levels [currentLevel].isUnlocked = true;

		//Save to file
		SaveGame();

		//Load scene
		SceneManager.LoadScene(floors[currentFloor].sceneName);
	}

	public void LoadGame() {
		if (File.Exists (Application.persistentDataPath + "/game.sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = new FileStream (Application.persistentDataPath + "/game.sav", FileMode.Open);

			currentFloor = (int)bf.Deserialize (file);
			currentLevel = (int)bf.Deserialize (file);
			floors = (List<Floor>)bf.Deserialize (file);
			file.Close ();

			//Load scene
			SceneManager.LoadScene(floors[currentFloor].sceneName);
		}
	}

	public void SaveGame() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = new FileStream (Application.persistentDataPath + "/game.sav", FileMode.Create);

		bf.Serialize (file, currentFloor);
		bf.Serialize (file, currentLevel);
		bf.Serialize (file, floors);
		file.Close ();
	}

	public void QuitGame() {
		Debug.Log ("Game Exited");
		Application.Quit();
	}

	public int GetCurrentLevel() {
		return currentLevel;
	}

	public Level LoadLevelData(int index) {
		return floors [currentFloor].levels [index];
	}
}

[Serializable]
public class Floor {
	public string name;
	public string sceneName;
	public bool isUnlocked;
	public List<Level> levels = new List<Level>();
}

[Serializable]
public class Level {
	public string name;
	public string sceneName;
	public bool isUnlocked;
	public int score;
	public int startTime;
}
