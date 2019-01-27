using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const float SATISFACTIONINCREASE = 0.33f;
    public InventoryItem[] Items;
    public float Money = 100;
    public int SatisfiedCustomers;

    private void Awake()
    {
        for(int i = 0; i < Items.Length; i++)
        {
            Items[i].InStockCount = 0;
        }
    }

    public void SetItemByName(InventoryItem item, string name)
    {
        for(int i = 0; i < Items.Length; i++)
        {
            if(Items[i].name.Equals(name))
            {
                Items[i] = item;
            }
        }
    }

    public void PurchaseStockIncreaseByName(string name)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].name.Equals(name))
            {
                int cost = Items[i].Price;

                if (cost <= Money)
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

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].name.Equals(name))
            {
                if (Items[i].InStockCount > 0)
                {
                    DecreaseStockItemByName(name);
                    cost = Items[i].Price;
                    if (npc.Needs.Contains(Items[i].Need))
                    {
                        npc.Needs.Remove(Items[i].Need);
                        npc.ComfortLevel += SATISFACTIONINCREASE;
                    }
                    else if (npc.SpecialNeeds.Contains(Items[i].Need))
                    {
                        npc.SpecialNeeds.Remove(Items[i].Need);
                        npc.ComfortLevel += SATISFACTIONINCREASE;
                    }
                    else
                    {
                        npc.ComfortLevel -= SATISFACTIONINCREASE;
                    }
                }
            }
        }

        return cost;
    }

    void IncreaseStockItemByName(string name)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].name.Equals(name))
            {
                Debug.Log("increased: " + name);
                Items[i].InStockCount++;
            }
        }
    }

    void DecreaseStockItemByName(string name)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].name.Equals(name))
            {
                Items[i].InStockCount--;
            }
        }
    }

}
