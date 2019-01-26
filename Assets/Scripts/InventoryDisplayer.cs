using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplayer : MonoBehaviour
{
    public GameObject ItemPrefab;
    public GameObject InventoryParent;
    public TextMeshProUGUI MoneyText;
    public Button CloseInventoryButton;

    public InventorySystem InventorySystem;

    bool isInitialized;

    // Start is called before the first frame update
    void Start()
    {
        if (!isInitialized)
        {
            MoneyText.text = InventorySystem.Inventory.Money.ToString();

            foreach (var item in InventorySystem.Inventory.Items)
            {
                var instance = Instantiate(ItemPrefab, InventoryParent.transform);
                var displayer = instance.GetComponent<ItemDisplayer>();
                displayer.Name.text = item.Name;
                displayer.StockCountText.text = "0";
                displayer.NeedText.text = item.Need.ToString();
                displayer.CostText.text = item.Price.ToString();
            }
            isInitialized = true;
        }
    }

    public void ShowInventoryForSelling()
    {
        if (InventoryParent.transform.childCount == 1)
        {
            Start();
        }
        for (int i = 1; i < InventoryParent.transform.childCount; i++)
        {
            var displayer = InventoryParent.transform.GetChild(i).GetComponent<ItemDisplayer>();
            displayer.ActionButton.onClick.RemoveAllListeners();
            displayer.ActionButton.onClick.AddListener(() => { InventorySystem.SellItem(displayer.Name.text); UpdateInventory(); });
            displayer.ActionButton.transform.GetChild(0).GetComponent<Text>().text = "Sell";
        }

        CloseInventoryButton.onClick.RemoveAllListeners();
        CloseInventoryButton.onClick.AddListener(() => InventorySystem.EndFullfillment());
        CloseInventoryButton.transform.GetChild(0).GetComponent<Text>().text = "Stop Selling";
    }

    public void ShowInventoryForResupply()
    {
        if(InventoryParent.transform.childCount == 1)
        {
            Start();
        }
        for (int i = 1; i < InventoryParent.transform.childCount; i++)
        {
            var displayer = InventoryParent.transform.GetChild(i).GetComponent<ItemDisplayer>();
            displayer.ActionButton.onClick.RemoveAllListeners();
            displayer.ActionButton.onClick.AddListener(() => { InventorySystem.BuyItem(displayer.Name.text); UpdateInventory(); });
            displayer.ActionButton.transform.GetChild(0).GetComponent<Text>().text = "Resupply";
        }

        CloseInventoryButton.onClick.RemoveAllListeners();
        CloseInventoryButton.onClick.AddListener(() => InventorySystem.EndResupply());
        CloseInventoryButton.transform.GetChild(0).GetComponent<Text>().text = "End Resupply";
    }

    public void UpdateInventory()
    {
        MoneyText.text = InventorySystem.Inventory.Money.ToString();

        int index = 1;
        foreach (var item in InventorySystem.Inventory.Items)
        {
            var displayer = InventoryParent.transform.GetChild(index).GetComponent<ItemDisplayer>();
            displayer.Name.text = item.Name;
            displayer.StockCountText.text = item.InStockCount.ToString();
            displayer.NeedText.text = item.Need.ToString();
            displayer.CostText.text = item.Price.ToString();
            index++;
        }
    }
}
