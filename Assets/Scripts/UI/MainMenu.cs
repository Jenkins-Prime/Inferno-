using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public Button loadButton;
	Image loadImage;

	public GameObject gameSelectScreen; //change to ui panel

	// Use this for initialization
	void Start () {
		loadImage = loadButton.GetComponent<Image> ();
		GameController.instance.CheckForSavedGames ();
		if (GameController.instance.saveCount > 0) {
			loadImage.enabled = true;
			loadButton.enabled = true;
		} else {
			loadImage.enabled = false;
			loadButton.enabled = false;
		}
	}

	public void ShowGameSelectScreen() {
		gameSelectScreen.SetActive (true);
	}
}
