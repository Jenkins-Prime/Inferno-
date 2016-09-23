using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Vector2 margin;

    private Transform player;
    private BoxCollider2D cameraBounds;
    private bool isFollowing;
    private Vector2 smoothing;
    private Vector3 minBorder;
    private Vector3 maxborder;
    private Vector2 velocity;

    private PlayerController playerController;
    private LevelManager levelManager;


    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }
    void Start ()
    {
       // minBorder = cameraBounds.bounds.min;
       // maxborder = cameraBounds.bounds.max;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
        isFollowing = true;
        smoothing.x = 2.0f;
        smoothing.y = 2.0f;

	}
	
	void LateUpdate ()
    {

        float currentXPosition = transform.position.x;
        float currentYPosition = transform.position.y;

        if (!playerController.isDead)
        {
            currentXPosition = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocity.x, smoothing.x * Time.deltaTime);
            currentYPosition = Mathf.SmoothDamp(transform.position.y, player.position.y, ref velocity.y, smoothing.y * Time.deltaTime);

        }
        else
        {
            currentXPosition = Mathf.SmoothDamp(transform.position.x, levelManager.curCheckPoint.position.x, ref velocity.x, smoothing.x * Time.deltaTime);
            currentYPosition = Mathf.SmoothDamp(transform.position.y, levelManager.curCheckPoint.position.y, ref velocity.y, smoothing.y * Time.deltaTime);
        }

        transform.position = new Vector3(currentXPosition, currentYPosition, transform.position.z);


    }


}
