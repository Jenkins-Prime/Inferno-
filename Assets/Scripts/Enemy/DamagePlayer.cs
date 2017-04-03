using UnityEngine;
using System.Collections;

public class DamagePlayer : MonoBehaviour
{
	[SerializeField]
    int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            EventManager.Instance.DecreaseHealth(damageAmount);
        }
    }
}
