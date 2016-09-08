using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectManager : MonoBehaviour {
	public float yOffset;
	public Text panelText;
	public LevelNode currentNode;
	public int currentLevel;
	public float moveSpeed = 2f;

	bool playerMoving;

	Animator playerAnim;

	void Start () {
		playerMoving = false;
		playerAnim = GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ();

		SetLevelNode (currentNode);
	}

	void Update () {
		if (playerMoving)
			MovePlayerToNode (currentNode);
		else
			CheckInput ();
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

	void SetLevelNode(LevelNode node) {
		Vector3 target = node.transform.position;
		target.y += yOffset;
		playerAnim.transform.position = target;
		playerMoving = false;
		playerAnim.SetBool ("isMoving", false);

		panelText.text = node.name;
		currentNode = node;
		currentNode.SetCurrentNodeSprite (true);
	}

	void CheckInput() {
		Vector2 input = InputManager.MainStick ();

		if (input.x > 0) { //Pressed Right
			if (currentNode.rightNode != null && currentNode.rightNode.IsUnlocked()) {
				playerMoving = true;
				playerAnim.SetBool ("isMoving", true);
				if (playerAnim.transform.localScale.x < 0) //Turn player if needed
					playerAnim.transform.localScale = new Vector3 (1f, 1f, 1f);

				currentNode.SetCurrentNodeSprite (false);
				currentNode = currentNode.rightNode;
				GameController.instance.currentLevel = currentNode.levelIndex;
			}
		} else if (input.x < 0) { //Pressed left
			if (currentNode.leftNode != null && currentNode.leftNode.IsUnlocked()) {
				playerMoving = true;
				playerAnim.SetBool ("isMoving", true);
				if (playerAnim.transform.localScale.x > 0) //Turn player if needed
					playerAnim.transform.localScale = new Vector3 (-1f, 1f, 1f);

				currentNode.SetCurrentNodeSprite (false);
				currentNode = currentNode.leftNode;
				GameController.instance.currentLevel = currentNode.levelIndex;
			}
		} else if (input.y > 0) { //Pressed up
			if (currentNode.upNode != null && currentNode.upNode.IsUnlocked()) {
				playerMoving = true;
				playerAnim.SetBool ("isMoving", true);

				currentNode.SetCurrentNodeSprite (false);
				currentNode = currentNode.upNode;
				GameController.instance.currentLevel = currentNode.levelIndex;
			}
		} else if (input.y < 0) { //Pressed down
			if (currentNode.downNode != null && currentNode.downNode.IsUnlocked()) {
				playerMoving = true;
				playerAnim.SetBool ("isMoving", true);

				currentNode.SetCurrentNodeSprite (false);
				currentNode = currentNode.downNode;
				GameController.instance.currentLevel = currentNode.levelIndex;
			}
		} else if (InputManager.ConfirmButton()) { //Pressed confirm button to play level
			SceneManager.LoadScene (currentNode.SceneName()); 
		}
	}
}
