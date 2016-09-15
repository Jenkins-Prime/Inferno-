using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerWeapon : MonoBehaviour
{
   

    public bool hasSword;
    public bool hasCrossbow;

    public Sprite swordSprite;
    public Sprite crossbowSprite;
    public Sprite NoneSprite;

    private Image weaponImage;
    public Weapons currentWeapon;

    void Awake()
    {
        weaponImage = GameObject.FindGameObjectWithTag("Weapon").transform.GetChild(1).GetComponent<Image>();
    }

	void Start ()
    {
        hasSword = false;
        hasCrossbow = false;
        currentWeapon = Weapons.NONE;
       
	}
	
	void Update ()
    {
        DisplayWeapons();

        if (hasSword && hasCrossbow && Input.GetButtonDown("Switch"))
        {
            SwitchWeapon();
        }

    }

    private void DisplayWeapons()
    {
        switch (currentWeapon)
        {
            case Weapons.NONE:
                hasSword = false;
                hasCrossbow = false;
                weaponImage.sprite = NoneSprite;
                break;

            case Weapons.SWORD:
                hasSword = true;
                weaponImage.sprite = swordSprite;

                if (Input.GetButtonDown("Attack"))
                {
                    Attack();
                }

                break;

            case Weapons.CROSSBOW:
                hasCrossbow = true;
                weaponImage.sprite = crossbowSprite;

                if (Input.GetButtonDown("Attack"))
                {
                    Attack();
                }
                break;
        }
    }

    private void SwitchWeapon()
    {
        if (currentWeapon == Weapons.SWORD)
        {
            currentWeapon = Weapons.CROSSBOW;
        }
        else if(currentWeapon == Weapons.CROSSBOW)
        {
            currentWeapon = Weapons.SWORD;
        }
    }

    private void Attack()
    {
        Debug.Log("You swing your sword");
    }

    private void Shoot()
    {
        Debug.Log("You shoot a projectile");
    }
}

public enum Weapons
{
    NONE,
    SWORD,
    CROSSBOW

}
