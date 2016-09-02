using UnityEngine;
using System.Collections;

public class LevelNode : MonoBehaviour {
	[Header ("UI Params")]
	public Sprite currentNode;
	public Sprite unlockedNode;

	[Header ("Level loading")]
	public string levelSceneName;
	public bool isUnlocked;
	public int startTime;

	[Header ("Connected Nodes")]
	public LevelNode leftNode;
	public LevelNode rightNode;
	public LevelNode upNode;
	public LevelNode downNode;

	void Awake () { //Needs to be Awake and not start
		if (isUnlocked) {
			SetCurrentNodeSprite (false);
		}
	}

	public void SetCurrentNodeSprite(bool current) {
		if(current)
			GetComponent<SpriteRenderer>().sprite = currentNode;
		else
			GetComponent<SpriteRenderer>().sprite = unlockedNode;
	}
}
