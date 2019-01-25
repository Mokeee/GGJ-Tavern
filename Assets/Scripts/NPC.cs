using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Need
{
    FOO
}

public enum Character
{
    FOO
}

public class NPC
{
    private LinkedList<Need> Needs { get; }
    private LinkedList<Need> SpecialNeeds { get; }

    private float ComfortLevel { get; }
    private Character Character { get; }

    public NPC(Character character, LinkedList<Need> needs)
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
