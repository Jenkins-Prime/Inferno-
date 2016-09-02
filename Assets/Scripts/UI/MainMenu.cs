using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	GameController gController;

	void Start() {
		gController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	public void NewGame() {
		//add onclick fx
		gController.NewGame ();
	}

	public void LoadGame() {
		//add onclick fx
		gController.LoadGame ();
	}

	public void QuitGame() {
		//add onclick sfx
		//add confirm msg
		Debug.Log ("Game Exited");
		Application.Quit();
	}
}