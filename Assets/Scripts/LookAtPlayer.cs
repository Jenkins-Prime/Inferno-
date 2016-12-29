using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {
	public Transform eyes;
    public float moveBy = 0.010f;
	public float xOffset = 0.2f;
	public float yOffset = 0.2f;

	Transform player;
	float moveX;
	float moveY;

	void Start () {
		player = FindObjectOfType<Player> ().transform;
		moveX = transform.position.x;
		moveY = transform.position.y;
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {

			//Check X
			if (player.position.x > gameObject.transform.position.x + xOffset) {
				moveX = transform.position.x + moveBy;
			} else if (player.position.x < gameObject.transform.position.x - xOffset) {
				moveX = transform.position.x - moveBy;
			} else { //inside the range = don't move the eyes on the X axis
				moveX = transform.position.x;
			}
				
			//Check Y
			if (player.position.y > gameObject.transform.position.y + yOffset) {
				moveY = transform.position.y + moveBy;
			} else if (player.position.y < gameObject.transform.position.y - yOffset) {
				moveY = transform.position.y - moveBy;
			} else { //inside the range = don't move the eyes on the Y axis
				moveY = transform.position.y;
			}

			eyes.position = new Vector3 (moveX, moveY, eyes.position.z);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") { //Reset x,y values
			moveX = transform.position.x;
			moveY = transform.position.y;
			eyes.position = new Vector3 (moveX, moveY, eyes.position.z);
		}
	}
}
