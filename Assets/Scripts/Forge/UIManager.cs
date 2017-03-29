using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject itemInformation;
    
    void Awake()
    {
        itemInformation.SetActive(false);

    }

    public void FocusItem()
    {
        
        itemInformation.SetActive(true);
        
        
    }
    public void UnfocusItem()
    {
        itemInformation.SetActive(false);
    }
	
}
