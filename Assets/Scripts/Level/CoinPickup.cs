using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {

	public int pointsToAdd;
	public GameObject coinParticles;
	public AudioSource soulSoundEffect;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.GetComponent<PlayerController>() == null)
			return;

		ScoreManager.AddPoints(pointsToAdd);
		Instantiate(coinParticles, transform.position, transform.rotation);
		soulSoundEffect.Play();
		Destroy(gameObject);
	}
}
