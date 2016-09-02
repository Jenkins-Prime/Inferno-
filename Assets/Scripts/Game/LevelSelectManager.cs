using UnityEngine;
using System.Collections;

public class LevelSelectManager : MonoBehaviour {
	//playerPos
	//or curNode pos
	//for lvl1 (leftNode = null, rightNode = lvl2)
	//for lvl2 (leftNode = (lvl1), rightNode = null)
	public Transform player;
	public Transform[] levels;
	int curLevel;
	int minLevel;
	int maxLevel;

	/*
	public string[] levelTags;
	public GameObject[] locks;
	public bool[] levelUnlocked;
	public int positionSelector;
	public float distanceBelowLock;
	public string[] levelName;
	public float moveSpeed;
	private bool isPressed;
	*/

	void Start () {
		curLevel = 0;
		minLevel = 0;
		maxLevel = levels.Length - 1;
		player.position = levels [curLevel].position;

		/*for(int i = 0; i < levelTags.Length; i++) {
			if(PlayerPrefs.GetInt(levelTags[i]) == null) {
				levelUnlocked[i] = false;
			} else if (PlayerPrefs.GetInt(levelTags[i]) == 0) {
				levelUnlocked[i] = false;
			} else {
				levelUnlocked[i] = true;
			}

			if(levelUnlocked[i]) {
				locks[i].SetActive (false);
			}
		}

		positionSelector = PlayerPrefs.GetInt("PlayerLevelSelectPosition");
		transform.position = locks[positionSelector].transform.position + new Vector3(0, distanceBelowLock, 0);
		*/
	}

	void Update () {
		float input = InputManager.MainStickX ();

		if (input > 0) {
			//find right node
			if (curLevel < maxLevel) {
				curLevel++;
				player.position = levels [curLevel].position;
			}
		} else if(input < 0) {
			if (curLevel > minLevel) {
				curLevel--;
				player.position = levels [curLevel].position;
			}
		}

		if (InputManager.ActionButton()) {
			//need to change that
			UnityEngine.SceneManagement.SceneManager.LoadScene ("TestScene"); 
		}

		/*if(!isPressed) {
			if(Input.GetAxis("Horizontal") > 0.25f) {
				positionSelector +=1;
				isPressed = true;
			}

			if(Input.GetAxis("Horizontal") < -0.25f) {
				positionSelector -=1;
				isPressed = true;
			}

			if(positionSelector >= levelTags.Length) {
				positionSelector = levelTags.Length - 1;
			}

			if(positionSelector < 0)
				positionSelector = 0;
		}

		if(isPressed) {
			if(Input.GetAxis("Horizontal") < 0.25f && Input.GetAxis("Horizontal") > -0.25f) {
				isPressed = false;
			}
		}

		transform.position = Vector3.MoveTowards(transform.position, locks[positionSelector].transform.position + new Vector3(0, distanceBelowLock, 0), moveSpeed * Time.deltaTime);

		if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")) {
			if(levelUnlocked[positionSelector]) {
				PlayerPrefs.SetInt("PlayerLevelSelectPosition", positionSelector);
				Application.LoadLevel(levelName[positionSelector]);
			}
		}*/
	}
}
