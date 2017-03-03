using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionBolt : MonoBehaviour
{
    public LayerMask detectLayer;

    private Player player;
    private SpriteRenderer playerSprite;
    private bool isManipulated;
    [SerializeField]
    private float possessDistance;

    [SerializeField]
    private float fireSpeed;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(fireSpeed * Time.deltaTime, GetComponent<Rigidbody2D>().velocity.y);
        player.canFire = false;
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            PossessEnemy();
            col.gameObject.GetComponent<EnemyManipulate>().controlMode = true;
            player.gameObject.transform.parent = col.gameObject.transform;
            player.gameObject.transform.position = col.gameObject.transform.position;
        }
    }


    private void PossessEnemy()
    {
        player.canFire = true;
        player.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        player.canMove = false;
        playerSprite.enabled = false;
        Destroy(gameObject);
    }


}
