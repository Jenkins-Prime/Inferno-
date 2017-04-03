using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HUDManager : MonoBehaviour
{
    #region Archived
    //[Header ("MP Bar")]
    //[SerializeField] Image MPBar;

    //[Header ("Collectibles")]
    //[SerializeField] Text blueSouls;
    //[SerializeField] Text keys;

    //[Header ("Health")]
    //[SerializeField] Slider healthBar;

    //[Header ("Lives")]
    //[SerializeField] Text lifeText;

    ////method for the mp bar goes here

    //public void SetBlueSouls(int amount)
    //   {
    //	blueSouls.text = amount.ToString ();
    //}

    //public void SetKeys(int amount)
    //   {
    //	keys.text = amount.ToString ();
    //}

    //public void SetHealthUI(int amount)
    //   {
    //	//healthBar.value = (float)amount;
    //}

    //public void SetLifeUI(int amount) {
    //	lifeText.text = amount.ToString ();
    //}
    #endregion
    private static HUDManager instance;
    public static HUDManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new HUDManager();
            }
            return instance;
        }
    }

    public delegate void OnAction(int amount);

    public event OnAction GainHealth;
    public event OnAction LoseHealth;

    public void OnGainHealth(int amount)
    {
        if(GainHealth != null)
        {
            GainHealth(amount);
        }
    }

    public void OnLoseHealth(int amount)
    {
        if (LoseHealth != null)
        {
            LoseHealth(amount);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    


 

}
