using UnityEngine;
using System.Collections;

public class GuideGhost : MonoBehaviour {

	public Transform[] points;
    public Transform startPoint;
    public Transform endPoint;
	public float moveSpeed;
	private Animator anim;
	public bool isMoving;
	public bool isEvil;
    public GameObject[] evilReward;
    public GameObject[] goodReward;
    public float distance;

    private int currentPoint;
    private float startTime;
    private float pointLength;
    private float pointDisdtance;

    private Transform player;
	// Use this for initialization
	void Start () 
    {
        currentPoint = 0;
        SetMovePoints();
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
       

        if (Vector2.Distance(player.position, transform.position) < distance)
        {
            Move();
        }

       
	}

    private void SetMovePoints()
    {
        startPoint = points[currentPoint];
        endPoint = points[currentPoint + 1];
        startTime = Time.time;
        pointLength = Vector2.Distance(startPoint.position, endPoint.position);
    }

    private void Move()
    {
        pointDisdtance = (Time.time - startTime) * moveSpeed;
        float journey = pointDisdtance / pointLength;
        transform.position = Vector2.Lerp(startPoint.position, endPoint.position, journey);

        if (journey >= 1.0f && currentPoint + 1 < points.Length - 1)
        {
            currentPoint++;
            SetMovePoints();
        }


    }
}
