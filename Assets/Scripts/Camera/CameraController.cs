using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public bool isFollowing;
	[SerializeField] float xOffset;
	[SerializeField] float yOffset;

	Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
        isFollowing = true;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (isFollowing)
            transform.position = new Vector3(player.position.x + xOffset, player.position.y + yOffset, transform.position.z);
	}
}
