using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race
{
    Human,
    Orc,
    Halfling,
    Pigman
}

public enum Profession
{
    Rogue,
    Mage,
    Druid,
    Barbarian
}

public class CharacterVisualsGenerator : MonoBehaviour
{
    public GameObject OrcPrefab;
    public GameObject HumanPrefab;
    public GameObject PigmanPrefab;
    public GameObject HalflingPrefab;

    public void GeneratePrefab(Transform parent)
    {
        GameObject character;
        Race race = (Race)Random.Range(0, 4);
        Profession profession = (Profession)Random.Range(0, 4);

        switch (race)
        {
            case Race.Human:
                character = Instantiate(HumanPrefab, parent);
                var display = character.GetComponent<NPCDisplayer>();
                display.Race = race;
                display.Profession = profession;
                break;
            case Race.Orc:
                character = Instantiate(OrcPrefab, parent);
                var display1 = character.GetComponent<NPCDisplayer>();
                display1.Race = race;
                display1.Profession = profession;
                break;
            case Race.Halfling:
                character = Instantiate(HalflingPrefab, parent);
                var display2 = character.GetComponent<NPCDisplayer>();
                display2.Race = race;
                display2.Profession = profession;
                break;
            case Race.Pigman:
                character = Instantiate(PigmanPrefab, parent);
                var display3 = character.GetComponent<NPCDisplayer>();
                display3.Race = race;
                display3.Profession = profession;
                break;
        }
    }
}
