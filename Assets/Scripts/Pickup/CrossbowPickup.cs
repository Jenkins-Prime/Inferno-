using UnityEngine;
using System.Collections;

public class CrossbowPickup : MonoBehaviour
{
    private PlayerWeapon playerWeapon;

    void Awake()
    {
        playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeapon>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerWeapon.currentWeapon = Weapons.CROSSBOW;
            Destroy(gameObject);
        }
    }
}
