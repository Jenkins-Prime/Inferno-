using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public GameObject deathParticle;
	public GameObject respawnParticle;
	public Transform curCheckPoint;

	[SerializeField] float deathDelay = 1f;
	[SerializeField] float respawnDelay = 2f;

	int curLives;
	int curHealth;
	int curScore;

	Player player;
    PlayerHealth playerHealth;
	

    private void OnEnable()
    {
        //HUDManager.Instance.LoseHealth += DecreaseHealth;
        //HUDManager.Instance.GainHealth += IncreaseHealth;
    }
    void Start () {
		Time.timeScale = 1f;

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        curLives = GameController.instance.playerData.curLives;
        curHealth = playerHealth.CurrentHealth;


        curScore = 0;

		
	}

	//Maybe move those two to gamecontroller
	public void IncreaseLife(int amount) {
        
       
		//Do fx here
	
		//Save playerprefs
	}

	//this one too
	public void DecreaseLife(int amount) {
		curLives -= amount;
		if (curLives < 0) {
			//Gameover sequence
			//save playerprefs
		} else {
			//StartCoroutine (RespawnPlayer ());
		}

		GameController.instance.playerData.curLives = curLives;
		
	}

	public void IncreaseHealth(int amount) {
		//playerHealth.AddHealth(amount);
        HUDManager.Instance.OnGainHealth(amount);
   //     if (curHealth > GameController.instance.playerData.maxHealth)
			//curHealth = GameController.instance.playerData.maxHealth;

		//do fx here
		
	}

	public void DecreaseHealth(int amount) {
        playerHealth.Damage(amount);
		//do fx here
		

		if (curHealth < 1)
			DecreaseLife (1);
	}

	public void IncreaseScore(int amount) {
		curScore += amount;
	}

	public void SetCheckPoint(Transform checkPoint) {
		curCheckPoint = checkPoint;
		Debug.Log("Activated Checkpoint" + curCheckPoint.position);
	}
		
	//IEnumerator RespawnPlayer() {
	//	//Death part
	//	player.KillPlayer(true);
	//	Instantiate (deathParticle, player.transform.position, player.transform.rotation);
	//	//ScoreManager.AddPoints(-pointPenaltyOnDeath); //not sure if useful removing score points

	//	yield return new WaitForSeconds(deathDelay);

	//	//Respawn particles part
	//	player.transform.position = curCheckPoint.position;
	//	Instantiate(respawnParticle, curCheckPoint.position, curCheckPoint.rotation);
	//	yield return new WaitForSeconds(respawnDelay);

	//	//Show player part
	//	IncreaseHealth (GameController.instance.playerData.maxHealth);
	//	player.KillPlayer (false);
	//}
}
