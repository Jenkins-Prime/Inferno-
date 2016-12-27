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
            //SpawnItems(3.0f);  
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, points[currentPoint].position) < distance)
        {
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

    private void SpawnItems(float duration)
    {
        float random = Random.value;

        if (random < 0.6)
        {
            isEvil = false;
            SpawnGoodReward();
        }
        else
        {
            isEvil = true;
            SpawnBadReward();
        }

        Destroy(gameObject, duration);
    }

    private void SpawnGoodReward()
    {
        int goodItems = Random.Range(0, goodReward.Length);
        GameObject items = Instantiate(goodReward[goodItems]);
    }

    private void SpawnBadReward()
    {
        int badItems = Random.Range(0, evilReward.Length);
        GameObject items = Instantiate(goodReward[badItems]);
    }

}


