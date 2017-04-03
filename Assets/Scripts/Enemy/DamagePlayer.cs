using UnityEngine;
using System.Collections;

public class DamagePlayer : MonoBehaviour {
	[SerializeField] bool killPlayer = false;
	[SerializeField] int damageAmount;

	LevelManager levelManager;

	void Start () {
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}

	private void OnTriggerEnter2D(Collider2D col)
    {
	    if(col.tag == "Player")
        {
            levelManager.DecreaseHealth(damageAmount);
        }	
	}
}
