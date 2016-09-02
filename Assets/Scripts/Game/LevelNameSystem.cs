using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelNameSystem : MonoBehaviour {

	public Text theText;
	private int levelNumber;

	// Use this for initialization
	void Start () {

		levelNumber = PlayerPrefs.GetInt("PlayerLevelSelectPosition");
		GetLevelName ();

	}

	// Update is called once per frame
	void Update () {

		Debug.Log (PlayerPrefs.GetInt ("PlayerLevelSelectPosition"));

	}



      void GetLevelName()
     {
		if (levelNumber == 0) 
		{
			theText.text = "Dread Plains";
		}

		if (levelNumber == 1) 
		{
			theText.text = "Haunted Corridor";
		}
 }

}