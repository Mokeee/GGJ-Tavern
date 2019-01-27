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


    public void Start()
    {

    }


    public void ApplySprites(Sprite head, Sprite torso)
    {
        HeadImage.sprite = head;

        if (Race != Race.Halfling)
        {
            TorsoImage.sprite = torso;
        }
    }

    public void GenerateVisuals()
    {
        gameObject.SetActive(false);

        string torsoid = Race.ToString() + " " + Profession.ToString() + " Torso";
        string headid = Race.ToString() + " " + Profession.ToString() + " Head " + Random.Range(1, 4);

        var path = "Sprites/" + torsoid;
        Debug.Log(path);
        var torsoSprite = Resources.Load<Sprite>(path);

        path = "Sprites/" + headid;
        Debug.Log(path);
        var headSprite = Resources.Load<Sprite>(path);

        ApplySprites(headSprite, torsoSprite);

        if (Race.Halfling != Race)
        {
            string torsoMatId = "Materials/NPCMaterial";
            TorsoImage.material = Resources.Load<Material>(torsoMatId);
            Debug.Log(torsoMatId);
        }

        string headMatId = "Materials/NPCMaterial";
        Debug.Log(headMatId);
        HeadImage.material = Resources.Load<Material>(headMatId);
    }

}
