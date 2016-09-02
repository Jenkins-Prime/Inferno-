using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	//Get rid of TimeManager
	//+ Scoremanager

	//The LevelManager Manages the below
	//Time
	//Score
	//Checkpoints
	//+PlayerManager
	public GameObject deathParticle;
	public GameObject respawnParticle;

	[SerializeField] int maxLives = 10;
	[SerializeField] int maxHealth = 5;
	[SerializeField] int maxScore = 100; //not sure if useful

	[SerializeField] float deathDelay = 1f;
	[SerializeField] float respawnDelay = 2f;
	[SerializeField] int pointPenaltyOnDeath = 100;

	int curLives;
	int curHealth;
	int curScore;
	int curTime;
	Transform curCheckPoint;

	GameController gController;
	PlayerController pController;
	HUDManager hudManager;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;
		//Init params
		//import from playerprefs
		curLives = 3;
		curHealth = maxHealth;

		pController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		gController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		hudManager = GameObject.FindGameObjectWithTag ("HUD").GetComponent<HUDManager> ();
		InitTime(400); //change it more level specific value
		//InvokeRepeating("CountTime", 0f, 1f);
	}

	//If we are going to use an update() then add input check for the levelLoader here

	//Maybe move those two to gamecontroller
	public void IncreaseLife(int amount) {
		curLives += amount;
		if (curLives > maxLives)
			curLives = maxLives;

		//Do sfx here
		hudManager.SetLifeUI(curLives);
		//Save playerprefs
	}
	//this one too
	public void DecreaseLife(int amount) {
		curLives -= amount;

		if (curLives < 0) {
			//Gameover sequence
			//save playerprefs
		} else {
			StartCoroutine (RespawnPlayer ());
		}

		hudManager.SetLifeUI (curLives);
	}

	public void IncreaseHealth(int amount) {
		curHealth += amount;

		if (curHealth > maxHealth)
			curHealth = maxHealth;

		//do sfx here
		hudManager.SetHealthUI(curHealth);
	}

	public void DecreaseHealth(int amount) {
		curHealth -= amount;

		//do sfx here
		hudManager.SetHealthUI(curHealth);

		if (curHealth <= 0)
			DecreaseLife (1);
	}

	public void InitTime(int amount) {
		curTime = amount;
	}

	public void SetCheckPoint(Transform checkPoint) {
		curCheckPoint = checkPoint;
		Debug.Log("Activated Checkpoint" + curCheckPoint.position);
	}

	//===== Coroutines =====
	/*IEnumerator CountTime() {
		curTime--;

		//update ui

		if (curTime < 0) {
			DecreaseLife (1);
		}

		yield return null;
	}*/

	IEnumerator RespawnPlayer() {
		//Death part
		pController.KillPlayer(true);
		Instantiate (deathParticle, pController.transform.position, pController.transform.rotation);
		ScoreManager.AddPoints(-pointPenaltyOnDeath); //not sure if useful removing score points
		yield return new WaitForSeconds(deathDelay);

		//Respawn particles part
		pController.transform.position = curCheckPoint.position;
		Instantiate(respawnParticle, curCheckPoint.position, curCheckPoint.rotation);
		yield return new WaitForSeconds(respawnDelay);

		//Show player part
		IncreaseHealth (maxHealth);
		pController.KillPlayer (false);
	}
}
