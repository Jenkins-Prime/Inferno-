using UnityEngine;
using System.Collections;

public class GuideGhost : MonoBehaviour {

	public Transform[] points;
	public int moveSpeed;
	private Animator anim;
	public bool isTriggered;
	public bool isMoving;
	public Transform currentPoint;
	public int pointSelection;
	public bool isEvil;
    public GameObject[] evilReward;
    public GameObject[] goodReward;

	// Use this for initialization
	void Start () {
		currentPoint = points[pointSelection];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isTriggered && isMoving)
            {
			transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

			if(currentPoint == points[pointSelection])
				{
					isTriggered = false;
					isMoving = false;
				}
		}
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		for (int index = 0; index < points.Length; index++)
        {
            if (currentPoint == points[index])
            {
                isTriggered = false;
                isMoving = false;
            }
        }

        if (transform.position == currentPoint.position) 
		{
			pointSelection++;
			currentPoint = points[pointSelection];
		}
	}
}
