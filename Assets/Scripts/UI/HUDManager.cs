using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	[SerializeField] Slider healthBar;
	[SerializeField] Text lifeText;
	[SerializeField] Text scoreText;
	[SerializeField] Image scytheContainer;
	[SerializeField] Image scytheWeapon;
	[SerializeField] Image crossbowContainer;
	[SerializeField] Image crossbowWeapon;
	[SerializeField] Text crossbowAmmo;

	//remove this
	void Start() {
		scytheContainer.color = Color.black;
		scytheWeapon.enabled = false;

		crossbowContainer.color = Color.black;
		crossbowWeapon.enabled = false;
		crossbowAmmo.enabled = false;
	}

	public void SetHealthUI(int amount) {
		healthBar.value = (float)amount;
	}

	public void SetLifeUI(int amount) {
		lifeText.text = amount.ToString ();
	}

	public void SetScoreUI(int amount) {
		scoreText.text = amount.ToString ();
	}

	public void AddWeapon(int id) {
		if (id == 0) {
			scytheContainer.color = Color.yellow;
			scytheWeapon.enabled = true;
		} else if (id == 1) {
			crossbowContainer.color = Color.yellow;
			crossbowWeapon.enabled = true;
			crossbowAmmo.enabled = true;
		}
	}

	public void SelectWeapon(bool select, int id) {
		if (select) {
			if (id == 0) {
				scytheContainer.color = Color.yellow;
			} else if (id == 1) {
				crossbowContainer.color = Color.yellow;
			}
		} else {
			if (id == 0) {
				scytheContainer.color = Color.white;
			} else if (id == 1) {
				crossbowContainer.color = Color.white;
			}
		}
	}

	public void EquipWeapon(bool equip, int id) {
		if (equip) {
			if (id == 0) {
				scytheContainer.color = Color.green;
			} else if (id == 1) {
				crossbowContainer.color = Color.green;
			}
		} else {
			if (id == 0) {
				scytheContainer.color = Color.yellow;
			} else if (id == 1) {
				crossbowContainer.color = Color.yellow;
			}
		}
	}
}
