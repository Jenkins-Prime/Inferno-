﻿using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	public enum PickupType {
		Coin,
		Health,
		Life,
		Weapon,
	};
	public PickupType pickupType;

	public GameObject particle;
	public AudioClip soundEffect;
	public int amountToAdd;

	LevelManager levelManager;

	void Start () {
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}

	public void Collect() {
		switch (pickupType) {
		case PickupType.Coin:
			levelManager.IncreaseScore (amountToAdd);
			break;
		case PickupType.Health:
			levelManager.IncreaseHealth (amountToAdd);
			break;
		case PickupType.Life:
			levelManager.IncreaseLife (amountToAdd);
			break;
		}

		if(particle != null)
			Instantiate(particle, transform.position, transform.rotation);
		if(soundEffect != null)
			AudioSource.PlayClipAtPoint (soundEffect, transform.position);
		Destroy (gameObject);
	}
}
