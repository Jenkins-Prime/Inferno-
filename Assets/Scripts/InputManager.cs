using UnityEngine;
using System.Collections;

public static class InputManager {
	static float MainStickX() {
		return Input.GetAxisRaw ("Horizontal");
	}

	static float MainStickY() {
		return Input.GetAxisRaw ("Vertical");
	}

	public static Vector2 MainStick() {
		return new Vector2 (MainStickX(), MainStickY());
	}

	public static bool JumpButton() {
		return Input.GetButtonDown ("Jump");
	}

	public static bool ReleaseJumpButton() {
		return Input.GetButtonUp ("Jump");
	}

	public static bool AttackButton() {
		return Input.GetButtonDown ("Fire1");
	}

	public static bool MeleeButton() {
		return Input.GetButtonDown ("Fire2");
	}

	public static bool ConfirmButton() {
		return Input.GetButtonDown ("Submit");
	}

	public static bool TalkButton() {
		return Input.GetButtonDown(KeyCode.F.ToString()); //change this lol
	}

	public static bool PauseButton() {
		return Input.GetButtonDown ("Cancel");
	}

	public static bool ActionButton() {
		return Input.GetButton (KeyCode.G.ToString ()); //Change this
	}

	public static bool EquipWeaponButton() {
		return Input.GetButtonDown ("Weapon Equip");
	}

	public static bool PreviousWeaponButton() { //change to include both prev & next with one input, negative-positive
		return Input.GetButtonDown ("Weapon Previous");
	}

	public static bool NextWeaponButton() {
		return Input.GetButtonDown ("Weapon Next");
	}
}
