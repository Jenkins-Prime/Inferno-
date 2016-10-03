using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
[ExecuteInEditMode]
public class Ladder : MonoBehaviour {
	[Header("Ladder Building")]
	[Range(1, 50)] [SerializeField] int ladderHeight = 1;
	[SerializeField] float ladderYNext = 0.48f;
	[SerializeField] float colliderOffsetY = 0.25f;
	[SerializeField] float colliderSizeY = 0.5f;

	[Header("Player Detection")]
	public float topOffset = 0.05f;
	public float botOffset = 0.01f;

	BoxCollider2D col;
	GameObject ladderPart;

	void Awake () {
		col = GetComponent<BoxCollider2D> ();
		ladderPart = transform.GetChild (0).gameObject;
	}

	public bool CheckPlayerPositionAbove(Vector3 position) {
		return position.y + topOffset > col.bounds.max.y;
	}

	public bool CheckPlayerPositionBelow(Vector3 position) {
		return position.y - botOffset < col.bounds.min.y;
	}

	public void BuildLadder() {
		GameObject ladderNode;

		//delete all the old ones
		for (int i = transform.childCount - 1; i > 0; i--) {
				DestroyImmediate (transform.GetChild (i).gameObject);
			}

		//create new nodes
		for (int i = 1; i < ladderHeight; i++) {
			ladderNode = (GameObject)Instantiate (ladderPart, new Vector3(transform.position.x, transform.position.y + i*ladderYNext, transform.position.z), Quaternion.identity);
			ladderNode.transform.parent = transform;
		}

		//readjust collider's bounds
		col.offset = new Vector2(col.offset.x, colliderOffsetY * ladderHeight);
		col.size = new Vector2(col.size.x, colliderSizeY * ladderHeight);
	}
}
