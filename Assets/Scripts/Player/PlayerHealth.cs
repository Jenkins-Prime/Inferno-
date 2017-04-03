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

    private void OnEnable()
    {
        EventManager.Instance.OnHealthDecrease += Damage;
        EventManager.Instance.OnHealthIncrease += IncreaseHealth;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnHealthDecrease -= Damage;
        EventManager.Instance.OnHealthIncrease -= IncreaseHealth;
    }

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

    private void Damage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth * healthPerContainer);
        UpdateHealth();
    }

    private void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth * healthPerContainer);
        UpdateHealth();
    }

    private void IncreaseHealthAmount(int amount)
    {
        startingHealth += amount;
        startingHealth = Mathf.Clamp(startingHealth, 0, maxHealthContainers);
        currentHealth = startingHealth * healthPerContainer;
        InitialiseHealth();
    }
}
