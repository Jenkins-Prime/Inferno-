using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public static int playerHealth;
    public int maxPlayerHealth;
    public bool isDead;
    private LevelManager levelManager;
   // Text text;
	public Slider healthBar;
	private LifeManager lifeSystem;
	private TimeManager theTime;

    // Use this for initialization
    void Start () {

     //   text = GetComponent<Text>();
		healthBar = GetComponent<Slider>();
		playerHealth = PlayerPrefs.GetInt ("PlayerCurrentHealth");
        levelManager = FindObjectOfType<LevelManager>();
        isDead = false; 
		lifeSystem = FindObjectOfType<LifeManager> ();
		theTime = FindObjectOfType<TimeManager> ();
    }
	
	// Update is called once per frame
	void Update () {
	
        if(playerHealth <= 0 && !isDead)
        {
            playerHealth = 0;
            levelManager.RespawnPlayer();
			lifeSystem.TakeLife ();
            isDead = true;
			theTime.ResetTime ();
        }

		if (playerHealth > maxPlayerHealth) 
		{
			playerHealth = maxPlayerHealth;
		}

		healthBar.value = playerHealth;
	}

    public static void HurtPlayer(int damageToGive)
    {
        playerHealth -= damageToGive;
		PlayerPrefs.SetInt ("PlayerCurrentHealth", playerHealth);
    }

    public void FullHealth()
    {
		playerHealth = PlayerPrefs.GetInt ("PlayerMaxHealth");
		PlayerPrefs.SetInt ("PlayerCurrentHealth", playerHealth);
    }

	public void KillPlayer()
	{
		playerHealth = 0;
	}
}
