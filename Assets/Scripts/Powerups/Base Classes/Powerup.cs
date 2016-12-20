using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    protected float powerUpDuration;

    public void PickUpPowerUp()
    {
        StartCoroutine(StartTimer());
    }

    protected virtual void SetPowerUpSprite(Sprite sprite){ }

    protected virtual void CollectPowerUp(AudioClip audio){}

    protected virtual void UsePowerUp(AudioClip audio) {}

    protected virtual void PowerUpTick(AudioClip audio) { }

    protected virtual void StopPowerUp(AudioClip audio){}

    protected virtual void DestroyPowerup(AudioClip audio) { }

    private IEnumerator StartTimer()
    {
        UsePowerUp(new AudioClip());
        float currentTime = Time.time;
        while (Time.time - currentTime < powerUpDuration)
        {
            PowerUpTick(new AudioClip());
            yield return null;
        }
       StopPowerUp(new AudioClip());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            CollectPowerUp(new AudioClip());
            DiableObject();
        }
    }

    private void DiableObject()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

}
