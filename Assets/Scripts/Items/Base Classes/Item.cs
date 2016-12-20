using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    protected virtual void SetItemSprite(Sprite sprite) { }
    protected virtual void CollectItem(AudioClip audio) { }
    protected virtual void UseItem(AudioClip audio) { }
    protected virtual void DestroyItem(AudioClip audio) { }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            CollectItem(new AudioClip());
        }
    }

    private void OnTriggerExit2D()
    {
        DestroyItem(new AudioClip());
    }
}
