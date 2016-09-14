using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {

	public int pointsToAdd;
	public GameObject coinParticles;
	public AudioSource soulSoundEffect;

	LevelManager levelManager;

	void Start() {
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.GetComponent<PlayerController>() == null)
			return;

		levelManager.AddScore (pointsToAdd);
		//ScoreManager.AddPoints(pointsToAdd);
		Instantiate(coinParticles, transform.position, transform.rotation);
		soulSoundEffect.Play();
		Destroy(gameObject);
	}
}
