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

    [SerializeField]
    private GameObject itemIndicator;
    private int itemPosition;
    [SerializeField]
    private float indicatorHeight;
    private UIManager uiManager;
    [SerializeField]
    private GameObject itemInformation;
    private bool isShowingItemInformation;


    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
        isShowingItemInformation = false; 
    }
    private void Start()
    {
        ItemInfo();
        itemPosition = 0;
        indicatorHeight = 125.0f;
    }
    private void Update()
    {
        if(!isShowingItemInformation)
        {
            SelectItem();
            FocusItem();
        }
        
    }
    private void ItemInfo()
    {
        GameObject items = GameObject.FindGameObjectWithTag("ItemContainer");
        for (int i = 0; i < products.Count; i++)
        {

            itemName = items.transform.GetChild(i).GetChild(0).GetComponent<Text>();
            itemicon = items.transform.GetChild(i).GetChild(1).GetComponent<Image>();

            itemName.text = products[i].name;
            itemicon.sprite = products[i].icon;
        }
    }

    private void SelectItem()
    {
        if(InputManager.Instance.NextItem())
        {
            if(itemPosition < items.Count -1)
            {
                itemPosition++;
                itemIndicator.transform.position = new Vector3(items[itemPosition].transform.position.x, items[itemPosition].transform.position.y + indicatorHeight, items[itemPosition].transform.position.z);

            }

        }
        if (InputManager.Instance.PreviousItem())
        {
            if (itemPosition > 0)
            {
                itemPosition--;
                itemIndicator.transform.position = new Vector3(items[itemPosition].transform.position.x, items[itemPosition].transform.position.y + indicatorHeight, items[itemPosition].transform.position.z);

            }
            else
            {
                itemPosition = 0;
            }

        }
      
    }
    private void FocusItem()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(itemPosition == i)
            {
                if(InputManager.Instance.JumpButton())
                {
                    uiManager.FocusItem();
                    SetItemInformation(itemPosition);
                    isShowingItemInformation = true;
                }
            }
        }
    }
    private void SetItemInformation(int itemPosition)
    {
    
        
           
            itemName = itemInformation.transform.GetChild(1).GetComponent<Text>();
            ItemDescription = itemInformation.transform.GetChild(2).GetComponent<Text>();
            itemPrice = itemInformation.transform.GetChild(3).GetComponent<Text>();
            itemicon = itemInformation.transform.GetChild(0).GetComponent<Image>();
          

            itemName.text = products[itemPosition].name;
            ItemDescription.text = products[itemPosition].description;
            itemPrice.text = products[itemPosition].price;
            itemicon.sprite = products[itemPosition].icon;

        

    }
    public void BuyItem()
    {

    }
    public void CloseItem()
    {

        uiManager.UnfocusItem();
        isShowingItemInformation = false;
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

