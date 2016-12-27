using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    protected bool isHit;

    protected virtual void SwitchHit(AudioClip audio) { }
    protected virtual void OnUse(AudioClip audio) { }
    protected virtual void OnExit(AudioClip audio) { }
    protected virtual void OnDestroy() { }

}
