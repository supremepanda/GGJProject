
using UnityEngine;
[CreateAssetMenu]
public class Item:ScriptableObject
{
    public string ItemName;
    public GameObject itemObject;
    public ItemType itemType;
    public ItemVariation itemVariation;
    public Sprite itemImage;
}

