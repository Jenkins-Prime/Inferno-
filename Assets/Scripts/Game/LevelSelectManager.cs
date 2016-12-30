using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectManager : MonoBehaviour {
	public Text panelText;
	public float moveSpeed = 2f;
	public List<Level> levels = new List<Level>();

	bool playerMoving;
	int current;
	float yOffset = 0.1f;
	Animator playerAnim;

	void Start () {
		playerMoving = false;
		playerAnim = GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ();

		ConnectLevelNodes ();
		current = 0;
		SetLevelNode (levels[current].node);
	}

	void Update () {
		if (playerMoving)
			MovePlayerToNode (levels [current].node);
		else
			CheckInput ();
	}

	void ConnectLevelNodes() {
		//assign a unique id in each node
		for (int i = 0; i < levels.Count; i++) {
			levels [i].node.id = i;

			if(GameController.instance.GetLevelData (i).isUnlocked)
				levels [i].node.SetCurrentNodeSprite (false);
		}

		//Connect the right with left, and the up with down
		for (int i = 0; i < levels.Count; i++) {
			if (levels [i].rightNode != null)
				levels [levels [i].rightNode.id].leftNode = levels [i].node;
			if (levels[i].upNode != null)
				levels [levels [i].upNode.id].downNode = levels [i].node;
		}
	}
		
	void SetLevelNode(LevelNode node) {
		Vector3 target = node.transform.position;
		target.y += yOffset;
		playerAnim.transform.position = target;
		playerMoving = false;
		playerAnim.SetBool ("isMoving", false);

		panelText.text = node.name;
		node.SetCurrentNodeSprite (true);
	}
		
	void MovePlayerToNode(LevelNode node) {
		Vector3 target = node.transform.position;
		target.y += yOffset;
		if (Vector3.Distance (playerAnim.transform.position, target) > 0.1f) {
			playerAnim.transform.position = Vector3.MoveTowards (playerAnim.transform.position, target, moveSpeed * Time.deltaTime);
		} else {
			SetLevelNode (node);
		}
	}

	void SetNextPosition(int nextNode) {
		playerMoving = true;
		playerAnim.SetBool ("isMoving", true);
		levels[current].node.SetCurrentNodeSprite (false);
		GameController.instance.currentLevel = current = nextNode;
	}

	void CheckInput() {
		Vector2 input = InputManager.Instance.MainStick ();

		if (input.x > 0 && levels[current].rightNode != null && GameController.instance.GetLevelData (levels[current].rightNode.id).isUnlocked) {//levels[levels[current].rightNode.id].isUnlocked) { //Pressed Right
			SetNextPosition (levels[current].rightNode.id);

			if (playerAnim.transform.localScale.x < 0) //Turn player if needed
				playerAnim.transform.localScale = new Vector3 (1f, 1f, 1f);
		} else if (input.x < 0 && levels[current].leftNode != null && GameController.instance.GetLevelData (levels[current].leftNode.id).isUnlocked) {//levels[levels[current].leftNode.id].isUnlocked) { //Pressed left
			SetNextPosition (levels[current].leftNode.id);

			if (playerAnim.transform.localScale.x > 0) //Turn player if needed
				playerAnim.transform.localScale = new Vector3 (-1f, 1f, 1f);
		} else if (input.y > 0 && levels[current].upNode != null && GameController.instance.GetLevelData (levels[current].upNode.id).isUnlocked) {//levels[levels[current].upNode.id].isUnlocked) { //Pressed up
			SetNextPosition (levels[current].upNode.id);
		} else if (input.y < 0 && levels[current].downNode != null && GameController.instance.GetLevelData (levels[current].downNode.id).isUnlocked) {//levels[levels[current].downNode.id].isUnlocked) { //Pressed down
			SetNextPosition (levels[current].downNode.id);
		} else if (InputManager.Instance.ConfirmButton()) { //Pressed confirm button to play level
			GameController.instance.currentLevel = current;
			GameController.instance.SaveGame();
			SceneManager.LoadScene (GameController.instance.GetLevelData (current).sceneName); //levels[current].sceneName); 
		}
	}
}

[System.Serializable]
public class Level {
	public LevelNode node;
	public LevelNode rightNode;
	public LevelNode upNode;
	//Hidden in inspector
	[HideInInspector] public LevelNode leftNode;
	[HideInInspector] public LevelNode downNode;

	//[Header("Level Data")]
	//public string sceneName;
	//public int startTime;
}
