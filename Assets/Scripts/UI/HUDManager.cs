using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	[Header ("MP Bar")]
	[SerializeField] Image MPBar;

	[Header ("Weapons")]
	//Scythe
	[SerializeField] Image scytheOutline;
	[SerializeField] Image scytheBackground;
	[SerializeField] Image scytheWeapon;
	//Crossbow
	[SerializeField] Image crossbowOutline;
	[SerializeField] Image crossbowBackground;
	[SerializeField] Image crossbowWeapon;
	[SerializeField] Text crossbowAmmo;

	[Header ("Collectibles")]
	[SerializeField] Text blueSouls;
	[SerializeField] Text keys;

	[Header ("Health")]
	[SerializeField] Slider healthBar;

	[Header ("Lives")]
	[SerializeField] Text lifeText;

	[SerializeField] Text scoreText; //TODO: remove that line 

	Color32 lightBlue = new Color32(0, 44, 255, 255);
	Color32 darkBlue = new Color32(0, 0, 128, 255);
	Color32 gold = new Color32(255, 220, 0, 255);
	Color32 darkGreen = new Color32 (0, 180, 0, 255);

	void Start() {
		scytheOutline.color = lightBlue;
		scytheBackground.color = darkBlue;
		scytheWeapon.enabled = false; //import pls

		crossbowOutline.color = lightBlue;
		crossbowBackground.color = darkBlue;
		crossbowWeapon.enabled = false; //import pls
		crossbowAmmo.enabled = false; //import pls
	}

	//method for the mp bar goes here

	public void AddWeapon(int id) {
		if (id == 0) {
			scytheOutline.color = Color.yellow;
			scytheBackground.color = gold;
			scytheWeapon.enabled = true;
		} else if (id == 1) {
			crossbowOutline.color = Color.yellow;
			crossbowBackground.color = gold;
			crossbowWeapon.enabled = true;
			crossbowAmmo.enabled = true;
		}
	}

	public void SelectWeapon(bool select, int id) {
		if (select) {
			if (id == 0) {
				scytheOutline.color = Color.yellow;
				scytheBackground.color = gold;
			} else if (id == 1) {
				crossbowOutline.color = Color.yellow;
				crossbowBackground.color = gold;
			}
		} else {
			if (id == 0) {
				scytheOutline.color = lightBlue;
				scytheBackground.color = darkBlue;
			} else if (id == 1) {
				crossbowOutline.color = lightBlue;
				crossbowBackground.color = darkBlue;
			}
		}
	}

	public void EquipWeapon(bool equip, int id) {
		if (equip) {
			if (id == 0) {
				scytheOutline.color = Color.green;
				scytheBackground.color = darkGreen;
			} else if (id == 1) {
				crossbowOutline.color = Color.green;
				crossbowBackground.color = darkGreen;
			}
		} else {
			if (id == 0) {
				scytheOutline.color = Color.yellow;
				scytheBackground.color = gold;
			} else if (id == 1) {
				crossbowOutline.color = Color.yellow;
				crossbowBackground.color = gold;
			}
		}
	}

	public void SetAmmo(int amount) {
		crossbowAmmo.text = amount.ToString ();
	}

	public void SetBlueSouls(int amount) {
		blueSouls.text = amount.ToString ();
	}

	public void SetKeys(int amount) {
		keys.text = amount.ToString ();
	}

	public void SetHealthUI(int amount) {
		//healthBar.value = (float)amount;
	}

	public void SetLifeUI(int amount) {
		lifeText.text = amount.ToString ();
	}

	//!!!!TODO: Remove that method !!!!
	public void SetScoreUI(int amount) {
		//scoreText.text = amount.ToString ();
	}
}
