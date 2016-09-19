using UnityEngine;
using System.Collections;

public class BlockTrigger : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector3 positionToSpawn;
    public string tagName;
    public bool isSwitch;

    private Animator playerAnimator;
    private CheckBounds checkBounds;
    private Rigidbody2D rBody;
   

    void Awake()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        checkBounds = gameObject.GetComponent<CheckBounds>();
        rBody = GameObject.FindGameObjectWithTag(tagName).GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isSwitch)
        {
            ActivateSwitch();
        }
        else
        {
           ReachedLocation();
        }
    }

    private void ActivateSwitch()
    {
       
    }

    private void ReachedLocation()
    {
        if (checkBounds.isInside)
        {
            GameObject key = Instantiate(objectToSpawn, positionToSpawn, transform.rotation) as GameObject;
            key.name = "Key";
            rBody.mass = 3.0f;
        }
        
        
    }
}
