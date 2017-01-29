using UnityEngine;
using System.Collections;

public class InputManager
{
    private static InputManager instance;

    private InputManager() {}

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InputManager();
            }

            return instance;
        }
    }

	public float MainStickX()
    {
		return Input.GetAxisRaw ("Horizontal");
	}

    public float MainStickY()
    {
		return Input.GetAxisRaw ("Vertical");
	}

	public Vector2 MainStick()
    {
		return new Vector2 (MainStickX(), MainStickY());
	}

	public bool JumpButton()
    {
		return Input.GetButtonDown ("Jump");
	}

	public bool ReleaseJumpButton()
    {
		return Input.GetButtonUp ("Jump");
	}

	public bool AttackButton()
    {
		return Input.GetButtonDown ("Fire1");
	}

	public bool MeleeButton()
    {
		return Input.GetButtonDown ("Fire2");
	}

	public bool ConfirmButton()
    {
		return Input.GetButtonDown ("Submit");
	}

	public bool TalkButton()
    {
		return Input.GetButtonDown("Talk");
	}

	public bool PauseButton()
    {
		return Input.GetButtonDown ("Cancel");
	}

	public bool ActionButton()
    {
		return Input.GetButtonDown("Action");
	}

    public bool PossessEnemy()
    {
        return Input.GetButtonDown("PossessEnemy");
    }

}
