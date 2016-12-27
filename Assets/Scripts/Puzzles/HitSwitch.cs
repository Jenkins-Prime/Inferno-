using UnityEngine;
using System.Collections;

public class HitSwitch : Switch
{
    [SerializeField]
    private GameObject objectToSpawn;
    private Transform playerPosition;


    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void SwitchHit(AudioClip audio)
    {
        OnUse(new AudioClip());
    }

    protected override void OnUse(AudioClip audio)
    {
        GameObject prefab = Instantiate(objectToSpawn);
        prefab.transform.position = playerPosition.position;
    }

    protected override void OnExit(AudioClip audio)
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            SwitchHit(new AudioClip());
        }
    }

    void OnTriggerExit2D()
    {
        OnExit(new AudioClip());
    }


}
