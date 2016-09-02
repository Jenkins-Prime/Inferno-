using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	public int healthToGive;
	public GameObject healthParticles;
	public AudioSource soulSoundEffect;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.GetComponent<PlayerController>() == null)
			return;

		//HealthManager.HurtPlayer (-healthToGive);
		Instantiate(healthParticles, transform.position, transform.rotation);
		soulSoundEffect.Play();
		Destroy(gameObject);
	}
}