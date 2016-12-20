using UnityEngine;
using System.Collections;

public class Collectables : MonoBehaviour
{
    protected virtual void SetCollectableSprite(Sprite sprite) { }
    protected virtual void CollectCollectable(AudioClip audio) { }
    protected virtual void UseCollectable(AudioClip audio) { }
    protected virtual void DestroyCollectable(AudioClip audio) { }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            CollectCollectable(new AudioClip());
        }
    }

    private void OnTriggerExit2D()
    {
        DestroyCollectable(new AudioClip());
    }

}
