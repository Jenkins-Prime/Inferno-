using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private static EventManager instance;
    private EventManager() { }

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventManager();
            }

            return instance;
        }
    }

    public delegate void HealthEventHandler(int amount);

    public event HealthEventHandler OnHealthIncrease;
    public  event HealthEventHandler OnHealthDecrease;


    public void IncreaseHealth(int amount)
    {
        if (OnHealthIncrease != null)
        {
            OnHealthIncrease(amount);
        }
    }

    public void DecreaseHealth(int amount)
    {
        if (OnHealthDecrease != null)
        {
            OnHealthDecrease(amount);
        }
    }


}
