using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopKeeper : MonoBehaviour
{
    [SerializeField]
    private List<Product> products;
    [SerializeField]
    private List<GameObject> items;
    private Text itemName;
    private Text ItemDescription;
    private Text itemPrice;
    private Image itemicon;
    

    
    private void Start()
    {
        ItemInfo();
    }

    private void ItemInfo()
    {
        Transform items = transform.FindChild("ItemContainer");
        for (int i = 0; i < products.Count; i++)
        {

            itemName = items.GetChild(i).GetChild(0).GetComponent<Text>();
            ItemDescription = items.GetChild(i).GetChild(1).GetComponent<Text>();
            itemPrice = items.GetChild(i).GetChild(2).GetComponent<Text>();
            itemicon = items.GetChild(i).GetChild(3).GetComponent<Image>();
            itemName.text = products[i].name;
            ItemDescription.text =  products[i].description ;
            itemPrice.text = products[i].price ;
            itemicon.sprite = products[i].icon;
        }
    }


 
}
[System.Serializable]
public class Product
{
    [SerializeField]
    public string name = "Health Orb";
    [SerializeField]
    public string description = "Restores Health";
    [SerializeField]
    public string price = "2 Souls";
    [SerializeField]
    public Sprite icon;
    
}

