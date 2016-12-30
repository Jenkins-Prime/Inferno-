using UnityEngine;
using System.Collections;

public class TimerSwitch : MonoBehaviour
{
    [SerializeField]
    private float switchTimer;

    [SerializeField]
    private Animator eventAnimation;

    private Animator switchAnimation;

    [SerializeField]
    private GameObject itemToSpawn;

    [SerializeField]
    private string switchAnimationName;

    [SerializeField]
    private string eventAnimationName;

    [SerializeField]
    private bool hasItem;

    protected virtual void SwitchHit(AudioClip audio)
    {
        switchAnimation = GetComponent<Animator>();
        switchAnimation.SetBool(switchAnimationName, true);
    }

    protected virtual void OnUse(AudioClip audio)
    {
        if (hasItem)
        {
            GameObject item = Instantiate(itemToSpawn, transform.position, transform.rotation) as GameObject;
            hasItem = false;
        }

        eventAnimation.SetBool(eventAnimationName, true);
    }

    protected virtual void OnExit(AudioClip audio)
    {
        switchAnimation = GetComponent<Animator>();
        switchAnimation.SetBool(switchAnimationName, false);
        eventAnimation.SetBool(eventAnimationName, false);
    }

    private IEnumerator StartTimer()
    {
        SwitchHit(new AudioClip());
        float currentTime = Time.time;
        while (Time.time - currentTime < switchTimer)
        {
            OnUse(new AudioClip());
            yield return null;
        }
        OnExit(new AudioClip());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(StartTimer());
        }
    }

}
