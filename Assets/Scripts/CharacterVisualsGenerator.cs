using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race
{
    Human,
    Orc
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

    public void GeneratePrefab(Transform parent)
    {
        GameObject character;
        Race race = (Race)Random.Range(1, 2);
        Profession profession = (Profession)Random.Range(0, 4);

        switch (race)
        {
            case Race.Human:
                break;
            case Race.Orc:
                character = Instantiate(OrcPrefab, parent);
                var display = character.GetComponent<NPCDisplayer>();
                display.Race = race;
                display.Profession = profession;
                break;
        }
    }
}
