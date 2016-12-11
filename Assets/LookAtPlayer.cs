using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

	private Player player;
	public float lookX;
	public float lookY;
	public GameObject eyes;
	public GameObject portrait;
    public float moveBy = 0.010f;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
	}
	
	// Update is called once per frame
	void Update () {

		lookX = player.transform.position.x;
		lookY = player.transform.position.y;

		if (lookX > portrait.transform.position.x) 
		{
			eyes.transform.position = new Vector3 (transform.position.x + moveBy, transform.position.y, transform.position.z);
		}

		if (lookY > portrait.transform.position.y) 
		{
			eyes.transform.position = new Vector3 (transform.position.x, transform.position.y + moveBy, transform.position.z);
		}
	
		if (lookX < portrait.transform.position.x) 
		{
			eyes.transform.position = new Vector3 (transform.position.x - moveBy, transform.position.y, transform.position.z);
		}


		if (lookY < portrait.transform.position.y) 
		{
			
			eyes.transform.position = new Vector3 (transform.position.x, transform.position.y - moveBy, transform.position.z);
		}
	}
}
