using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public Weapon weapon;

	Animator anim;
	LevelManager levelManager;
	HUDManager hud;

	bool hasEquiped;
	int weaponCount;
	enum Weapons
	{
		None,
		Scythe,
		Crossbow
	};
	Weapons curWeapon;

	void Start () {
		anim = GetComponent<Animator> ();
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
		hud = GameObject.FindGameObjectWithTag ("HUD").GetComponent<HUDManager> ();

		hasEquiped = false;
		weaponCount = 0; //import already hasWeapons
		curWeapon = Weapons.None;
	}

	void Update () {
		if (InputManager.EquipWeaponButton() && weaponCount> 0) {
			WeaponEquip ();
		} else if (InputManager.PreviousWeaponButton ()) {
			WeaponPrevious ();
		} else if (InputManager.NextWeaponButton ()) {
			WeaponNext ();
		}
	}

	void WeaponEquip() {
		hasEquiped = !hasEquiped;
		hud.EquipWeapon(hasEquiped, (int)curWeapon-1);
		SetAnimState();
	}

	void WeaponPrevious() {
		if(HasWeapons() > 1) {
			if (hasEquiped) {
				WeaponEquip(); //Unequip old one
				hud.SelectWeapon(false, (int)curWeapon-1); //deselct 
				curWeapon = (curWeapon == Weapons.Scythe) ? Weapons.Crossbow : (curWeapon - 1);
				WeaponEquip ();//Equip current one
				//Debug.Log ("CurWeapon: " + curWeapon);
			} else {
				hud.SelectWeapon(false, (int)curWeapon-1); //deselect
				curWeapon = (curWeapon == Weapons.Scythe) ? Weapons.Crossbow : (curWeapon - 1);
				hud.SelectWeapon(true, (int)curWeapon-1); //select
				//Debug.Log ("CurWeapon: " + curWeapon);
			}
		}
	}

	void WeaponNext() {
		if(HasWeapons() > 1) {
			if(hasEquiped) {
				WeaponEquip(); //Unequip old one
				hud.SelectWeapon(false, (int)curWeapon-1); //deselct 
				curWeapon = (curWeapon == Weapons.Crossbow) ? Weapons.Scythe : (curWeapon + 1);
				WeaponEquip ();//Equip current one
				//Debug.Log("CurWeapon: " + curWeapon);
			} else {
				hud.SelectWeapon(false, (int)curWeapon-1); //deselect
				curWeapon = (curWeapon == Weapons.Scythe) ? Weapons.Crossbow : (curWeapon - 1);
				hud.SelectWeapon(true, (int)curWeapon-1); //select
				//Debug.Log ("CurWeapon: " + curWeapon);
			}
		}
	}

	int HasWeapons() {
		int count = 0;
		if(GameController.instance.playerData.hasScythe)
			count++;
		if(GameController.instance.playerData.hasCrossbow)
			count++;

		return count;
	}
		
	void SetAnimState() {
		switch (curWeapon) {
		case Weapons.Scythe:
			anim.SetBool ("Scythe", hasEquiped);
			break;
		case Weapons.Crossbow:
			anim.SetBool ("Crossbow", hasEquiped);
			break;
		default:
			anim.SetBool ("Scythe", false);
			anim.SetBool ("Crossbow", false);
			break;
		}
	}
	//===== Public functions =====
	public void AddWeapon(int id) {
		switch (id) {
		case 0: //Scythe
			if (!GameController.instance.playerData.hasScythe) { //if it hasn't the scythe yet
				GameController.instance.playerData.hasScythe = true;
				if(curWeapon != Weapons.None)
					hud.SelectWeapon (false, 1); //Deselect crossbow. Weird method, i know == temp solution
				curWeapon = Weapons.Scythe;
				weaponCount++;
			}
			break;
		case 1: //Crossbow
			if (!GameController.instance.playerData.hasCrossbow) { //if it hasn't the crossbow yet
				GameController.instance.playerData.hasCrossbow = true;
				if(curWeapon != Weapons.None)
					hud.SelectWeapon(false, 0); //Deselect scythe. Weird method, i know == temp solution
				curWeapon = Weapons.Crossbow;
				weaponCount++;
			}
			break;
		}
		hud.AddWeapon ((int)curWeapon-1);
	}
}
