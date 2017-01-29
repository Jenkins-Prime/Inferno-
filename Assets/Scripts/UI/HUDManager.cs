using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	[Header ("MP Bar")]
	[SerializeField] Image MPBar;

	[Header ("Collectibles")]
	[SerializeField] Text blueSouls;
	[SerializeField] Text keys;

	[Header ("Health")]
	[SerializeField] Slider healthBar;

	[Header ("Lives")]
	[SerializeField] Text lifeText;

	//method for the mp bar goes here

	public void SetBlueSouls(int amount)
    {
		blueSouls.text = amount.ToString ();
	}

	public void SetKeys(int amount)
    {
		keys.text = amount.ToString ();
	}

	public void SetHealthUI(int amount)
    {
		//healthBar.value = (float)amount;
	}

	public void SetLifeUI(int amount) {
		lifeText.text = amount.ToString ();
	}
}
