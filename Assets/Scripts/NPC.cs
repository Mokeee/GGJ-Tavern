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

    public float ComfortLevel;
    public Personality Character;

    public int StayDuration = 0;
    public int FutureStayDuration = 0;
    public int ID;
    public bool Satisfied;

    public int DuePayment;

    public NPC(Personality character)
    {
        Needs = new List<Need>();
        SpecialNeeds = new List<Need>();
        Character = character;
        ComfortLevel = 1.0f;
    }


    public void AddSpecialNeed(Need need)
    {
        SpecialNeeds.Add(need);
    }

}
