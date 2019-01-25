using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Need
{
    None,
	Hunger
}

public enum Personality
{
    Heroic,
	Noble,
	Shy,
}

public class NPC
{
    private LinkedList<Need> Needs { get; }
    private LinkedList<Need> SpecialNeeds { get; }

    private float ComfortLevel { get; }
    private Personality Character { get; }

    public NPC(Personality character, LinkedList<Need> needs)
    {
        this.Needs = needs;
        this.SpecialNeeds = new LinkedList<Need>();
        this.Character = character;
    }


    public void AddSpecialNeed(Need need)
    {
        SpecialNeeds.AddLast(need);
    }

}
