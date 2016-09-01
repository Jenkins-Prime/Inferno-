using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public GameObject currentCheckpoint;

    private PlayerController player;

    public GameObject deathparticle;
    public GameObject respawnanim;
    public float respawnDelay;
    public int pointPenaltyOnDeath;
    public CameraController camcontrol;
    public float respawnAnimDelay;
    public HealthManager healthManager;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType <PlayerController>();
        camcontrol = FindObjectOfType<CameraController>();
        healthManager = FindObjectOfType<HealthManager>();
		Time.timeScale = 1f;

	}
	
	// Update is called once per frame
	void Update () {
	}

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
		player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		Instantiate (deathparticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
		camcontrol.isFollowing = false;
        ScoreManager.AddPoints(-pointPenaltyOnDeath);
        Debug.Log("You deeeeed!");
        yield return new WaitForSeconds(respawnDelay);
		player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.transform.position = currentCheckpoint.transform.position;
		camcontrol.isFollowing = true;
		Instantiate(respawnanim, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
		yield return new WaitForSeconds(respawnAnimDelay);
		player.enabled = true;
		player.GetComponent<Renderer>().enabled = true;
        healthManager.FullHealth();
        healthManager.isDead = false;
    }

}
