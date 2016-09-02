using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	[SerializeField] Slider healthBar;
	[SerializeField] Text lifeText;
	[SerializeField] Text timeText;
	[SerializeField] Text scoreText;

	/*// Use this for initialization
	void Start () {
	
	}*/

	public void SetHealthUI(int amount) {
		healthBar.value = (float)amount;
	}

	public void SetLifeUI(int amount) {
		lifeText.text = amount.ToString ();
	}

	public void SetScoreUI(int amount) {
		scoreText.text = amount.ToString ();
	}

	public void SetTimeUI(int amount) {
		timeText.text = amount.ToString ();
	}
}
