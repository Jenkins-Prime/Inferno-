using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {

    public int enemyHealth;
    public GameObject deathEffect;
    public int pointsOnDeath;

	LevelManager levelManager;

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	//Change this lol
     if(enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            //ScoreManager.AddPoints(pointsOnDeath);
			//WARNING: DO NOT UNCOMMENT THIS LINE BELOW, IT FREAKS OUT WHEN YOU TRY TO KILL IT
			//levelManager.AddScore(pointsOnDeath);
            Destroy(gameObject);
        }

	}
    public void giveDamage (int damageToGive)
    {
        enemyHealth -= damageToGive;
    }

}
