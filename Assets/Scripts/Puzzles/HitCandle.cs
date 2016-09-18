using UnityEngine;
using System.Collections;

public class HitCandle : MonoBehaviour
{
    public GameObject block;
    public GameObject blockPosition;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Projectile")
        {
            GameObject temp = Instantiate(block, blockPosition.transform.position, blockPosition.transform.rotation) as GameObject;
            Destroy(gameObject);
        }
    }
}
