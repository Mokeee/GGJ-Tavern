using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Need
{
    None,
    Hunger,
    Tired,
    Injured,
    Poisoned,
    Thirsty,
    DamagedHelmet,
    DamagedChestPiece,
    DamagedWeapon,
    RechargeMagic,
    MakeAPotion,
    BuySupplies
}

public enum Personality
{
    Heroic,
	Noble,
	Shy,
}

public class NPC
{
    public List<Need> Needs;
    public List<Need> SpecialNeeds;

    public float ComfortLevel { get; }
    public Personality Character { get; }

    public int StayDuration = 0;
    public int FutureStayDuration = 0;
    public int id;

    public int DuePayment;

    public NPC(Personality character)
    {
        Needs = new List<Need>();
        SpecialNeeds = new List<Need>();
        Character = character;
    }


    public void AddSpecialNeed(Need need)
    {
        SpecialNeeds.Add(need);
    }

}
