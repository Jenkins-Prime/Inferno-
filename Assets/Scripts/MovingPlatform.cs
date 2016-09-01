using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public GameObject platform;
	public float moveSpeed;
	public Transform currentpoint;
	public Transform[] points;
	public int pointSelection;

	// Use this for initialization
	void Start () {
		currentpoint = points[pointSelection];
	
	}
	
	// Update is called once per frame
	void Update () {
		platform.transform.position = Vector3.MoveTowards (platform.transform.position, currentpoint.position, Time.deltaTime * moveSpeed);
		if (platform.transform.position == currentpoint.position) 
		{
			pointSelection++;

			if (pointSelection == points.Length) 
			{
				pointSelection = 0;
			}
		
		}

		currentpoint = points [pointSelection];
	}
}
