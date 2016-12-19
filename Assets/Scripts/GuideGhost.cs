using UnityEngine;
using System.Collections;

public class GuideGhost : MonoBehaviour
{

	public Transform[] points;
	public float moveSpeed;
	private Animator anim;
	public bool isMoving;
	public bool isEvil;
    public GameObject[] evilReward;
    public GameObject[] goodReward;
    public float distance;

    private int currentPoint;

    private Transform player;

	void Start () 
    {
        currentPoint = -1;
        SetMovePoints();
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update () 
	{
        if (isMoving)
        {
            Move();
        }
	}

    private void SetMovePoints()
    {
        if (currentPoint < points.Length - 1)
        {
            currentPoint++;
            
        }
        else
        {
            currentPoint = points.Length - 1;
            Debug.Log("End reached");
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, points[currentPoint].position) < distance)
        {
            transform.position = points[currentPoint].position;
            isMoving = false;
            SetMovePoints();
    }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isMoving = true;
        }
        
    }
}
