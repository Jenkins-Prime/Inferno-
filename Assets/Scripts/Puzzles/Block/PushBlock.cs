using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PushBlock : MonoBehaviour
{
    public string tagName;
    public bool hasGravity;

    private Rigidbody2D rBody;
    private Animator playerAnimator;
    


    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
       ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (!hasGravity)
        {
            rBody.gravityScale = 0.0f;
        }
        else
        {
            rBody.gravityScale = 1.0f;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player" && Input.GetAxis("Horizontal") > 0)
        {
            playerAnimator.SetBool("isPushing", true);
        }
        else
        {
            playerAnimator.SetBool("isPushing", false);
        }
    }
}
