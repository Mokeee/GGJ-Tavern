using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]
public class InventoryItem : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public int InStockCount;
    public Need Need;
    public int Price;
}
