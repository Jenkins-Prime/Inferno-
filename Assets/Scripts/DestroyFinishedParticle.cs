using UnityEngine;
using System.Collections;

public class DestroyFinishedParticle : MonoBehaviour {

    private ParticleSystem thisparticlesystem;

	// Use this for initialization
	void Start () {
        thisparticlesystem = GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {
        if (thisparticlesystem.isPlaying)
            return;
            

    Destroy(gameObject);
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
