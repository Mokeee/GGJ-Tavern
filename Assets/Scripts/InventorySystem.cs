using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventorySystem : MonoBehaviour
{
    public Inventory Inventory;
    public NPCPool pool;
    public UnityEvent StartedFullfillment;
    public UnityEvent EndedFullfillment;
    public UnityEvent StartedResupply;
    public UnityEvent EndedResupply;

    NPC NPC;

    public void StartFullfillingNeeds(NPC npc)
    {
        NPC = npc;
        StartedFullfillment.Invoke();
    }

    public void StartResupplying()
    {
        StartedResupply.Invoke();
    }

    public void SellItem(string name)
    {
        NPC.DuePayment += Inventory.SellItemByName(name,ref NPC);
    }

    public void BuyItem(string name)
    {
        Inventory.PurchaseStockIncreaseByName(name);
    }

    public void EndFullfillment()
    {
        Inventory.Money += NPC.DuePayment * NPC.ComfortLevel;
        Debug.Log(NPC.DuePayment);
        Debug.Log(NPC.ComfortLevel);

        if (NPC.ComfortLevel < 1)
        {
            pool.AnnihilateNPC(NPC.ID);

            if (NPC.Satisfied)
            {
                Inventory.SatisfiedCustomers--;
                NPC.Satisfied = false;
            }
        }
        else
        {
            if (!NPC.Satisfied)
            {
                Inventory.SatisfiedCustomers++;
                NPC.Satisfied = true;
            }
        }

        pool.NPCVisuals[NPC.ID].SetActive(false);

        pool.UpdateNPC(NPC);
        NPC = null;
        EndedFullfillment.Invoke();
    }

    public void EndResupply()
    {
        EndedResupply.Invoke();
    }
}
