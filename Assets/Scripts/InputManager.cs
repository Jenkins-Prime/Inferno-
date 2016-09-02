using UnityEngine;
using System.Collections;

public static class InputManager {
	public static float MainStickX() {
		return Input.GetAxisRaw ("Horizontal");
	}

	public static float MainStickY() {
		return Input.GetAxisRaw ("Vertical");
	}

	public static Vector2 MainStick() {
		return new Vector2 (MainStickX(), MainStickY());
	}

	public static bool JumpButton() {
		return Input.GetButtonDown ("Jump");
	}

	public static bool FireButton() {
		return Input.GetButtonDown ("Fire1");
	}

	public static bool MeleeButton() {
		return Input.GetButtonDown ("Fire2");
	}

	public static bool ActionButton() {
		return Input.GetButtonDown ("Submit");
	}

	public static bool TalkButton() {
		return Input.GetButtonDown(KeyCode.F.ToString()); //change this lol
	}

	public static bool PauseButton() {
		return Input.GetButtonDown ("Cancel");
	}
}
