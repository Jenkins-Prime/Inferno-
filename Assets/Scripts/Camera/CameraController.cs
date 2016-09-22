using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Vector2 margin;
    public Vector2 smoothing;

    private Transform player;
    private BoxCollider2D cameraBounds;
    private bool isFollowing;
    private Vector3 minBorder;
    private Vector3 maxborder;


    void Awake()
    {
        //cameraBounds = GameObject.FindGameObjectWithTag("Bounds").GetComponent<BoxCollider2D>();
    }
    void Start ()
    {
       // minBorder = cameraBounds.bounds.min;
       // maxborder = cameraBounds.bounds.max;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
        isFollowing = true;
	}
	
	void LateUpdate ()
    {

        float currentXPosition = transform.position.x;
        float currentYPosition = transform.position.y;

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

      // float cameraWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);

        //currentXPosition = Mathf.Clamp(currentXPosition, minBorder.x + cameraWidth, maxborder.x - cameraWidth);
       // currentYPosition = Mathf.Clamp(currentYPosition, minBorder.y + GetComponent<Camera>().orthographicSize, maxborder.y - GetComponent<Camera>().orthographicSize);

        transform.position = new Vector3(currentXPosition, currentYPosition, transform.position.z);

    }

}
