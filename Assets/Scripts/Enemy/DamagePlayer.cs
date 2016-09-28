using UnityEngine;
using System.Collections;

public class DamagePlayer : MonoBehaviour {
	[SerializeField] bool killPlayer = false;
	[SerializeField] int damageAmount = 1;

	LevelManager levelManager;

	void Start () {
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}

	public void DealDamage() {
		if (killPlayer) {
			levelManager.DecreaseLife (1);
		} else {
			levelManager.DecreaseHealth (damageAmount);
		}
	}
}
