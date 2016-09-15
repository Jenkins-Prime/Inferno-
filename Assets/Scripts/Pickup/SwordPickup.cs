using UnityEngine;
using System.Collections;

public class SwordPickup : MonoBehaviour
{
    private PlayerWeapon playerWeapon;

	void Awake ()
    {
        playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeapon>();
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerWeapon.currentWeapon = Weapons.SWORD;
            Destroy(gameObject);
        }
    }
	
}
