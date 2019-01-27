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

    public float StartY;
    public float EndY;
    public float Speed;

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

        GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, StartY);
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
            displayer.ActionButton.gameObject.SetActive(true);
        }

        CloseInventoryButton.onClick.RemoveAllListeners();
        CloseInventoryButton.onClick.AddListener(() => InventorySystem.EndFullfillment());
        CloseInventoryButton.transform.GetChild(0).GetComponent<Text>().text = "Stop Selling";

        StopAllCoroutines();
        StartCoroutine(MoveIn());
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
            displayer.ActionButton.gameObject.SetActive(true);
        }

        CloseInventoryButton.onClick.RemoveAllListeners();
        CloseInventoryButton.onClick.AddListener(() => InventorySystem.EndResupply());
        CloseInventoryButton.transform.GetChild(0).GetComponent<Text>().text = "End Resupply";

        StopAllCoroutines();
        StartCoroutine(MoveIn());
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

    public void HideInfoPanel()
    {
        for (int i = 1; i < InventoryParent.transform.childCount; i++)
        {
            var displayer = InventoryParent.transform.GetChild(i).GetComponent<ItemDisplayer>();
            displayer.ActionButton.onClick.RemoveAllListeners();
            displayer.ActionButton.gameObject.SetActive(false);
        }

        StopAllCoroutines();
        StartCoroutine(MoveOut());
    }

    private IEnumerator MoveIn()
    {
        var recT = GetComponent<RectTransform>();

        while (recT.anchoredPosition.y < EndY)
        {
            recT.anchoredPosition = new Vector2(recT.anchoredPosition.x, recT.anchoredPosition.y + Time.deltaTime * Speed);
            yield return null;
        }

        recT.anchoredPosition = new Vector2(recT.anchoredPosition.x, EndY);
    }


    private IEnumerator MoveOut()
    {
        var recT = GetComponent<RectTransform>();

        while (recT.anchoredPosition.y > StartY)
        {
            recT.anchoredPosition = new Vector2(recT.anchoredPosition.x, recT.anchoredPosition.y - Time.deltaTime * Speed);
            yield return null;
        }

        recT.anchoredPosition = new Vector2(recT.anchoredPosition.x, StartY);
    }
}
