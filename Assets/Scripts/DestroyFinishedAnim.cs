using UnityEngine;
using System.Collections;

public class DestroyFinishedAnim : MonoBehaviour {

    public Animator respawnAnimation;

    // Use this for initialization
    void Start()
    {

        respawnAnimation = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.respawnAnimation.GetCurrentAnimatorStateInfo(0).IsName("SpawnAnim"))
            return;

        
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}