using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    
	public string startLevel;

	public string levelSelect;

	public int playerLives;

	public int playerHealth;

	public string level1Tag;



public void NewGame()
	{

		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt ("PlayerCurrentLives", playerLives);
		PlayerPrefs.SetInt ("CurrentPlayerScore", 0);
		PlayerPrefs.SetInt ("PlayerCurrentHealth", playerHealth);
		PlayerPrefs.SetInt ("PlayerMaxHealth", playerHealth);
		PlayerPrefs.SetInt(level1Tag, 1);
		PlayerPrefs.SetInt ("PlayerLevelSelectPosition", 0);
		Application.LoadLevel(startLevel);
	}

public void LevelSelect()
	{
		PlayerPrefs.SetInt ("PlayerCurrentLives", playerLives);
		PlayerPrefs.SetInt ("CurrentPlayerScore", 0);
		PlayerPrefs.SetInt ("PlayerCurrentHealth", playerHealth);
		PlayerPrefs.SetInt ("PlayerMaxHealth", playerHealth);
		PlayerPrefs.SetInt(level1Tag, 1);
		if(!PlayerPrefs.HasKey("PlayerLevelSelectPosition"))
			{
			PlayerPrefs.SetInt ("PlayerLevelSelectPosition", 0);
			}

			
		Application.LoadLevel(levelSelect);
	}

public void QuitGame()
	{
		Debug.Log ("Game Exited");
		Application.Quit();
	}
}