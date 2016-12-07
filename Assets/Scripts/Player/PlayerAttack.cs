using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public Vector3 meleeCenter;
	public float meleeRange;
	public LayerMask enemyMask;
	public GameObject bullet;
	public Transform firePoint;

	Animator anim;
	LevelManager levelManager;
	HUDManager hud;

	bool hasEquiped;
	bool isAttacking;
	int weaponCount;
	enum Weapons
	{
		None,
		Scythe,
		Crossbow
	};
	Weapons curWeapon;

	Player player;

	void Start () {
		anim = GetComponent<Animator> ();
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
		hud = GameObject.FindGameObjectWithTag ("HUD").GetComponent<HUDManager> ();

		hasEquiped = false;
		isAttacking = false;
		weaponCount = 0; //import already hasWeapons
		curWeapon = Weapons.None;

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	void Update () {
		if (InputManager.AttackButton () && hasEquiped && !anim.GetBool ("Attack")) {
			WeaponAttack ();
		} else if (InputManager.EquipWeaponButton() && weaponCount> 0) {
			WeaponEquip ();
		} else if (InputManager.PreviousWeaponButton ()) {
			WeaponPrevious ();
		} else if (InputManager.NextWeaponButton ()) {
			WeaponNext ();
		}
	}

	void WeaponAttack() {
		if (curWeapon == Weapons.Scythe) {
			anim.SetTrigger("Attack"); //set animator state to attack

			//check if ther is enemy on the weapon's hit range
			Vector3 meleePos = (player.velocity.x < 0) ? new Vector3(transform.position.x - meleeCenter.x, transform.position.y + meleeCenter.y, transform.position.z + meleeCenter.z) : transform.position + meleeCenter;
			Collider2D col2D;
			col2D = Physics2D.OverlapCircle (meleePos, meleeRange, enemyMask);
			if (col2D != null) {
				col2D.GetComponent<EnemyHealthManager> ().GiveDamage (1); //TODO: change 1 to scythe's damage
			}
		} else if (curWeapon == Weapons.Crossbow) {
			//attack timer check
			Instantiate (bullet, firePoint.position, firePoint.rotation); //change this to .enable for cpu optimization
			//timer update
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
					hud.SelectWeapon (false, 1); //Deselect crossbow. Weird method I know == temp solution
				curWeapon = Weapons.Scythe;
				weaponCount++;
			}
			break;
		case 1: //Crossbow
			if (!GameController.instance.playerData.hasCrossbow) { //if it hasn't the crossbow yet
				GameController.instance.playerData.hasCrossbow = true;
				if(curWeapon != Weapons.None)
					hud.SelectWeapon(false, 0); //Deselect scythe. Weird method I know == temp solution
				curWeapon = Weapons.Crossbow;
				weaponCount++;
			}
			break;
		}
		hud.AddWeapon ((int)curWeapon-1);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position + meleeCenter, meleeRange);
	}
}
