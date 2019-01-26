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

    public void ShowInventoryForResupply()
    {
        if(InventoryParent.transform.childCount == 1)
        {
            Start();
        }
        for (int i = 1; i < InventoryParent.transform.childCount; i++)
        {
            var displayer = InventoryParent.transform.GetChild(i).GetComponent<ItemDisplayer>();
            displayer.ActionButton.onClick.AddListener(() => { InventorySystem.BuyItem(displayer.Name.text); UpdateInventory(); });
            displayer.ActionButton.transform.GetChild(0).GetComponent<Text>().text = "Resupply";
        }
    }

    public void UpdateInventory()
    {
        MoneyText.text = InventorySystem.Inventory.Money.ToString();

        int index = 1;
        foreach (var item in InventorySystem.Inventory.Items)
        {
            Debug.Log("Update Loop: " + (index - 1));
            Debug.Log(item.InStockCount);
            var displayer = InventoryParent.transform.GetChild(index).GetComponent<ItemDisplayer>();
            displayer.Name.text = item.Name;
            displayer.StockCountText.text = item.InStockCount.ToString();
            displayer.NeedText.text = item.Need.ToString();
            displayer.CostText.text = item.Price.ToString();
            index++;
        }
    }
}
