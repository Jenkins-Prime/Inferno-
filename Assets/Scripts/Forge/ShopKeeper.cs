using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopKeeper : MonoBehaviour
{

//    Shop:
//player enters shop
//    can have different types of merchants
//    one specializes in health
//    one sales armor.

//a UI shop manager to showcase all the items
//need a list of items the shop owner has
//Need a shopkeeper object
//shopkeep can sell only(for now), has an inventory to sale, items in inventory have a price(value)

//    Note: Perhaps base class item.cs can hold a field for "Value" and possibly "ItemName"

    [SerializeField]
    private List<string> stock; //should hold a generic list of items
    [SerializeField]
    private List<Text> itemDisplay;
    private Canvas shopMenu;

    
    private void Start()
    {
       // stock = new List<Product>();
        //stock.Add(new Product(productName, 10.0f, ProductType.HEALTH));
    }

}


