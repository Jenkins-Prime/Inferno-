using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	//public bool isPaused;
	public GameObject pauseMenuCanvas;

	PlayerController pController;

    void Start() {
		pController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
    }

    void Update() {
		if (InputManager.PauseButton()) {
			if (Time.timeScale == 0.0f)	{ //Resume Game
				Time.timeScale = 1.0f;
				pController.canMove = true;
			} else { //Pause Game
				Time.timeScale = 0.0f;
				pController.canMove = false;
			}
        }
    }

	public void LevelSelect(string levelSelect)
	{
        SceneManager.LoadScene(levelSelect);
	}

	public void Quit (string mainMenu)
	{
        SceneManager.LoadScene(mainMenu);
    }
}
