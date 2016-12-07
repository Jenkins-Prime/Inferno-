using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {

    public int enemyHealth = 1;
    public GameObject deathEffect;
    public int pointsOnDeath;

	LevelManager levelManager;

	void Start () {
		GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}

	void Die() {
		Instantiate(deathEffect, transform.position, transform.rotation);
		//ScoreManager.AddPoints(pointsOnDeath);
		//WARNING: DO NOT UNCOMMENT THIS LINE BELOW, IT FREAKS OUT WHEN YOU TRY TO KILL IT
		//levelManager.AddScore(pointsOnDeath);
		Destroy(gameObject);
	}

	public void GiveDamage (int damageToGive) {
        enemyHealth -= damageToGive;
		if (enemyHealth <= 0)
			Die ();
    }

}
