using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private Vector2 margin;
    [SerializeField]
    private Vector2 smoothing;
    private BoxCollider2D cameraBounds;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Camera cameraSize;


    private PlayerController playerController;
    private LevelManager levelManager;


    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        cameraBounds = GameObject.FindGameObjectWithTag("Bounds").GetComponent<BoxCollider2D>();
        cameraSize = GetComponent<Camera>();
    }
    void Start ()
    {
		player = GameObject.FindGameObjectWithTag ("Player").transform;

        minBounds = cameraBounds.bounds.min;
        maxBounds = cameraBounds.bounds.max;

        smoothing.x = 4.0f;
        smoothing.y = 4.0f;

	}
	
	void LateUpdate ()
    {
        FollowPlayer();  
    }

    private void FollowPlayer()
    {
        float currentXPosition = transform.position.x;
        float currentYPosition = transform.position.y;
        float cameraWidth = cameraSize.orthographicSize * ((float)(Screen.width / Screen.height));

        if (playerController.isDead)
        {

            currentXPosition = Mathf.Lerp(currentXPosition, levelManager.curCheckPoint.position.x, smoothing.x * Time.deltaTime);
            currentYPosition = Mathf.Lerp(currentYPosition, levelManager.curCheckPoint.position.y, smoothing.y * Time.deltaTime);

            currentXPosition = Mathf.Clamp(currentXPosition, minBounds.x + cameraWidth, maxBounds.x - cameraWidth);
            currentYPosition = Mathf.Clamp(currentYPosition, minBounds.y + cameraSize.orthographicSize, maxBounds.y - cameraSize.orthographicSize);
        }
        else
        {
            if (Mathf.Abs(currentXPosition - player.position.x) > margin.x)
            {
                currentXPosition = Mathf.Lerp(currentXPosition, player.position.x, smoothing.x * Time.deltaTime);
            }

            if (Mathf.Abs(currentYPosition - player.position.y) > margin.y)
            {
                currentYPosition = Mathf.Lerp(currentYPosition, player.position.y, smoothing.y * Time.deltaTime);
            }

            currentXPosition = Mathf.Clamp(currentXPosition, minBounds.x + cameraWidth, maxBounds.x - cameraWidth);
            currentYPosition = Mathf.Clamp(currentYPosition, minBounds.y + cameraSize.orthographicSize, maxBounds.y - cameraSize.orthographicSize);

        }

        transform.position = new Vector3(currentXPosition, currentYPosition, transform.position.z);

    }

}
