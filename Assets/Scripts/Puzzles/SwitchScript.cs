using UnityEngine;
using System.Collections;

public class SwitchScript : Switch
{
    private Animator switchAnimator;
    [SerializeField]
    private string switchAnimationName;
    [SerializeField]
    private string targetAnimationName;
    [SerializeField]
    private Animator targetAnimator;

    void Awake()
    {
       switchAnimator = GetComponent<Animator>();
	}

    protected override void SwitchHit(AudioClip audio)
    {
        isHit = true;
        switchAnimator.SetBool(switchAnimationName, true);
        Debug.Log("Hit Switch");
        OnUse(new AudioClip());
    }

    protected override void OnUse(AudioClip audio)
    {
        Debug.Log("Opened Gate");
        targetAnimator.SetBool(targetAnimationName, true);

    }

    protected override void OnExit(AudioClip audio)
    {
        switchAnimator.SetBool(switchAnimationName, false);
        targetAnimator.SetBool(targetAnimationName, false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Block")
        {
            SwitchHit(new AudioClip());
        }
    }

    void OnTriggerExit2D()
    {
        OnExit(new AudioClip());
    }

}
