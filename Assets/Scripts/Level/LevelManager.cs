using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public GameObject deathParticle;
	public GameObject respawnParticle;

//	[SerializeField] int maxScore = 100; //not sure if useful

	[SerializeField] float deathDelay = 1f;
	[SerializeField] float respawnDelay = 2f;
	[SerializeField] int pointPenaltyOnDeath = 100;

	int curLives;
	int curHealth;
	int curScore;
	public Transform curCheckPoint;

	Player player;
	HUDManager hudManager;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;

		curLives = GameController.instance.playerData.curLives;
		curHealth = GameController.instance.playerData.maxHealth;
		curScore = 0;

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		hudManager = GameObject.FindGameObjectWithTag ("HUD").GetComponent<HUDManager> ();

		//InvokeRepeating("CountTime", 0f, 1f);
	}

	//If we are going to use an update() then add input check for the levelLoader here
	//Temp solution
	void Update() {
	}


	//Maybe move those two to gamecontroller
	public void IncreaseLife(int amount) {
		curLives += amount;
		if (curLives > GameController.instance.playerData.maxLives)
			curLives = GameController.instance.playerData.maxLives;

		//Do fx here
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

		GameController.instance.playerData.curLives = curLives;
		hudManager.SetLifeUI (curLives);
	}

	public void IncreaseHealth(int amount) {
		curHealth += amount;

		if (curHealth > GameController.instance.playerData.maxHealth)
			curHealth = GameController.instance.playerData.maxHealth;

		//do fx here
		hudManager.SetHealthUI(curHealth);
	}

	public void DecreaseHealth(int amount) {
		curHealth -= amount;

		//do fx here
		hudManager.SetHealthUI(curHealth);

		if (curHealth < 1)
			DecreaseLife (1);
	}

	public void IncreaseScore(int amount) {
		curScore += amount;
		hudManager.SetScoreUI (curScore);
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
		player.KillPlayer(true);
		Instantiate (deathParticle, player.transform.position, player.transform.rotation);
		//ScoreManager.AddPoints(-pointPenaltyOnDeath); //not sure if useful removing score points

		yield return new WaitForSeconds(deathDelay);

		//Respawn particles part
		player.transform.position = curCheckPoint.position;
		Instantiate(respawnParticle, curCheckPoint.position, curCheckPoint.rotation);
		yield return new WaitForSeconds(respawnDelay);

		//Show player part
		IncreaseHealth (GameController.instance.playerData.maxHealth);
		player.KillPlayer (false);
	}
}
