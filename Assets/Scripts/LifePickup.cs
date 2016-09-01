using UnityEngine;
using System.Collections;

public class LifePickup : MonoBehaviour {

	private LifeManager lifeManager;
	public GameObject lifePickupEffect;

	// Use this for initialization
	void Start () {
	 
		lifeManager = FindObjectOfType<LifeManager>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player") 
		{
			lifeManager.GiveLife ();
			Instantiate (lifePickupEffect, transform.position, transform.rotation);
			Destroy (gameObject);
		}

	}
}
