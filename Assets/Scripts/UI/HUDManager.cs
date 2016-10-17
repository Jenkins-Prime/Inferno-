using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	[SerializeField] Slider healthBar;
	[SerializeField] Text lifeText;
	[SerializeField] Text scoreText;

	public void SetHealthUI(int amount) {
		healthBar.value = (float)amount;
	}

	public void SetLifeUI(int amount) {
		lifeText.text = amount.ToString ();
	}

	public void SetScoreUI(int amount) {
		scoreText.text = amount.ToString ();
	}
}
