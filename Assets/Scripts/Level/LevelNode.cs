using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelNode : MonoBehaviour {
	[HideInInspector] public int id;

	public Sprite currentNode;
	public Sprite unlockedNode;

	SpriteRenderer rend;

	public void Awake() {
		rend = GetComponent<SpriteRenderer> ();
	}

	public void SetCurrentNodeSprite(bool current) {
		if(current)
			rend.sprite = currentNode;
		else
			rend.sprite = unlockedNode;
	}
}
