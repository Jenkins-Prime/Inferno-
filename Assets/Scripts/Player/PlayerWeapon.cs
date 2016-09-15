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
    public Weapons currentWeapon;
    public int maxAmmo;

    private Image weaponImage;
    private Text ammoAmount;
    private int currentAmmo;





    void Awake()
    {
        weaponImage = GameObject.FindGameObjectWithTag("Weapon").transform.GetChild(1).GetComponent<Image>();
        ammoAmount = GameObject.FindGameObjectWithTag("Weapon").transform.GetChild(2).GetComponent<Text>();
    }

	void Start ()
    {
        hasSword = false;
        hasCrossbow = false;
        currentWeapon = Weapons.NONE;
        ammoAmount.enabled = false;
        currentAmmo = maxAmmo;
        ammoAmount.text = currentAmmo.ToString();
	}
	
	void Update ()
    {
        DisplayWeapons();


        if (Input.GetButtonDown("Switch") && hasSword && hasCrossbow)
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
                ammoAmount.enabled = false;
                break;

            case Weapons.SWORD:
                hasSword = true;
                weaponImage.sprite = swordSprite;
                ammoAmount.enabled = false;
         
                if (Input.GetButtonDown("Attack"))
                {
                    Attack();
                }

                break;

            case Weapons.CROSSBOW:
                hasCrossbow = true;
                weaponImage.sprite = crossbowSprite;
                ammoAmount.enabled = true;

                if (Input.GetButtonDown("Attack"))
                {
                    Shoot();
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
        if (currentAmmo <= 0)
        {
            currentAmmo = 0;
            Debug.Log("Out of ammo");
        }
        else
        {
            currentAmmo--;
        }

        ammoAmount.text = currentAmmo.ToString();
    }
}

public enum Weapons
{
    NONE,
    SWORD,
    CROSSBOW

}
