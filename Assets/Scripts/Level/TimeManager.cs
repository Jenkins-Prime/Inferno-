using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimeManager : MonoBehaviour {
	public float startingTime;

	float countingTime;

	Text theText;
	HealthManager healthManager;
	PauseMenu thePauseMenu;

	// Use this for initialization
	void Start () {
		theText = GetComponent<Text> ();
		thePauseMenu = FindObjectOfType<PauseMenu> ();
		countingTime = startingTime;
		healthManager = FindObjectOfType<HealthManager> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (thePauseMenu.isPaused)
			return;
		
		countingTime -= Time.deltaTime;

		if(countingTime <= 0) {
			healthManager.KillPlayer();
		}

		theText.text = "" + Mathf.Round (countingTime);
	}

	public void ResetTime() {
		countingTime = startingTime;
	}
}
