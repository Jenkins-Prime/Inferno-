using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public GameObject pauseMenuCanvas;

	Player player;

    void Start() {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
    }

    void Update() {
		if (InputManager.Instance.PauseButton())
        {
			if (Time.timeScale == 0.0f)	{ //Resume Game
				Time.timeScale = 1.0f;
				player.canMove = true;
			} else { //Pause Game
				Time.timeScale = 0.0f;
				player.canMove = false;
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
