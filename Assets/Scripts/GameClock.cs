using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    [Range(1, 5)]
    public static int MaxStayDuration;
    [Range(1,5)]
    public int MaxAmountOfCustomersPerDay;

    public DialogSystem DialogSystem;
    public InventorySystem InventorySystem;
    public NPCPool pool;

    public int Day;
    int CustomerCount;

    List<List<NPC>> CustomersOfDay;

    ConversationOverData Cod;


    // Start is called before the first frame update
    void Start()
    {
        Day = 0;

        Cod = new ConversationOverData();

        DialogSystem.ConversationOverEvent.AddListener(delegate { HandleEndOfDialog(Cod); });
        InventorySystem.EndedFullfillment.AddListener(() => { HandleEndOfFullfillment(); });
        InventorySystem.EndedResupply.AddListener(() => { EndDay(); });
        StartResupply();
    }

    /// <summary>
    /// Gets all customers for new day.
    /// Calls proceed.
    /// </summary>
    void GetAllCustomersForNewDay()
    {
        CustomersOfDay = new List<List<NPC>>();
        CustomersOfDay.Add(GetLeavingCustomers());
        CustomersOfDay.Add(GetRevistingCustomers());

        CustomerCount = CustomersOfDay[0].Count;
        CustomerCount += CustomersOfDay[1].Count;

        if (CustomerCount > MaxAmountOfCustomersPerDay)
            CustomersOfDay.Add(new List<NPC>());
        else
            CustomersOfDay.Add(GenerateRandomNewCustomers());

        CustomerCount += CustomersOfDay[2].Count;
        Debug.Log(CustomerCount);

        Proceed();
    }

    /// <summary>
    /// Gets the revisting customers.
    /// </summary>
    /// <returns>The revisting customers.</returns>
    List<NPC> GetRevistingCustomers()
    {
        return pool.GetRevisitingNPCS();
    }

    /// <summary>
    /// Gets the leaving customers.
    /// Moves Customers array.
    /// </summary>
    /// <returns>The leaving customers.</returns>
    List<NPC> GetLeavingCustomers()
    {
        return pool.GetLeavingNPCS();
    }

    /// <summary>
    /// Generates random new customers.
    /// </summary>
    /// <returns>Random new customers.</returns>
    List<NPC> GenerateRandomNewCustomers()
    {
        var newNpcs = new List<NPC>();

        for(int i = 0; i < MaxAmountOfCustomersPerDay - CustomerCount; i++)
        {
            var npc = new NPC(GeneratePersonality());
            newNpcs.Add(pool.RegisterNPC(npc));
        }
        return newNpcs;
    }

    Personality GeneratePersonality()
    {
        int i = Random.Range(0, 3);

        return (Personality)i;
    }

    /// <summary>
    /// Starts the dialog by activating the DialogSystem.
    /// </summary>
    /// <param name="npc">Npc.</param>
    /// <param name="isLeaving">If set to <c>true</c> is leaving.</param>
    void StartDialog(NPC npc, bool isLeaving)
    {
        Cod.npc = npc;
        Cod.isLeaving = isLeaving;
        pool.NPCVisuals[npc.id].SetActive(true);
        DialogSystem.StartDialog(npc, isLeaving);
    }

    /// <summary>
    /// Handles the end of dialog.
    /// </summary>
    /// <param name="npc">Npc.</param>
    /// <param name="isLeaving">Is leaving.</param>
    public void HandleEndOfDialog(ConversationOverData cod)
    {
        if (cod.isLeaving)
        {
            pool.AnnihilateNPC(cod.npc.id);
            Proceed();
        }
        else
            StartFullfillment(cod.npc);
    }

    /// <summary>
    /// Proceed to the next customer.
    /// Starts DialogSystem.
    /// </summary>
    void Proceed()
    {
        if (CustomerCount > 0)
        {
            bool customerFound = false;

            while (!customerFound)
            {
                int index = Random.Range(0, 3);

                var customerList = CustomersOfDay[index];

                if (customerList.Count > 0)
                {
                    var customerIndex = Random.Range(0, customerList.Count);
                    var customer = customerList[customerIndex];

                    StartDialog(customer, (index == 0));

                    CustomersOfDay[index].RemoveAt(customerIndex);
                    CustomerCount--;
                    customerFound = true;
                }
            }
        }
        else
        {
            StartResupply();
        }
    }

    /// <summary>
    /// Starts the fullfillment process.
    /// </summary>
    /// <param name="npc">NPC</param>
    void StartFullfillment(NPC npc)
    {
        InventorySystem.StartFullfillingNeeds(npc);
    }

    /// <summary>
    /// Handles the end of fullfillment.
    /// </summary>
    public void HandleEndOfFullfillment()
    {
        pool.NPCVisuals[Cod.npc.id].SetActive(false);
        Proceed();
    }

    /// <summary>
    /// Invokes the resupply functionality of the inventory system.
    /// </summary>
    void StartResupply()
    {
        InventorySystem.StartResupplying();
    }

    /// <summary>
    /// Is invoked on the end of the day.
    /// Asseses income and proceeds to next day.
    /// </summary>
    void EndDay()
    {
        Day++;

        //Show UI
        GetAllCustomersForNewDay();
    }
}
