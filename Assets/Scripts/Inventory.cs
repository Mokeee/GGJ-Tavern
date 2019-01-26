using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryItem[] items;
    public float Money;

    public void SetItemByName(InventoryItem item, string name)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].name.Equals(name))
            {
                items[i] = item;
            }
        }
    }

    public void PurchaseStockIncreaseByName(string name)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name.Equals(name))
            {
                int cost = items[i].Price;

                if (cost < Money)
                {
                    IncreaseStockItemByName(name);
                    Money -= cost;
                }
            }
        }

    }

    public int SellItemByName(string name, ref NPC npc)
    {
        int cost = 0;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name.Equals(name))
            {
                if (items[i].InStockCount > 0)
                {
                    DecreaseStockItemByName(name);
                    cost = items[i].Price;
                    if (npc.Needs.Contains(items[i].Need))
                        npc.Needs.Remove(items[i].Need);
                    else if (npc.SpecialNeeds.Contains(items[i].Need))
                        npc.Needs.Remove(items[i].Need);
                }
            }
        }

        return cost;
    }

    void IncreaseStockItemByName(string name)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name.Equals(name))
            {
                items[i].InStockCount++;
            }
        }
    }

    void DecreaseStockItemByName(string name)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name.Equals(name))
            {
                items[i].InStockCount--;
            }
        }
    }

}
