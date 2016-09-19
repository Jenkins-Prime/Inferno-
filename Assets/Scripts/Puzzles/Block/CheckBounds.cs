using UnityEngine;
using System.Collections;

public class CheckBounds : MonoBehaviour
{
    public bool isInside;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (Physics2D.OverlapPoint(collider.bounds.min) && Physics2D.OverlapPoint(collider.bounds.max))
        {
            isInside = true;
        }
    }

    void OnTriggerExit2D()
    { 
       isInside = false;
    }
}
