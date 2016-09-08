using UnityEngine;
using System.Collections;

public class LevelNode : MonoBehaviour {
	public int levelIndex;

	[Header ("UI Params")]
	public Sprite currentNode;
	public Sprite unlockedNode;

	[Header ("Connected Nodes")]
	public LevelNode leftNode;
	public LevelNode rightNode;
	public LevelNode upNode;
	public LevelNode downNode;

	Level level;

	void Awake () { //Needs to be Awake and not start
		level = GameController.instance.LoadLevelData(levelIndex);

		if (level.isUnlocked) {
			SetCurrentNodeSprite (false);
		}
	}

	public void SetCurrentNodeSprite(bool current) {
		if(current)
			GetComponent<SpriteRenderer>().sprite = currentNode;
		else
			GetComponent<SpriteRenderer>().sprite = unlockedNode;
	}

	public string SceneName() {
		return level.sceneName;
	}

	public bool IsUnlocked() {
		return level.isUnlocked;
	}
}
