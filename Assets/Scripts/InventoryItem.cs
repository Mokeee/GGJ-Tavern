using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public int InStockCount;
    public Need Need;
    public int Price;
}
