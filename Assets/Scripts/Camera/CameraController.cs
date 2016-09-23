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
        smoothing.x = 4.0f;
        smoothing.y = 4.0f;

	}
	
	void LateUpdate ()
    {

        float currentXPosition = transform.position.x;
        float currentYPosition = transform.position.y;

        if (playerController.isDead)
        {
            isFollowing = false;
            currentXPosition = Mathf.Lerp(currentXPosition, levelManager.curCheckPoint.transform.position.x, smoothing.x * Time.deltaTime);
            currentYPosition = Mathf.Lerp(currentYPosition, levelManager.curCheckPoint.transform.position.y, smoothing.y * Time.deltaTime);
        }
        else
        {
            isFollowing = true;
        }

        if (isFollowing)
        {
            
                if (Mathf.Abs(currentXPosition - player.position.x) > margin.x)
                {
                    currentXPosition = Mathf.Lerp(currentXPosition, player.position.x, smoothing.x * Time.deltaTime);
                }

                if (Mathf.Abs(currentYPosition - player.position.y) > margin.y)
                {
                    currentYPosition = Mathf.Lerp(currentYPosition, player.position.y, smoothing.y * Time.deltaTime);
                }
        }
   
       transform.position = new Vector3(currentXPosition, currentYPosition, transform.position.z);

       
    }


}
