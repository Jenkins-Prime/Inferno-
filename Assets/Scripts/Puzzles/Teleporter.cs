using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public bool jump;
	public GameObject target;
	public float adjustY;
	public float adjustX;


	void OnTriggerEnter2D (Collider2D other)
	{
		if (!jump) {
			if (other.tag == "Player") {
				jump = true;
				other.gameObject.transform.position = new Vector3 (target.transform.position.x + adjustX, target.transform.position.y + adjustY, 0);
			}
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player") {
			jump = false;
		}
	}

}
