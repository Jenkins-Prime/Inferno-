using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public bool isPaused;
	public GameObject pauseMenuCanvas;

    void Start()
    {
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
    }


    public void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
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
