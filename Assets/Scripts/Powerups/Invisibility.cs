using UnityEngine;
using System.Collections;

public class Invisibility : Powerup
{
    private SpriteRenderer player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        powerUpDuration = 5.0f;
    }

    protected override void SetPowerUpSprite(Sprite sprite)
    {

    }

    protected override void CollectPowerUp(AudioClip audio)
    {
        PickUpPowerUp();
    }

    protected override void UsePowerUp(AudioClip audio)
    {
        player.enabled = false;
    }

    protected override void PowerUpTick(AudioClip audio)
    {
        Debug.Log("Using powerup");
    }

    protected override void StopPowerUp(AudioClip audio)
    {
        player.enabled = true;
    }

    protected override void DestroyPowerup(AudioClip audio)
    {
        Destroy(gameObject);
    }


}
