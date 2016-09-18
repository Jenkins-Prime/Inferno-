using UnityEngine;
using System.Collections;

public class BlockTrigger : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector3 positionToSpawn;
    public string tagName;
    public bool isSwitch;

    private Animator playerAnimator;
    private Rigidbody2D rBody;
   

    void Awake()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        rBody = GameObject.FindGameObjectWithTag("Block").GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagName)
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

    }

    private void ActivateSwitch()
    {
       
    }

    private void ReachedLocation()
    {
        GameObject key = Instantiate(objectToSpawn, positionToSpawn, transform.rotation) as GameObject;
        key.name = "Key";
    }
}
