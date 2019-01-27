using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDisplayer : MonoBehaviour
{
    public Image HeadImage;
    public Image TorsoImage;

    public Race Race;
    public Profession Profession;

    public void ApplySprites(Sprite head, Sprite torso)
    {
        HeadImage.overrideSprite = head;

        if (Race != Race.Halfling)
        {
            TorsoImage.overrideSprite = torso;
        }
    }

    public void GenerateVisuals()
    {
        gameObject.SetActive(false);
        var path = "Sprites/" + Race.ToString() + " " + Profession.ToString() + " Torso";
        Debug.Log(path);
        var torsoSprite = Resources.Load<Sprite>(path);
        path = "Sprites/" + Race.ToString() + " " + Profession.ToString() + " Head " + Random.Range(0, 4);
        var headSprite = Resources.Load<Sprite>(path);
        Debug.Log(path);

        ApplySprites(headSprite, torsoSprite);
    }
}
