using UnityEngine;
using System.Collections;
using UnityEngine.UI; //get rid of the ui

public class PlayerManager : MonoBehaviour {
	
	/*

	//public int startingLives;
	public GameObject gameOverScreen;
	[SerializeField] string gameOverLoadLevel = "Title_Menu";
	[SerializeField] float waitAfterGameOver = 4f;
	[SerializeField] int maxLives = 10;

	int lifeCounter;

	Text theText;
	GameObject player;
	//PlayerController pController;

	//Health
	public static int playerHealth;
	public int maxPlayerHealth;
	public bool isDead;
	private LevelManager levelManager;
	// Text text;
	public Slider healthBar;
	private TimeManager theTime;

	// Use this for initialization
	void Start () {
		theText = GetComponent<Text> (); //change it
		lifeCounter = PlayerPrefs.GetInt ("PlayerCurrentLives");

		player = GameObject.FindGameObjectWithTag ("Player");

		healthBar = GetComponent<Slider>();
		playerHealth = PlayerPrefs.GetInt ("PlayerCurrentHealth");
		levelManager = FindObjectOfType<LevelManager>();
		isDead = false; 
		theTime = FindObjectOfType<TimeManager> ();
	}

	public void GiveLife() {
		lifeCounter++;
		if (lifeCounter > maxLives) {
			lifeCounter = maxLives;
		}

		theText.text = "x " + lifeCounter;

		PlayerPrefs.SetInt ("PlayerCurrentLives", lifeCounter);
	}

	public void TakeLife() {
		lifeCounter--;

		if (lifeCounter < 0) {
			gameOverScreen.SetActive (true);
			player.SetActive(false);


			//What?
			if (gameOverScreen.activeSelf)  {
				waitAfterGameOver -= Time.deltaTime;
			}

			if (waitAfterGameOver < 0)  {
				Application.LoadLevel (gameOverLoadLevel);
			}
			//??
		}

		theText.text = "x " + lifeCounter;
		PlayerPrefs.SetInt ("PlayerCurrentLives", lifeCounter);
	}

	public static void HurtPlayer(int damageToGive)
	{
		playerHealth -= damageToGive;

		if(playerHealth <= 0 && !isDead) {
			playerHealth = 0;
			levelManager.RespawnPlayer();
			lifeSystem.TakeLife ();
			isDead = true;
			theTime.ResetTime ();
		}

		PlayerPrefs.SetInt ("PlayerCurrentHealth", playerHealth);
	}

	public void FullHealth()
	{
		//Arbitary
		if (playerHealth > maxPlayerHealth)  {
			playerHealth = maxPlayerHealth;
		}

		healthBar.value = playerHealth;

		playerHealth = PlayerPrefs.GetInt ("PlayerMaxHealth");
		PlayerPrefs.SetInt ("PlayerCurrentHealth", playerHealth);
	}

	public void KillPlayer()
	{
		playerHealth = 0;
	}*/
}
