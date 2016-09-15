using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject[] weapons;

    public bool hasSword;
    public bool hasCrossbow;

    public Sprite swordSprite;
    public Sprite crossbowSprite;


    private Image weaponImage;
    private int currentWeapon;
    private int maxWeapons;

    void Awake()
    {
        weaponImage = GameObject.FindGameObjectWithTag("Weapon").transform.GetChild(1).GetComponent<Image>();
    }

	void Start ()
    {
        hasSword = false;
        hasCrossbow = false;
        currentWeapon = 0;
        maxWeapons = 2;
       
	}
	
	void Update ()
    {
        ShowWeapon();

        if (Input.GetButtonDown("Switch"))
        {
            SwitchWeapon();
        }

        if (Input.GetButtonDown("Attack") && currentWeapon == 1)
        {
            Attack();
        }
        else if (Input.GetButtonDown("Attack") && currentWeapon == 2)
        {
            Shoot();
        }
        
	}

    private void ShowWeapon()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            hasSword = true;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            hasCrossbow = true;
        }
    }

    private void SwitchWeapon()
    {
        if (hasSword && hasCrossbow)
        {
            if (currentWeapon > weapons.Length - 1)
            {
                currentWeapon = 0;

            }
            else
            {
                Switch(weapons[currentWeapon++]);
            }
        }
    }

    private void Switch(GameObject selectedWeapon)
    {
        for (int index = 0; index < weapons.Length; index++)
        {
            if (weapons[index] == selectedWeapon)
            {
                weapons[index].SetActive(true);
                weaponImage.sprite = selectedWeapon.GetComponent<Image>().sprite;
            }
        }
    }

    private void Attack()
    {
        Debug.Log("Attack");
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
    }
}
