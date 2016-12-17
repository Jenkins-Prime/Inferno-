using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealthContainers;
    public int startingHealth;
    public Image[] healthContainers;
    public Sprite[] health;

    private int currentHealth;
    private int maxHealth;
    private int healthPerContainer;

	void Start ()
    {
        healthPerContainer = 2;

        currentHealth = startingHealth * healthPerContainer;
        maxHealth = maxHealthContainers * healthPerContainer;
        InitialiseHealth();
	
	}

    private void InitialiseHealth()
    {
        for (int index = 0; index < maxHealthContainers; index++)
        {
            if (startingHealth <= index)
            {
                healthContainers[index].enabled = false;
            }
            else
            {
                healthContainers[index].enabled = true;
            }
        }

        UpdateHealth();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Damage(1);
            UpdateHealth();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            AddOrbs(2);
        }
    }

    private void UpdateHealth()
    {
        bool isEmpty = false;
        int playerHealth = 0;

        foreach(Image img in healthContainers)
        {
            if (isEmpty)
            {
                img.sprite = health[0];
            }
            else
            {
                playerHealth++;

                if (currentHealth >= playerHealth * healthPerContainer)
                {
                    img.sprite = health[health.Length - 1];
                }
                else
                {
                    int currentOrbHealth = (int)(healthPerContainer - (healthPerContainer * playerHealth - currentHealth));
                    int healthPerSprite = healthPerContainer / (health.Length - 1);
                    int healthIndex = currentOrbHealth / healthPerSprite;
                    img.sprite = health[healthIndex];
                    isEmpty = true;
                }

            }
        }
    }

    public void Damage(int amount)
    {
        currentHealth -= amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth * healthPerContainer);
        UpdateHealth();
    }

    public void AddOrbs(int amount)
    {
        startingHealth += amount;
        startingHealth = Mathf.Clamp(startingHealth, 0, maxHealthContainers);
        currentHealth = startingHealth * healthPerContainer;
        InitialiseHealth();
    }
}
